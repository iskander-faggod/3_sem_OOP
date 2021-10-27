namespace Banks.Entities.Interfaces
{
    public interface IMainBank : IBank
    {
        void RegisterBank();
        void CreateBank();
        void Notification();
    }
}