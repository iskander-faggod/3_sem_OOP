using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.Merge
{
    public class SoftMerge : IMergeInstruction
    {
        public void AddPoint(ExtraBackupJob extraBackupJob, List<FileDescription> files)
        {
            extraBackupJob.AddPoint(files);
            RestorePoint newPoint = extraBackupJob.GetLastRestorePoint();
            var pointsToMerge = extraBackupJob.GetAlgorithm().FindPointsToClear(extraBackupJob).ToList();
            foreach (RestorePoint point in pointsToMerge)
            {
                extraBackupJob.MergeRestorePoints(point, newPoint);
            }
        }
    }
}