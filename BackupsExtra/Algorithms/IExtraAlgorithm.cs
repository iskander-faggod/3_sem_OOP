using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Entities;
using BackupsExtra.Serializer;

namespace BackupsExtra.Algorithms
{
    public interface IExtraAlgorithm
    {
        IReadOnlyList<RestorePoint> FindPointsToClear(ExtraBackupJob extraBackupJob);
        void ClearPoints(ExtraBackupJob extraBackupJob);
        IClearLimitSnapShot ToSnapshot();
    }
}