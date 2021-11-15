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
        private decimal _monthComission = 0;

        public CreditAccount(decimal limit, decimal commission, Guid accountId)
        {
            if (accountId == Guid.Empty) throw new BanksException("AccountId is null");
            _commission = commission;
            _limit = limit;
            _accountId = accountId;
        }

        public void AccountPayoff()
        {
            if (_deposit < _limit) throw new BanksException("Deposit can't be less then money limit");
            if (_deposit < 0) _monthComission -= _deposit - _commission;
        }

        public void AccrualOfCommission()
        {
            CashReplenishmentToAccount(_monthComission);
            _monthComission = 0;
        }

        public void CashWithdrawalFromAccount(decimal value)
        {
            if (value < 0) throw new BanksException("Value can't be less then 0");
            _deposit -= value;
        }

        public void CashReplenishmentToAccount(decimal value)
        {
            if (value < 0) throw new BanksException("Value can't be less then 0");
            _deposit += value;
        }

        public Guid GetAccountId() => _accountId;
        public decimal GetDeposit() => _deposit;
    }
}