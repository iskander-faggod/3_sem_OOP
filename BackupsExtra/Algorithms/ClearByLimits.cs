using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Backups.Entities;
using Backups.Tools;
using BackupsExtra.Entities;

namespace BackupsExtra.Algorithms
{
    public class ClearByLimits : IExtraAlgorithm
    {
        private readonly List<IExtraAlgorithm> _extraAlgorithms;

        public ClearByLimits(List<IExtraAlgorithm> extraAlgorithms)
        {
            _extraAlgorithms = extraAlgorithms ?? throw new BackupsException("Invalid extraAlgorithms in ClearByLimits algorithm");
        }

        public IReadOnlyList<RestorePoint> FindPointsToClear(ExtraBackupJob extraBackupJob)
        {
            if (extraBackupJob is null)
                throw new BackupsException("Invalid ExtraBackupjob in ClearByLimits algorithm (FindPointsToClean)");
            return _extraAlgorithms.SelectMany(algorithm => algorithm.FindPointsToClear(extraBackupJob)).ToImmutableList();
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