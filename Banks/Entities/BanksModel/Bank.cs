using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Backups.Tools;
using Banks.Commands;
using Banks.Entities.AccountsModel;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.Creator;
using Banks.Tools;

namespace Banks.Entities
{
    public class Bank
    {
        private readonly List<Client> _clients;
        private readonly Dictionary<Client, List<Guid>> _clientAccountsById;
        private readonly Dictionary<Guid, IAccount> _accounts;
        private readonly BankSettings _settings;

        public Bank(BankSettings settings)
        {
            _settings = settings ?? throw new BanksException("Invalid settings");
            _clients = new List<Client>();
            _clientAccountsById = new Dictionary<Client, List<Guid>>();
            _accounts = new Dictionary<Guid, IAccount>();
        }

        public void CreateCreditAccount(Client client)
        {
            var accountId = Guid.NewGuid();
            _accounts.Add(accountId, AccountBuilderFactory.Create(AccountType.Credit)
                .SetAccountId(accountId)
                .SetCommission(_settings.Commission)
                .SetLimit(_settings.TransferLimit)
                .Build());
            _clientAccountsById[client].Add(accountId);
        }

        public void CreateDepositAccount(Client client)
        {
            var accountId = Guid.NewGuid();
            _accounts.Add(accountId, AccountBuilderFactory.Create(AccountType.Deposit)
                .SetAccountId(accountId)
                .SetLowPercent(_settings.BelowFiftyThousandPercent)
                .SetMiddlePercent(_settings.BetweenFiftyAndHundredThousandPercent)
                .SetHighPercent(_settings.AboveHundredThousandPercent)
                .SetUnlockDate(_settings.DepositUnlockDate)
                .Build());
            if (!_clientAccountsById.TryGetValue(client, out var clientAccounts))
            {
                clientAccounts = new List<Guid> { accountId };
                _clientAccountsById.Add(client, clientAccounts);
            }

            clientAccounts.Add(accountId);
        }

        public void CreateDebitAccount(Client client)
        {
            var accountId = Guid.NewGuid();
            _accounts.Add(accountId, AccountBuilderFactory.Create(AccountType.Debit)
                .SetAccountId(accountId)
                .SetPercent(_settings.YearPercent)
                .Build());
            _clientAccountsById[client].Add(accountId);
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

        public void AddClient(Client client)
        {
            if (_clients.Contains(client)) throw new BanksException("Client is already added in this bank");
            _clients.Add(client);
        }

        public Guid TransferMoneyToAnotherAccount(IAccount oldAccount, IAccount newAccount, decimal value)
        {
            if (oldAccount is null) throw new BackupsException("OldAccount is null");
            if (newAccount is null) throw new BackupsException("OldAccount is null");
            newAccount.CashReplenishmentToAccount(value);
            oldAccount.CashWithdrawalFromAccount(value);
            var transferId = Guid.NewGuid();
            return transferId;
        }

        public Dictionary<Client, List<Guid>> GetClientAccountsById() => _clientAccountsById;
        public Dictionary<Guid, IAccount> GetAccounts() => _accounts;
        public string GetBankName() => _settings.Name;
    }
}