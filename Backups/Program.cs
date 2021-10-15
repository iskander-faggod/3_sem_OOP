using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Algorithms;
using Backups.Entities;

namespace Backups
{
    internal class Program
    {
        public static string CreateZipFile(string restorePointPath, string fileName)
        {
            string zipFile = $"{restorePointPath}/{fileName}.zip";
            return zipFile;
        }

        private static void Main()
        {
            // Test - 2
            string zipeFilePath = "/home/iskander/Desktop/iskander-faggod/Backups/";
            var files = new List<(string, string)>
            {
                (zipeFilePath + "Source/", "file1.txt"),
                (zipeFilePath + "Source/", "file2.txt"),
            };
            /*using ZipArchive zipArchive = ZipFile.Open(zipeFilePath + "Zip/arc.zip", ZipArchiveMode.Create);
            foreach ((string, string) file in files)
                zipArchive.CreateEntryFromFile(file.Item1 + file.Item2, file.Item2);*/

            // Test -1
            string dir = Directory.CreateDirectory(zipeFilePath + $"Zip/{DateTime.Now:f}").FullName;
            foreach ((string, string) file in files)
            {
                string zipeFilePath2 = CreateZipFile(dir, file.Item2);
                using ZipArchive archive = ZipFile.Open(zipeFilePath2, ZipArchiveMode.Update);
                archive.CreateEntryFromFile(file.Item1 + file.Item2, file.Item2, CompressionLevel.Optimal);
            }
        }
    }
}