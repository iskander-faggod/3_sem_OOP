﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Algorithms;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;
using Backups.Repositories;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            const string zipFilePath = "/home/iskander/Desktop/iskander-faggod/Backups";
            var repository = new FilesRepository(zipFilePath + "/Repository");
            var file1 = new FileDescription("main1.txt", zipFilePath + "/Source/");
            var file2 = new FileDescription("main2.txt", zipFilePath + "/Source/");

            var backupJob = new BackUpJob("TestBackUpJob", new SingleStorage());

            var files = new List<FileDescription> { file1, file2 };

            backupJob.AddPoint(files);
            repository.CreateRepository(backupJob, files);
        }
    }
}