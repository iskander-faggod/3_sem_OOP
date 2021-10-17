using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Algorithms;
using Backups.Algorithms.Intrerfaces;
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
            string zipeFilePath = "/home/iskander/Desktop/iskander-faggod/Backups";

            var file1 = new FileDescription("main1.txt", zipeFilePath + "/Source/");
            var file2 = new FileDescription("main2.txt", zipeFilePath + "/Source/");
            var file3 = new FileDescription("main3.txt", zipeFilePath + "/Source/");
            var objectJob = new ObjectJob();
            objectJob.AddFile(file1);
            objectJob.AddFile(file2);
            objectJob.AddFile(file3);

            var backupJob = new BackUpJob(zipeFilePath + "/Zip/", "Single", new SingleStorage());
            backupJob.AddPoint(objectJob);
        }
    }
}