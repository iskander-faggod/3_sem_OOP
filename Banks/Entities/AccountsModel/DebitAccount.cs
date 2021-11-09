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
        private decimal _monthCommission = 0;

        public DebitAccount(decimal percent, Guid accountId)
        {
            if (percent < 0) throw new BanksException("Invalid percent data");
            _deposit = 0;
            _accountId = accountId;
            _percent = percent;
        }

        public override void AccountPayoff()
        {
            _monthCommission = (_deposit * _percent) / 365;
        }

        public override void AccrualOfCommission()
        {
            CashReplenishmentToAccount(_monthCommission);
            _monthCommission = 0;
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

        public override Guid GetAccountId() => _accountId;
        public override decimal GetDeposit() => _deposit;
    }
}