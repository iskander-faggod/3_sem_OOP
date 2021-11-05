using Banks.Entities;

namespace Banks.Commands
{
    public interface IBankCommand
    {
        public void Execute(ClientContext context);

        public void Rollback();
    }
}