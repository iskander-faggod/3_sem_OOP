using System;
using System.IO;
using System.IO.Compression;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;

namespace Backups.Algorithms
{
    public class SingleStorage : IAlgorithm
    {
        public string CompressFileToZip(string pointPath, string fileName)
        {
            return $"{pointPath}/{fileName}";
        }

        public void SaveFile(string pointPath, BackUpJob backUpJob)
        {
            string pathToZip = CompressFileToZip(pointPath, backUpJob.GetBackUpName());
            try
            {
                ZipFile.CreateFromDirectory(pointPath, pathToZip);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var zipToOpen = new FileStream(pathToZip, FileMode.Open);
            var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);

            foreach (FileDescription archEntry in backUpJob.GetBackUpFiles())
            {
                archive.CreateEntryFromFile(archEntry.GetFilePath(), archEntry.GetFileName());
            }
        }
    }
}