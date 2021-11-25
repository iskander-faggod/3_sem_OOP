using BackupsExtra.Algorithms;

namespace BackupsExtra.Serializer
{
    public class ClearByCountSnapShot : IClearLimitSnapShot
    {
        public int CountRestorePoints { get; init; }

        public IExtraAlgorithm ToObject()
        {
            return new ClearByCount(CountRestorePoints);
        }
    }
}