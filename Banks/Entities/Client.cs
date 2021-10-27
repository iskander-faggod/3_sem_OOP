using Banks.Entities.Accounts.Interfaces;
using Banks.Tools;

namespace Banks.Entities
{
    public class Client
    {
        private string _name;
        private string _surname;
        private string? _address;
        private int? _passportId;
        private IAccount _accountType;

        public Client(string name, string surname, string? address, int? passportId, IAccount accountType)
        {
            if (string.IsNullOrEmpty(name)) throw new BanksException($"Invalid name - {name}");
            if (string.IsNullOrEmpty(surname)) throw new BanksException($"Invalid surname - {surname}");
            if (string.IsNullOrEmpty(address)) throw new BanksException($"Invalid address - {address}");
            if (passportId < 1000000000) throw new BanksException($"Invalid passport id  - {passportId}");
            _name = name;
            _surname = surname;
            _address = address;
            _passportId = passportId;
            _accountType = accountType ?? throw new BanksException("Invalid account type");
        }
    }
}