using System.Collections.Generic;
using System.Linq;
using Backups.Algorithms;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;
using Backups.Tools;
using BackupsExtra.Algorithms;
using BackupsExtra.Logger;
using BackupsExtra.Merge;
using BackupsExtra.Settings;

namespace BackupsExtra.Entities
{
    public class ExtraBackupJob : BackUpJob
    {
        private readonly BackupExtraSettings _settings;
        private IExtraAlgorithm _extraAlgorithm;
        private IMergeInstruction _mergeInstruction;

        public ExtraBackupJob(string backUpName, IAlgorithm algorithm, BackupExtraSettings settings)
            : base(backUpName, algorithm)
        {
            _settings = settings ?? throw new BackupsException("ExtraBackup settings is null");
            _extraAlgorithm = settings.GetExtraAlgorithm();
            _mergeInstruction = settings.GetMergeInstruction();
        }

        public void AddRestorePoint(List<FileDescription> files)
        {
            _mergeInstruction.AddPoint(extraBackupJob: this, files);
            foreach (FileDescription fileDescription in files)
            {
                Logger.Logger.Log($"{fileDescription.GetFileName()} was added in restore point");
            }
        }

        public void DeleteRestorePoint(RestorePoint restorePointToDelete)
        {
            if (restorePointToDelete is null) throw new BackupsException("Invalid restorePoint in DeleteRestorePoint");
            RemovePoint(restorePointToDelete);
            Logger.Logger.Log($"{restorePointToDelete} was removed");
        }

        public void DeleteAllRestorePoints()
        {
            var restorePointsToDelete = _extraAlgorithm.FindPointsToClear(this).ToList();
            foreach (RestorePoint restorePoint in restorePointsToDelete)
            {
                DeleteRestorePoint(restorePoint);
            }

            Logger.Logger.Log("All points was deleted");
        }

        public void MergeRestorePoints(RestorePoint removePoint, RestorePoint mergePoint)
        {
            if (removePoint.GetBackupAlgorithm() is SingleStorage)
            {
                RemovePoint(removePoint);
            }

            var pointsWithoutConflicts = removePoint
                .GetStorages()
                .Where(storage => !mergePoint.GetStorages().Contains(storage))
                .ToList();
            foreach (Storage storage in pointsWithoutConflicts)
            {
                mergePoint.AddStorage(storage.GetStorageBytesInfo(), storage.GetStorageName());
            }

            RemovePoint(removePoint);
            Logger.Logger.Log($"{removePoint} && {mergePoint} was merged");
        }

        public IExtraAlgorithm GetAlgorithm() => _settings.GetExtraAlgorithm();
        public IMergeInstruction GetInstruction() => _settings.GetMergeInstruction();

        public void SetAlgorithm(IExtraAlgorithm algorithm)
        {
            _extraAlgorithm = algorithm;
        }

        public void SetInstruction(IMergeInstruction instruction)
        {
            _mergeInstruction = instruction;
        }
    }
}