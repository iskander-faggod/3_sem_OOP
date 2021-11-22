using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.Merge
{
    public interface IMergeInstruction
    {
        public void AddPoint(ExtraBackupJob extraBackupJob, List<FileDescription> files);
    }
}