using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Backups.Tools;
using Banks.Commands;
using Banks.Entities.AccountsModel;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;
using Banks.Observing;
using Banks.Tools;

namespace Banks.Entities
{
    public class Bank : IObservable
    {
        private readonly Dictionary<Client, List<Guid>> _clientAccountsById;
        private readonly Dictionary<Guid, IAccount> _accounts;
        private readonly BankSettings _settings;
        private readonly List<IObserver> _observers;

        public Bank(BankSettings settings)
        {
            _settings = settings ?? throw new BanksException("Invalid settings");
            _clientAccountsById = new Dictionary<Client, List<Guid>>();
            _accounts = new Dictionary<Guid, IAccount>();
            _observers = new List<IObserver>();
        }

        public IAccount CreateCreditAccount(Client client)
        {
            var accountId = Guid.NewGuid();
            IAccount account = AccountBuilderFactory.Create(AccountType.Credit)
                .SetAccountId(accountId)
                .SetCommission(_settings.Commission)
                .SetLimit(_settings.TransferLimit)
                .Build();
            _accounts.Add(accountId, account);
            if (_clientAccountsById.ContainsKey(client))
            {
                _clientAccountsById[client].Add(account.GetAccountId());
            }
            else
            {
                _clientAccountsById.Add(client, new List<Guid> { account.GetAccountId() });
            }

            return account;
        }

        public Client GetClientById(int id)
        {
            if (id == default) throw new BanksException("Invalid id");
            return _clientAccountsById.Keys.FirstOrDefault(client => client.PassportId == id);
        }

        public IAccount CreateDepositAccount(Client client)
        {
            var accountId = Guid.NewGuid();
            IAccount account = AccountBuilderFactory.Create(AccountType.Deposit)
                .SetAccountId(accountId)
                .SetLowPercent(_settings.BelowFiftyThousandPercent)
                .SetMiddlePercent(_settings.BetweenFiftyAndHundredThousandPercent)
                .SetHighPercent(_settings.AboveHundredThousandPercent)
                .SetUnlockDate(_settings.DepositUnlockDate)
                .Build();
            _accounts.Add(accountId, account);
            if (_clientAccountsById.ContainsKey(client))
            {
                _clientAccountsById[client].Add(account.GetAccountId());
            }
            else
            {
                _clientAccountsById.Add(client, new List<Guid> { account.GetAccountId() });
            }

            return account;
        }

        public IAccount CreateDebitAccount(Client client)
        {
            var accountId = Guid.NewGuid();
            IAccount account = AccountBuilderFactory.Create(AccountType.Debit)
                .SetAccountId(accountId)
                .SetPercent(_settings.YearPercent)
                .Build();
            _accounts.Add(accountId, account);
            if (_clientAccountsById.ContainsKey(client))
            {
                _clientAccountsById[client].Add(account.GetAccountId());
            }
            else
            {
                _clientAccountsById.Add(client, new List<Guid> { account.GetAccountId() });
            }

            return account;
        }

        public void AddClient(Client client)
        {
            if (_clientAccountsById.Keys.Contains(client)) throw new BanksException("Client is already exist");
            _clientAccountsById.Add(client, new List<Guid>());
        }

        public void AddAccountToClient(Client client, IAccount clientAccount)
        {
            foreach (KeyValuePair<Client, List<Guid>> account in _clientAccountsById.Where(account => account.Key.Equals(client)))
            {
                account.Value.Add(clientAccount.GetAccountId());
            }

            _accounts.TryAdd(clientAccount.GetAccountId(), clientAccount);
        }

        public void HandleCommand(IBankCommand command, Client executor)
        {
            if (executor.IsSuspicious)
            {
                throw new BanksException(
                    "You can't do some operations with accounts before you don' fill all properties");
            }

            if (!_clientAccountsById.TryGetValue(executor, out List<Guid> accountIds))
            {
                throw new BanksException("Client does not exists");
            }

            var accounts = _accounts
                .Where(x => accountIds.Contains(x.Key))
                .ToImmutableDictionary();
            var context = new ClientContext(accounts, executor);
            try
            {
                command.Execute(context);
            }
            catch (BankCommandExecutionExecption e)
            {
                command.Rollback();
                throw new BanksException(e);
            }
        }

        public Dictionary<Client, List<Guid>> GetClientAccountsById() => _clientAccountsById;
        public Dictionary<Guid, IAccount> GetAccounts() => _accounts;
        public string GetBankName() => _settings.Name;

        public IAccount GetAccountById(Guid id)
        {
            return (from account in _accounts where account.Key == id select account.Value).FirstOrDefault();
        }

        public List<IAccount> GetAllAccounts()
        {
            return _accounts.Values.ToList();
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Modify(this);
            }
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }
    }
}