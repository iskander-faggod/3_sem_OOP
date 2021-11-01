using System;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel
{
    public class DebitAccount : IAccount
    {
        private decimal _deposit;
        private decimal _percent;
        private Guid _accountId;

        public DebitAccount(decimal deposit, decimal percent, Guid accountId)
        {
            if (deposit < 0) throw new BanksException("Invalid deposit data");
            if (percent < 0) throw new BanksException("Invalid percent data");
            _deposit = deposit;
            _accountId = accountId;
            _percent = percent;
        }

        public override void CashWithdrawalFromAccount(decimal value)
        {
            if (value > _deposit) throw new BanksException("Value can't be more then deposit");
            _deposit -= value;
        }

        public override void CashReplenishmentToAccount(decimal value)
        {
            _deposit += value;
        }

        public override void CashTransferToAnotherBankAccount(Bank bank, decimal value)
        {
            throw new NotImplementedException();
        }
    }
}