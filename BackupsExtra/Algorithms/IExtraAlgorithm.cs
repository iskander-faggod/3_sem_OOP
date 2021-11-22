using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.Algorithms
{
    public interface IExtraAlgorithm
    {
        IReadOnlyList<RestorePoint> FindPointsToClear(ExtraBackupJob extraBackupJob);
        void ClearPoints(ExtraBackupJob extraBackupJob);
    }
}