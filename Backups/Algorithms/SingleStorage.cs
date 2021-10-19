using System;
using System.IO;
using System.IO.Compression;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;
using Backups.Tools;

namespace Backups.Algorithms
{
    public class SingleStorage : IAlgorithm
    {
        public string GetFileZipPath(string pointPath, string fileName) => Path.Join(pointPath, $"{fileName}.zip");

        public void SaveFile(BackUpJob backUpJob, RestorePoint restorePoint)
        {
            if (backUpJob is null) throw new BackupsException("BackUpJob is null");
            string storageName = $"{Math.Abs(Guid.NewGuid().ToString("D").GetHashCode())}|{DateTime.Now:h:mm:ss}";
            string zipFilePath = GetFileZipPath(backUpJob.GetBackUpName(), storageName);
            ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create);
            foreach (FileDescription file in backUpJob.GetBackUpFiles())
                zipArchive.CreateEntryFromFile(file.GetFileFullPath(), file.GetFileName());
            zipArchive.Dispose();
            byte[] archiveBytes = File.ReadAllBytes(zipFilePath);
            restorePoint.AddStorage(archiveBytes, zipFilePath);
        }
    }
}