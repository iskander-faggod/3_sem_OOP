using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Backups.Entities;
using Backups.Tools;
using BackupsExtra.Entities;

namespace BackupsExtra.Algorithms
{
    public class ClearByDate : IExtraAlgorithm
    {
        private readonly DateTime _timeUntillWeStoreFiles;

        public ClearByDate(DateTime timeUntillWeStoreFiles)
        {
            _timeUntillWeStoreFiles = timeUntillWeStoreFiles;
        }

        public IReadOnlyList<RestorePoint> FindPointsToClear(ExtraBackupJob extraBackupJob)
        {
            if (extraBackupJob is null) throw new BackupsException("Invalid ExtraBackupjob in ClearByDate algorithm (FindPointsToClean)");
            var restorePoints = extraBackupJob.GetRestorePoints().Where(restorePoint =>
                restorePoint.GetRestorePointCreationTime() < _timeUntillWeStoreFiles).ToImmutableList();
            return restorePoints;
        }

        public void ClearPoints(ExtraBackupJob extraBackupJob)
        {
            if (extraBackupJob is null) throw new BackupsException("Invalid ExtraBackupjob in ClearByDate algorithm (ClearPoints)");
            IReadOnlyList<RestorePoint> restorePoints = FindPointsToClear(extraBackupJob);
            foreach (RestorePoint point in restorePoints)
            {
                extraBackupJob.DeleteRestorePoint(point);
            }
        }
    }
}