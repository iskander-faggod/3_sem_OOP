using Banks.Entities;
using Banks.Entities.ClientModel;

namespace Banks.Commands
{
    public interface IBankCommand
    {
        public void Execute(ClientContext context);

        public void Rollback();
    }
}