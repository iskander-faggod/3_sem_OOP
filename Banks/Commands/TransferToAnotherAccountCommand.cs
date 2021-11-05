using System;
using Banks.Entities;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Commands
{
    public class TransferToAnotherAccountCommand
    {
        private readonly Guid _accountId;
        private readonly decimal _amount;
        private IAccount _currentAccount;
        private IAccount _newAccount;
        private bool rollbackAvailable = false;

        public TransferToAnotherAccountCommand(Guid accountId, decimal amount, IAccount currentAccount, IAccount newAccount)
        {
            if (accountId == default) throw new BanksException("Invalid accountId");
            if (amount < 0) throw new BanksException("Amount can't be less then 0");
            _accountId = accountId;
            _amount = amount;
            _currentAccount = currentAccount;
            _newAccount = newAccount;
        }

        public void Execute(ClientContext context)
        {
            if (!rollbackAvailable) throw new BanksException("You can't execute");
            if (!context.GetAccounts().ContainsKey(_accountId)) throw new TransferToAnotherAccountExcpetion("Can't execute this command");
            _currentAccount.CashWithdrawalFromAccount(_amount);
            _newAccount.CashReplenishmentToAccount(_amount);
            rollbackAvailable = true;
        }

        public void Rollback()
        {
            if (rollbackAvailable) throw new BanksException("You can't execute");
            _currentAccount.CashReplenishmentToAccount(_amount);
            _newAccount.CashWithdrawalFromAccount(_amount);
            rollbackAvailable = false;
        }
    }
}