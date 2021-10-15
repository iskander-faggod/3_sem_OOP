using System;
using System.IO;
using System.IO.Compression;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;

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