using System;
using System.Collections.Immutable;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;

namespace Banks.Entities
{
    public class ClientContext
    {
        private readonly ImmutableDictionary<Guid, IAccount> _accounts;
        private readonly Client _client;
        public ClientContext(ImmutableDictionary<Guid, IAccount> accounts, Client client)
        {
            _accounts = accounts;
            _client = client;
        }

        public ImmutableDictionary<Guid, IAccount> GetAccounts() => _accounts;
        public Client GetClient() => _client;
    }
}