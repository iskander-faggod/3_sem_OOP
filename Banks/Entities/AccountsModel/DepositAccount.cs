using System;
using System.Globalization;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel
{
    public class DepositAccount : IAccount
    {
        private const decimal AmountForLowPercent = 50000;
        private const decimal AmountForMiddleAndHighPercent = 100000;

        private decimal _deposit;
        private Guid _accountId;
        private decimal _lowPercent;
        private decimal _middlePercent;
        private decimal _highPercent;
        private DateTime _depositUnlockDate;
        private decimal _monthCommission = 0;
        public DepositAccount(
            decimal lowPercent,
            decimal middlePercent,
            decimal highPercent,
            Guid accountId,
            DateTime depositUnlockDate)
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

        public void AccountPayoff()
        {
            int daysInYear = new GregorianCalendar().GetDaysInYear(DateTime.Now.Year);
            _monthCommission = _deposit switch
            {
                < AmountForLowPercent => (_deposit * _lowPercent) / daysInYear,
                > AmountForLowPercent and < AmountForMiddleAndHighPercent => (_deposit * _middlePercent) / daysInYear,
                <= AmountForLowPercent => (_deposit * _highPercent) / daysInYear,
                _ => _monthCommission
            };
        }

        public void AccrualOfCommission()
        {
            CashReplenishmentToAccount(_monthCommission);
            _monthCommission = 0;
        }

        public void CashWithdrawalFromAccount(decimal value)
        {
            if (value < 0) throw new BanksException("Value can't be less then 0");
            _deposit += value;
        }

        public void CashReplenishmentToAccount(decimal value)
        {
            if (value < 0) throw new BanksException("Value can't be less then 0");
            if (_deposit < value) throw new BanksException("You cant replenishment money from deposit");
            _deposit -= value;
        }

        public Guid GetAccountId() => _accountId;
        public decimal GetDeposit() => _deposit;
    }
}