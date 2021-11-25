using System;
using System.Collections.Generic;
using Backups.Repositories;
using BackupsExtra.Entities;

namespace BackupsExtra.Serializer
{
    public class SnapShot
    {
        public List<string> RepositoryPaths { get; set; }
        public List<ExtraBackupJob> ExtraBackupJobs { get; set; }
    }
}