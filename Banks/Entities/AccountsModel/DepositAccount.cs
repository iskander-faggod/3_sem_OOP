using System;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel
{
    public class DepositAccount : IAccount
    {
        private decimal _deposit;
        private Guid _accountId;
        private decimal _lowPercent;
        private decimal _middlePercent;
        private decimal _highPercent;
        private DateTime _depositUnlockDate;

        public DepositAccount(decimal lowPercent, decimal middlePercent, decimal highPercent, Guid accountId, DateTime depositUnlockDate)
        {
            if (lowPercent > middlePercent) throw new BanksException("LowPercent can't be more then MiddlePercent");
            if (middlePercent > highPercent) throw new BanksException("middlePercent can't be more then highPercent");
            if (lowPercent > highPercent) throw new BanksException("LowPercent can't be more then highPercent");
            if (depositUnlockDate < DateTime.Now) throw new BanksException("Unlock date should be in future");
            _deposit = 0;
            _lowPercent = lowPercent;
            _middlePercent = middlePercent;
            _highPercent = highPercent;
            _accountId = accountId;
            _depositUnlockDate = depositUnlockDate;
        }

        public override void AccountPayoff()
        {
            if (_deposit < 50000) _deposit = (_deposit * _lowPercent) + _deposit;
            if (_deposit is > 50000 and < 100000) _deposit = (_deposit * _middlePercent) + _deposit;
            if (_deposit < 50000) _deposit = (_deposit * _highPercent) + _deposit;
        }

        public override void CashWithdrawalFromAccount(decimal value)
        {
            _deposit += value;
        }

        public override void CashReplenishmentToAccount(decimal value)
        {
            if (_deposit < value) throw new BanksException("You cant replenishment money from deposit");
            _deposit -= value;
        }

        public Guid GetAccountId() => _accountId;
    }
}