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
        public string CompressFileToZip(string pointPath, string fileName)
        {
            return $"{pointPath}/{fileName}.zip";
        }

        public void SaveFile(string pointPath, BackUpJob backUpJob)
        {
            if (string.IsNullOrEmpty(pointPath)) throw new BackupsException("RestorePoint path is invalid");
            if (backUpJob is null) throw new BackupsException("BackUpJob is null");

            string zipFilePath = CompressFileToZip(pointPath, $"{backUpJob.GetBackUpName()}|{DateTime.Now:O}");
            using ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create);
            foreach (FileDescription file in backUpJob.GetBackUpFiles())
                zipArchive.CreateEntryFromFile(file.GetFileFullPath(), file.GetFileName());
            zipArchive.Dispose();
        }
    }
}