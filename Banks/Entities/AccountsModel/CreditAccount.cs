using System;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel
{
    public class CreditAccount : IAccount
    {
        private decimal _deposit = 0;
        private decimal _limit;
        private decimal _percent;
        private Guid _accountId;
        private decimal _commission;

        public CreditAccount(decimal percent, decimal limit, decimal commission, Guid accountId)
        {
            if (percent < 0) throw new BanksException("Invalid percent data");
            _percent = percent;
            _commission = commission;
            _limit = limit;
            _accountId = accountId;
        }

        public void AccountPayoff()
        {
            _deposit = _deposit + (_deposit * _percent);
        }

        public override void CashWithdrawalFromAccount(decimal value)
        {
            if (value > _deposit) throw new BanksException("Value more then deposit");
            _deposit -= value;
        }

        public override void CashReplenishmentToAccount(decimal value)
        {
            _limit -= value;
        }

        public override void CashTransferToAnotherBankAccount(Bank bank, decimal value)
        {
            if (bank is null) throw new BanksException("Incorrect bank");
        }
    }
}