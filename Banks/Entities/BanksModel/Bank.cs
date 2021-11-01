using System;
using System.Collections.Generic;
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
                .SetMiddlePercent(_settings.AboveHundredThousandPercent)
                .Build());
            _clientAccountsById[client].Add(accountId);
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
    }
}