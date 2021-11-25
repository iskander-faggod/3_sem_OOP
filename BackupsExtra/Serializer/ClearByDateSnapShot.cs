using System;
using BackupsExtra.Algorithms;

namespace BackupsExtra.Serializer
{
    public class ClearByDateSnapShot : IClearLimitSnapShot
    {
        public DateTime TimeUntilWeStoreFiles { get; init; }

        public IExtraAlgorithm ToObject()
        {
            return new ClearByDate(TimeUntilWeStoreFiles);
        }
    }
}