using System.Collections.Generic;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;

namespace Banks.Entities.Creator
{
    public abstract class ABank
    {
        private List<IAccount> _accounts;
        private List<Client> _clients;

        protected ABank()
        {
            _accounts = new List<IAccount>();
            _clients = new List<Client>();
        }

        public void ChangePercent()
        {
        }

        public void ChangeTransactionLimit()
        {
        }

        public void ChangesNotification()
        {
        }

        public void CancelTransaction()
        {
        }
    }
}