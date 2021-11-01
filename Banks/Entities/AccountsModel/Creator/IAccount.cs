using System;

namespace Banks.Entities.AccountsModel.Creator
{
    public abstract class IAccount
    {
        public abstract void CashWithdrawalFromAccount(decimal value);
        public abstract void CashReplenishmentToAccount(decimal value);
        public abstract void CashTransferToAnotherBankAccount(Bank bank, decimal value);
    }
}