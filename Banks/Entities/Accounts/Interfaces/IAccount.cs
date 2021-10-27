namespace Banks.Entities.Accounts.Interfaces
{
    public interface IAccount
    {
        void CashWithdrawalFromAccount();
        void CashReplenishmentToAccount();
        void CashTransferToAnotherBankAccount();
    }
}