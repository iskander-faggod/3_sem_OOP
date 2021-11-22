using System.Linq;
using Backups.Algorithms;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;
using Backups.Tools;
using BackupsExtra.Algorithms;
using BackupsExtra.Merge;
using BackupsExtra.Settings;

namespace BackupsExtra.Entities
{
    public class ExtraBackupJob : BackUpJob
    {
        private readonly BackupExtraSettings _settings;
        private IExtraAlgorithm _algorithm;
        private IMergeInstruction _instruction;

        public ExtraBackupJob(string backUpName, IAlgorithm algorithm, BackupExtraSettings settings)
            : base(backUpName, algorithm)
        {
            if (settings is null) throw new BackupsException("ExtraBackup settings is null");
            _settings = settings;
        }

        public void AddRestorePoint()
        {
        }

        public void DeleteRestorePoint(RestorePoint restorePointToDelete)
        {
            if (restorePointToDelete is null) throw new BackupsException("Invalid restorePoint in DeleteRestorePoint");
            RemovePoint(restorePointToDelete);
        }

        public void DeleteAllRestorePoints()
        {
            var restorePointsToDelete = _algorithm.FindPointsToClear(this).ToList();
            foreach (RestorePoint restorePoint in restorePointsToDelete)
            {
                RemovePoint(restorePoint);
            }
        }

        public void MergeRestorePoints(RestorePoint removePoint, RestorePoint mergePoint)
        {
            if (removePoint.GetBackupAlgorithm() is SingleStorage)
            {
                RemovePoint(removePoint);
            }
            else
            {
                var pointsWithoutConflicts = removePoint
                    .GetStorages()
                    .Where(storage => !mergePoint.GetStorages().Contains(storage)).ToList();
                foreach (Storage storage in pointsWithoutConflicts)
                {
                    mergePoint.AddStorage(storage.GetStorageBytesInfo(), storage.GetStorageName());
                }

                RemovePoint(removePoint);
            }
        }

        public IExtraAlgorithm GetAlgorithm() => _settings.GetExtraAlgorithm();
        public IMergeInstruction GetInstruction() => _settings.GetMergeInstruction();

        public void SetAlgorithm(IExtraAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public void SetInstruction(IMergeInstruction instruction)
        {
            _instruction = instruction;
        }
    }
}