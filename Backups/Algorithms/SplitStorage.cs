using System;
using System.IO;
using System.IO.Compression;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;
using Backups.Tools;

namespace Backups.Algorithms
{
    public class SplitStorage : IAlgorithm
    {
        public string CompressFileToZip(string pointPath, string fileName)
        {
            return $"{pointPath}/{fileName}.zip";
        }

        public void SaveFile(string pointPath, BackUpJob backUpJob)
        {
            if (backUpJob is null) throw new BackupsException("BackUpJob is incorrect");
            foreach (FileDescription file in backUpJob.GetBackUpFiles())
            {
                string zipFile = CompressFileToZip(pointPath, $"{backUpJob.GetBackUpName()}|{DateTime.Now:f}");
                using ZipArchive archive = ZipFile.Open(zipFile, ZipArchiveMode.Create);
                archive.CreateEntryFromFile(file.GetFileFullPath(), file.GetFileName());
                archive.Dispose();
            }
        }
    }
}