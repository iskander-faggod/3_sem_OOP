using System.Collections.Generic;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;

namespace BackupsExtra.Serializer
{
    public class BackupJobSnapShot
    {
        public string BackUpName { get; set; }
        public List<string> BackUpFilesFullPaths { get; set; }
        public List<RestorePoint> RestorePoints { get; set; }
        public IAlgorithm Algorithm { get; set; }
    }
}