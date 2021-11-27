using System;
using System.Collections.Generic;
using System.IO;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Repositories;
using BackupsExtra.Algorithms;
using BackupsExtra.Entities;
using BackupsExtra.Merge;
using BackupsExtra.Resotrer;
using BackupsExtra.Settings;
using NUnit.Framework;
using Serilog;

namespace BackupsExtra.Tests
{
    public class Test
    {
        private static readonly DirectoryInfo RuntimeDir = Directory.GetParent(Environment.CurrentDirectory);
        private static readonly string Pwd = RuntimeDir?.Parent?.Parent?.FullName + '/';
        private static readonly string BackupsPath = RuntimeDir?.Parent?.Parent?.Parent?.FullName + '/';
        private static readonly string sourcePath = "/home/iskander/Desktop/iskander-faggod/BackupsExtra/"; 


        public void AddFiles_UseClearAlgorithms_CheckForPoints()
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger();
            Logger.Logger.SetLogger(logger);
            
            string repositoryPath = Pwd + "Test_Repo";
            var repository = new FilesRepository(repositoryPath);
            var file1 = new FileDescription("file1.txt", sourcePath + "Source/");
            var file2 = new FileDescription("file2.txt", sourcePath + "Source/");
            var file3 = new FileDescription("file3.txt", sourcePath + "Source/");
            var files1 = new List<FileDescription> { file1, file2 };
            var files2 = new List<FileDescription> { file1, file3 };
            var compositeClearAlgo = new ClearByLimits(new List<IExtraAlgorithm>()
            {
                new ClearByCount(4),
                new ClearByDate(DateTime.Now.AddDays(-1)),
            });
            var newBackupExtraSettings = new BackupExtraSettings(new SoftMerge(), compositeClearAlgo);
            var extraBackupJob = new ExtraBackupJob(
                "BackupExtraJOBA",
                new SplitStorage(),
                newBackupExtraSettings);
            extraBackupJob.AddRestorePoint(files1);
            extraBackupJob.AddRestorePoint(files2);
            repository.CreateRepository(extraBackupJob, files1);
            repository.CreateRepository(extraBackupJob, files2);
            Assert.True(extraBackupJob.GetBackUpFiles().Count == 0);
            Assert.True(extraBackupJob.GetRestorePoints()[0].GetStorages().Count == 4);
        }
    }
}