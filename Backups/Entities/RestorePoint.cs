using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Backups.Algorithms.Intrerfaces;
using Backups.Tools;

namespace Backups.Entities
{
    public class RestorePoint : IEquatable<RestorePoint>
    {
        private readonly DateTime _creationTime;
        private readonly BackUpJob _backUpJob;
        private readonly List<Storage> _storages;
        private readonly IAlgorithm _backupAlgorithm;

        public RestorePoint(BackUpJob backUpJob, IAlgorithm backupAlgorithm)
        {
            _backUpJob = backUpJob ?? throw new BackupsException("BackUpJob is invalid");
            _backupAlgorithm = backupAlgorithm;
            _storages = new List<Storage>();
            _creationTime = DateTime.Now;
        }

        public void AddStorage(byte[] bytes, string name)
        {
            if (bytes is null) throw new BackupsException("Bytes are null");
            if (string.IsNullOrEmpty(name)) throw new BackupsException("Name is invalid");
            _storages.Add(new Storage(bytes, name));
        }

        public DateTime GetRestorePointCreationTime() => _creationTime;
        public BackUpJob GetBackUpJobInfo() => _backUpJob;
        public IAlgorithm GetBackupAlgorithm() => _backupAlgorithm;
        public IReadOnlyList<Storage> GetStorages() => _storages;

        public override string ToString()
        {
            return $"RestorePoint | {_creationTime:O}";
        }

        public bool Equals(RestorePoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _creationTime.Equals(other._creationTime) && Equals(_backUpJob, other._backUpJob) && Equals(_storages, other._storages) && Equals(_backupAlgorithm, other._backupAlgorithm);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RestorePoint)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_creationTime, _backUpJob, _storages, _backupAlgorithm);
        }
    }
}