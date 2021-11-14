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

        public void AccountPayoff()
        {
            _monthCommission = (_deposit * _percent) / DateTime.Now.Year;
        }

        public void AccrualOfCommission()
        {
            CashReplenishmentToAccount(_monthCommission);
            _monthCommission = 0;
        }

        public void CashWithdrawalFromAccount(decimal value)
        {
            if (value < 0) throw new BanksException("Value can't be less then 0");
            if (value > _deposit) throw new BanksException("Value can't be more then deposit");
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