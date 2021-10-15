using System.Collections.Generic;
using Backups.Algorithms.Intrerfaces;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackUpJob
    {
        private readonly string _backUpName;
        private readonly string _backUpPath;
        private IAlgorithm _algorithmType;
        private List<FileDescription> _backUpFiles;
        private List<RestorePoint> _restorePoints;

        public BackUpJob(string backUpPath, string backUpName, IAlgorithm algorithmType)
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

        public void SetALgorithmType(IAlgorithm type)
        {
            if (type is null) throw new BackupsException("Type is incorrect");
            _algorithmType = type;
        }

        public void AddPoint(List<FileDescription> files)
        {
            _backUpFiles.AddRange(files);
            var restorePoint = new RestorePoint(_backUpPath, files);
            _algorithmType.SaveFile(_backUpPath, this);
            _restorePoints.Add(restorePoint);
        }

        public IReadOnlyList<FileDescription> GetBackUpFiles() => _backUpFiles;
        public IReadOnlyList<RestorePoint> GetRestorePoints() => _restorePoints;
        public string GetBackUpPath() => _backUpPath;
        public string GetBackUpName() => _backUpName;
        public IAlgorithm GetBackUpAlgorithmType() => _algorithmType;
    }
}