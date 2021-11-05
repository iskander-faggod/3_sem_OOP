using System.Collections.Generic;
using System.Linq;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities
{
    public class MainBank
    {
        private readonly List<Bank> _banks;

        public MainBank(List<Bank> banks)
        {
            _banks = banks ?? throw new BanksException("Invalid banks");
        }

        public MainBank()
        {
            _banks = new List<Bank>();
        }

        public IReadOnlyList<Bank> GetBanks => _banks;

        public void TimeRewind(int monthCount)
        {
            if (monthCount < 0) throw new BanksException("Month count can't be less then 0");
            for (int i = 0; i < monthCount; i++)
            {
                foreach (IAccount account in _banks
                             .SelectMany(bank => bank
                                 .GetAccounts().Values))
                {
                    account?.AccountPayoff();
                }
            }
        }

        public Bank AddNewBank(Bank newBank)
        {
            if (_banks.Contains(newBank)) throw new BanksException("Bank is already exist");
            _banks.Add(newBank);
            return newBank;
        }

        public void Notification()
        {
        }
    }
}