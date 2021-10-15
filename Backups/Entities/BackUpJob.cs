using System.Collections.Generic;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackUpJob
    {
        private readonly string _backUpName;
        private readonly string _algorithmType;
        private readonly string _backUpPath;
        private List<FileDescription> _backUpFiles;
        private List<RestorePoint> _restorePoints;

        public BackUpJob(string algorithmType, string backUpPath, string backUpName)
        {
            if (algorithmType is null) throw new BackupsException("Algorithm Type incorrect");
            if (string.IsNullOrEmpty(backUpPath)) throw new BackupsException("BackUpPath incorrect");
            if (string.IsNullOrEmpty(backUpName)) throw new BackupsException("BackUpName incorrect");
            _backUpFiles = new List<FileDescription>();
            _restorePoints = new List<RestorePoint>();
            _algorithmType = algorithmType;
            _backUpPath = backUpPath;
            _backUpName = backUpName;
        }

        public IReadOnlyList<FileDescription> GetBackUpFiles() => _backUpFiles;
        public IReadOnlyList<RestorePoint> GetRestorePoints() => _restorePoints;
        public string GetBackUpPath() => _backUpPath;
        public string GetBackUpName() => _backUpName;
        public string GetBackUpAlgorithmType() => _algorithmType;
    }
}