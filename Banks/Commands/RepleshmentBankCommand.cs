using System;
using Banks.Entities;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Commands
{
    public class RepleshmentBankCommand : IBankCommand
    {
        private readonly Guid _accountId;
        private readonly decimal _amount;

        private IAccount _currentAccount;
        private bool rollbackAvailable = false;

        public RepleshmentBankCommand(Guid accountId, decimal amount, IAccount currentAccount)
        {
            if (accountId == default) throw new BanksException("Invalid accountId");
            if (amount < 0) throw new BanksException("Amount can't be less then 0");
            _accountId = accountId;
            _amount = amount;
            _currentAccount = currentAccount;
        }

        public void Execute(ClientContext context)
        {
            if (rollbackAvailable) throw new BanksException("You can't execute");
            if (!context.GetAccounts().ContainsKey(_accountId))
            {
                throw new NonRevertableCommandExecption("Can't execute this command");
            }

            _currentAccount.CashReplenishmentToAccount(_amount);
            rollbackAvailable = true;
        }

        public void Rollback()
        {
            if (!rollbackAvailable)
            {
                throw new BanksException("You can't rollback");
            }

            _currentAccount.CashWithdrawalFromAccount(_amount);
            rollbackAvailable = false;
        }
    }
}