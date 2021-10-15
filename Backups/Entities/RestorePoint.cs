using System;
using System.Collections.Generic;
using System.IO;
using Backups.Tools;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private List<FileDescription> _filesToCopy;
        private DateTime _creationTime;
        private string _restorePointPath;

        public RestorePoint(DateTime creationTime, string restorePointPath)
        {
            if (string.IsNullOrEmpty(restorePointPath)) throw new BackupsException("Point path incorrect");
            _restorePointPath = restorePointPath;
            _creationTime = creationTime;
            _filesToCopy = new List<FileDescription>();
        }

        public IReadOnlyList<FileDescription> GetRestorePointFilesInfo() => _filesToCopy;
        public DateTime GetRestorePointCreationTime() => _creationTime;
        public string GetRestorePointPath() => _restorePointPath;
    }
}