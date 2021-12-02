using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.Merge
{
    public class HardMerge : IMergeInstruction
    {
        public void AddPoint(ExtraBackupJob extraBackupJob, List<FileDescription> files)
        {
            extraBackupJob.AddPoint(files);
            extraBackupJob.DeleteAllRestorePoints();
        }
    }
}