using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Algorithms;

namespace BackupsExtra.Serializer
{
    public class ClearByLimitsSnapShot : IClearLimitSnapShot
    {
        public List<IClearLimitSnapShot> Algorithms { get; init; }

        public IExtraAlgorithm ToObject()
        {
            return new ClearByLimits(Algorithms.Select(x => x.ToObject()).ToList());
        }
    }
}