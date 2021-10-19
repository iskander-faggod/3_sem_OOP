using System;
using Backups.Entities;

namespace Backups.Algorithms.Intrerfaces
{
    public interface IAlgorithm
    {
        string GetFileZipPath(string pointPath, string fileName);
        public void SaveFile(BackUpJob backUpJob, RestorePoint restorePoint);
    }
}