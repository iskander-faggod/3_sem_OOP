using System;

namespace Banks.Entities.AccountsModel.Creator
{
    public abstract class IAccount
    {
        public abstract void CashWithdrawalFromAccount(decimal value);
        public abstract void AccountPayoff();
        public abstract void AccrualOfCommission();
        public abstract void CashReplenishmentToAccount(decimal value);
        public abstract Guid GetAccountId();
        public abstract decimal GetDeposit();
    }
}