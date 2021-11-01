using Banks.Entities.AccountsModel;
using Banks.Tools;

namespace Banks.Entities
{
    public class ClientBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private string _passportId;

        public Client Build()
        {
            if (!string.IsNullOrWhiteSpace(_name))
                throw new BanksException($"Required field {nameof(_name)} is missing");
            if (!string.IsNullOrWhiteSpace(_surname))
                throw new BanksException($"Required field {nameof(_surname)} is missing");
            return new Client(_name, _surname, _address, _passportId);
        }

        public ClientBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BanksException($"Field {nameof(name)} is required");
            }

            _name = name;
            return this;
        }

        public ClientBuilder SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new BanksException($"Field {nameof(surname)} is required");
            }

            _surname = surname;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new BanksException($"Field {nameof(address)} is required");
            }

            _address = address;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            if (string.IsNullOrWhiteSpace(passport))
            {
                throw new BanksException($"Field {nameof(passport)} is required");
            }

            _passportId = passport;
            return this;
        }
    }
}