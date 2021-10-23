using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;
using Backups.Tools;

namespace Backups.Algorithms
{
    public class SplitStorage : IAlgorithm
    {
        public string GetFileZipPath(string pointPath, string fileName) => Path.Join(pointPath, $"{fileName}.zip");

        public void SaveFile(BackUpJob backUpJob, RestorePoint restorePoint)
        {
            if (backUpJob is null) throw new BackupsException("BackUpJob is incorrect");
            if (restorePoint is null) throw new BackupsException("RestorePoint is incorrect");
            string path = Path.Join(backUpJob.GetBackUpName(), "_", Guid.NewGuid().ToString("D").GetHashCode().ToString(), "|", DateTime.Now.ToString("h:mm:ss"));
            if (File.Exists(path)) throw new BackupsException("File with this path was created");
            string dir = Directory
                .CreateDirectory(path).FullName;
            foreach (FileDescription file in backUpJob.GetBackUpFiles())
            {
                string fileName = $"{file.GetFileName()}|{DateTime.Now:h:mm:ss}";
                string zipFilePath = GetFileZipPath(dir, fileName);
                ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create);
                archive.CreateEntryFromFile(file.GetFileFullPath(), file.GetFileName());

                archive.Dispose();
                byte[] archiveBytes = File.ReadAllBytes(zipFilePath);
                restorePoint.AddStorage(archiveBytes, fileName);
            }
        }
    }
}