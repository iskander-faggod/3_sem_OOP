using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Backups.Algorithms;
using Backups.Entities;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Test1
    {
        string zipeFilePath = "/home/iskander/Desktop/iskander-faggod/Backups/";

        [Test]
        public void AddTwoFilesSingleStorage()
        {
            var file1 = new FileDescription("main1.txt", zipeFilePath + "Source/");
            var file2 = new FileDescription("main2.txt", zipeFilePath + "Source/");

            var backupJob = new BackUpJob(zipeFilePath + "Zip/", "Archive", new SingleStorage());

            var files = new List<FileDescription> {file1, file2};

            backupJob.AddPoint(files);

            var files2 = new List<FileDescription>() {file1};
            backupJob.AddPoint(files2);

            Assert.True(backupJob.GetRestorePoints().Count == 2);
            Assert.Contains(file1, (ICollection) backupJob.GetRestorePoints()[0].GetRestorePointFilesInfo());
            Assert.Contains(file2, (ICollection) backupJob.GetRestorePoints()[0].GetRestorePointFilesInfo());
            Assert.Contains(file1, (ICollection) backupJob.GetRestorePoints()[1].GetRestorePointFilesInfo());
            bool contains = backupJob.GetRestorePoints()[1].GetRestorePointFilesInfo().Contains(file2);
            Assert.False(contains);
            Assert.True(backupJob.GetRestorePoints()[0].GetRestorePointFilesInfo().Count == 2);
            Assert.True(backupJob.GetRestorePoints()[1].GetRestorePointFilesInfo().Count == 1);
        }
    }
}