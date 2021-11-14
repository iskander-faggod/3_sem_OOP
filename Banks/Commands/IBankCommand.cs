using Banks.Entities;
using Banks.Entities.ClientModel;

namespace Banks.Commands
{
    public interface IBankCommand
    {
         void Execute(ClientContext context);

         void Rollback();
    }
}