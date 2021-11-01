using System.Collections.Generic;
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