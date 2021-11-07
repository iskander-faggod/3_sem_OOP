using System;
using System.Collections.Generic;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel
{
    public class CreditAccount : IAccount
    {
        private decimal _deposit = 0;
        private decimal _limit;
        private Guid _accountId;
        private decimal _commission;

        public CreditAccount(decimal limit, decimal commission, Guid accountId)
        {
            if (accountId == Guid.Empty) throw new BanksException("AccountId is null");
            _commission = commission;
            _limit = limit;
            _accountId = accountId;
        }

        public override void AccountPayoff()
        {
            if (_deposit < _limit) throw new BanksException("Deposit can't be less then money limit");
            if (_deposit < 0) _deposit -= _deposit * _commission;
        }

        public override void CashWithdrawalFromAccount(decimal value)
        {
            _deposit -= value;
        }

        public override void CashReplenishmentToAccount(decimal value)
        {
            // TODO : Условие связанное с лимитом
            _deposit += value;
        }

        public override Guid GetAccountId() => _accountId;
        public override decimal GetDeposit() => _deposit;
    }
}