using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Repositories;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Test1
    {
        static string zipFilePath = "/home/iskander/Desktop/iskander-faggod/Backups/"; 
        FilesRepository repository = new FilesRepository(zipFilePath + "/Repository");
        [Test] [Ignore("FileSystem doesn't work in GitHub CI")]
        public void AddTwoFilesSingleStorage()
        {
            var file1 = new FileDescription("main1.txt", zipFilePath + "Source/");
            var file2 = new FileDescription("main2.txt", zipFilePath + "Source/");

            var backupJob = new BackUpJob("TestBackUpJob", new SplitStorage());

            var files = new List<FileDescription> {file1, file2};

            backupJob.AddPoint(files);
            repository.CreateRepository(backupJob, files);


            var files2 = new List<FileDescription>() {file1};
            backupJob.AddPoint(files2);
            
            Assert.True((backupJob.GetRestorePoints()[0].GetStorages().Count + backupJob.GetRestorePoints()[1].GetStorages().Count) == 3 );
            Assert.True(backupJob.GetRestorePoints().Count == 2);
        }
    }
}