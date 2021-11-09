using System;
using Banks.Observing;
using Banks.Tools;

namespace Banks.Entities.ClientModel
{
    public class Client : IEquatable<Client>, IObserver
    {
        public Client(string name, string surname, string address = null, string passportId = null)
        {
            if (string.IsNullOrEmpty(name)) throw new BanksException($"Invalid name - {name}");
            if (string.IsNullOrEmpty(surname)) throw new BanksException($"Invalid surname - {surname}");
            if (address == string.Empty) throw new BanksException("Invalid address");
            Name = name;
            Surname = surname;
            Address = address;
            PassportId = passportId;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Address { get; private set; }
        public string PassportId { get; private set; }
        public bool IsSuspicious => string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(PassportId);
        public bool Notified { get; private set; }
        public void SetPassport(string passportId)
        {
            if (string.IsNullOrWhiteSpace(passportId)) throw new BanksException("Invalid passport id");
            PassportId = passportId;
        }

        public void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new BanksException("Invalid address id");
            Address = address;
        }

        public bool Equals(Client other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Surname == other.Surname && Address == other.Address &&
                   PassportId == other.PassportId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Client)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Address, PassportId);
        }

        public void Modify(IObservable subject)
        {
            Notified = true;
        }
    }
}