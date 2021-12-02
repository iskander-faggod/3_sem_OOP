using System.Collections.Generic;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;
using BackupsExtra.Entities;
using BackupsExtra.Settings;

namespace BackupsExtra.Serializer
{
    public class BackupJobSnapShot
    {
        public string BackUpName { get; set; }
        public BackupExtraSettings Settings { get; set; }
        public IAlgorithm Algorithm { get; set; }

        public ExtraBackupJob ToObject()
        {
            return new ExtraBackupJob(BackUpName, Algorithm, Settings);
        }
    }
}