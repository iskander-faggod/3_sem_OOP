using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Algorithms.Intrerfaces;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackUpJob
    {
        private readonly string _backUpName;
        private readonly List<FileDescription> _backUpFiles;
        private readonly List<RestorePoint> _restorePoints;
        private IAlgorithm _algorithm;

        public BackUpJob(string backUpName, IAlgorithm algorithm)
        {
            if (string.IsNullOrEmpty(backUpName)) throw new BackupsException("BackUpName incorrect");
            _backUpFiles = new List<FileDescription>();
            _restorePoints = new List<RestorePoint>();
            _algorithm = algorithm;
            _backUpName = backUpName;
        }

        public void SetAlgorithmType(IAlgorithm type)
        {
            _algorithm = type ?? throw new BackupsException("Type is incorrect");
        }

        public void AddPoint(List<FileDescription> filesToCopy)
        {
            if (filesToCopy is null) throw new BackupsException("FilesToCopy are null");
            _backUpFiles.AddRange(filesToCopy);
            var restorePoint = new RestorePoint(this, _algorithm);
            _restorePoints.Add(restorePoint);
            _algorithm.SaveFile(this, restorePoint);
            _backUpFiles.Clear();
        }

        public RestorePoint GetLastRestorePoint() => _restorePoints.LastOrDefault();

        public void RemovePoint(RestorePoint restorePoint)
        {
            if (restorePoint is null) throw new BackupsException("RestorePoint is invalid");
            _restorePoints.Remove(restorePoint);
        }

        public IReadOnlyList<FileDescription> GetBackUpFiles() => _backUpFiles;
        public IReadOnlyList<RestorePoint> GetRestorePoints() => _restorePoints;
        public int GetRestorePointsSize() => _restorePoints.Count;
        public string GetBackUpName() => _backUpName;
        public IAlgorithm GetBackUpAlgorithmType() => _algorithm;
    }
}