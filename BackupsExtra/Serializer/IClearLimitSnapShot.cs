using BackupsExtra.Algorithms;

namespace BackupsExtra.Serializer
{
    public interface IClearLimitSnapShot
    {
        public IExtraAlgorithm ToObject();
    }
}