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
            // проверить проценты
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
            if (_deposit < 50000) _deposit = (_deposit * _lowPercent) + _deposit;
            if (_deposit is > 50000 and < 100000) _deposit = (_deposit * _middlePercent) + _deposit;
            if (_deposit < 50000) _deposit = (_deposit * _highPercent) + _deposit;
        }

        public override void CashWithdrawalFromAccount()
        {
            throw new NotImplementedException();
        }

        public override void CashReplenishmentToAccount()
        {
            throw new NotImplementedException();
        }

        public override void CashTransferToAnotherBankAccount()
        {
            throw new NotImplementedException();
        }
    }
}