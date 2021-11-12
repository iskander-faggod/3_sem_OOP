using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;
using Banks.Tools;

namespace Banks.Entities
{
    public class MainBank : IEquatable<MainBank>
    {
        private readonly List<Bank> _banks;
        private readonly List<Client> _clients;

        public MainBank()
        {
            _clients = new List<Client>();
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
                    account?.AccrualOfCommission();
                }
            }
        }

        public Bank GetBankByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new BanksException("Invalid bank name");
            return _banks.FirstOrDefault(bank => bank.GetBankName() == name);
        }

        public Client GetClientById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new BanksException("Invalid client id");
            return _clients.FirstOrDefault(client => client.PassportId == id);
        }

        public Bank AddNewBank(Bank newBank)
        {
            if (_banks.Contains(newBank)) throw new BanksException("Bank is already exist");
            _banks.Add(newBank);
            return newBank;
        }

        public Client AddNewClient(Client client)
        {
            if (_clients.Contains(client)) throw new BanksException("Client is already exist");
            _clients.Add(client);
            return client;
        }

        public bool Equals(MainBank other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_banks, other._banks) && Equals(_clients, other._clients);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((MainBank)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_banks, _clients);
        }
    }
}