using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.Merge
{
    public interface IMergeInstruction
    {
        void AddPoint(ExtraBackupJob extraBackupJob, List<FileDescription> files);
    }
}