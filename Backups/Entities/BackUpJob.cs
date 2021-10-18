using System.Collections.Generic;
using Backups.Algorithms.Intrerfaces;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackUpJob
    {
        private readonly string _backUpName;
        private readonly string _backUpPath;
        private IAlgorithm _algorithm;
        private List<FileDescription> _backUpFiles;
        private List<RestorePoint> _restorePoints;

        public BackUpJob(string backUpPath, string backUpName, IAlgorithm algorithm)
        {
            if (string.IsNullOrEmpty(backUpPath)) throw new BackupsException("BackUpPath incorrect");
            if (string.IsNullOrEmpty(backUpName)) throw new BackupsException("BackUpName incorrect");
            _backUpFiles = new List<FileDescription>();
            _restorePoints = new List<RestorePoint>();
            _algorithm = algorithm ??
                             throw new BackupsException("Algorithm Type incorrect");
            _backUpPath = backUpPath;
            _backUpName = backUpName;
        }

        public void SetAlgorithmType(IAlgorithm type)
        {
            _algorithm = type ?? throw new BackupsException("Type is incorrect");
        }

        public void AddPoint(List<FileDescription> filesToCopy)
        {
            _backUpFiles.AddRange(filesToCopy);
            var restorePoint = new RestorePoint(_backUpPath, new List<FileDescription>(filesToCopy));
            _restorePoints.Add(restorePoint);
            _algorithm.SaveFile(_backUpPath, this);
            _backUpFiles.Clear();
        }

        public void RemovePoint(RestorePoint restorePoint)
        {
            if (restorePoint is null) throw new BackupsException("RestorePoint is invalid");
            _restorePoints.Remove(restorePoint);
        }

        public IReadOnlyList<FileDescription> GetBackUpFiles() => _backUpFiles;
        public IReadOnlyList<RestorePoint> GetRestorePoints() => _restorePoints;
        public int GetRestorePointsSize() => _restorePoints.Count + 1;
        public string GetBackUpPath() => _backUpPath;
        public string GetBackUpName() => _backUpName;
        public IAlgorithm GetBackUpAlgorithmType() => _algorithm;
    }
}