using System;
using System.Collections.Generic;
using System.IO;
using Backups.Tools;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private List<FileDescription> _filesToCopy;
        private ObjectJob _objectJob;
        private DateTime _creationTime;
        private string _restorePointPath;

        public RestorePoint(string restorePointPath, List<FileDescription> filesToCopy)
        {
            if (string.IsNullOrEmpty(restorePointPath)) throw new BackupsException("Point path incorrect");
            _restorePointPath = restorePointPath;
            _filesToCopy = filesToCopy;
            _creationTime = DateTime.Now;
        }

        public RestorePoint(string restorePointPath, ObjectJob objectJob)
        {
            if (string.IsNullOrEmpty(restorePointPath)) throw new BackupsException("Point path incorrect");
            _restorePointPath = restorePointPath;
            _objectJob = objectJob;
            _creationTime = DateTime.Now;
        }

        public IReadOnlyList<FileDescription> GetRestorePointFilesInfo() => _filesToCopy;
        public DateTime GetRestorePointCreationTime() => _creationTime;
        public string GetRestorePointPath() => _restorePointPath;
    }
}