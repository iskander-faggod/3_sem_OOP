using System.Collections.Generic;
using Banks.Entities.Accounts.Interfaces;

namespace Banks.Entities.Interfaces
{
    public interface IBank
    {
        List<IAccount> _accounts;
        List<Client> _clients;
    }
}