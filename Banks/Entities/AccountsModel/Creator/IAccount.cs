using System;

namespace Banks.Entities.AccountsModel.Creator
{
    public interface IAccount
    {
        void CashWithdrawalFromAccount(decimal value);
        void AccountPayoff();
        void AccrualOfCommission();
        void CashReplenishmentToAccount(decimal value);
        Guid GetAccountId();
        decimal GetDeposit();
    }
}