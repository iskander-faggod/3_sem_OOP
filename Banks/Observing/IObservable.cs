namespace Banks.Observing
{
    public interface IObservable
    {
        void Notify();
        void AddObserver(IObserver observer);
    }
}