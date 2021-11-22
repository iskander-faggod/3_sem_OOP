using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Backups.Entities;
using Backups.Tools;
using BackupsExtra.Entities;

namespace BackupsExtra.Algorithms
{
    public class ClearByCount : IExtraAlgorithm
    {
        private readonly int _countRestorePoints;

        public ClearByCount(int countRestorePoints)
        {
            if (countRestorePoints < 0) throw new BackupsException("Invalid count of restorePoints in CLearByCount");
            _countRestorePoints = countRestorePoints;
        }

        public IReadOnlyList<RestorePoint> FindPointsToClear(ExtraBackupJob extraBackupJob)
        {
            if (extraBackupJob is null)
                throw new BackupsException("Invalid ExtraBackupjob in ClearByCount algorithm (FindPointsToClean)");
            var restorePoints = extraBackupJob.GetRestorePoints().Take(_countRestorePoints).ToImmutableList();
            return restorePoints;
        }

        public void ClearPoints(ExtraBackupJob extraBackupJob)
        {
            if (extraBackupJob is null)
                throw new BackupsException("Invalid ExtraBackupjob in ClearByCount algorithm (ClearPoints)");
            IReadOnlyList<RestorePoint> restorePoints = FindPointsToClear(extraBackupJob);
            foreach (RestorePoint point in restorePoints)
            {
                extraBackupJob.DeleteRestorePoint(point);
            }
        }
    }
}