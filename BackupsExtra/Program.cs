using System;
using System.Collections.Generic;
using System.IO;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Repositories;
using BackupsExtra.Algorithms;
using BackupsExtra.Entities;
using BackupsExtra.Merge;
using BackupsExtra.Serializer;
using BackupsExtra.Settings;
using Newtonsoft.Json;
using Serilog;

namespace BackupsExtra
{
    internal class Program
    {
        private static readonly DirectoryInfo RuntimeDir = Directory.GetParent(Environment.CurrentDirectory);
        private static readonly string Pwd = RuntimeDir?.Parent?.Parent?.FullName + '/';
        private static readonly string BackupsPath = RuntimeDir?.Parent?.Parent?.Parent?.FullName + '/';

        private static void Main()
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger();
            Logger.Logger.SetLogger(logger);

            logger.Information("test");
            Logger.Logger.Log("test3");
            var repositoryPath = Pwd + "1_Repository";
            var repository = new FilesRepository(repositoryPath);
            var file1 = new FileDescription("file1.txt", Pwd + "Source/");
            var file2 = new FileDescription("file2.txt", Pwd + "Source/");
            var file3 = new FileDescription("file3.txt", Pwd + "Source/");
            var files_1 = new List<FileDescription> { file1, file2 };
            var files_2 = new List<FileDescription> { file1, file3 };
            var compositeClearAlgo = new ClearByLimits(new List<IExtraAlgorithm>()
            {
                new ClearByCount(4),
                new ClearByDate(DateTime.Now.AddDays(-1)),
            });
            var backupExtraSettings = new BackupExtraSettings(new SoftMerge(), compositeClearAlgo);
            var serializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
            };
            File.WriteAllText(
                "state.json",
                JsonConvert.SerializeObject(
                    new BackupSettingsSnapShot
                    {
                        MergeInstruction = backupExtraSettings.GetMergeInstruction(),
                        ClearLimitSnapShot = backupExtraSettings.GetExtraAlgorithm().ToSnapshot(),
                    }, serializerSettings));
            var settingsSnapshot = JsonConvert.DeserializeObject<BackupSettingsSnapShot>(File.ReadAllText("state.json"), serializerSettings);
            var newBackupExtraSettings = settingsSnapshot.ToObject();
            var extraBackupJob = new ExtraBackupJob(
                "BackupExtraJOBA",
                new SingleStorage(),
                newBackupExtraSettings);
            extraBackupJob.AddRestorePoint(files_1);
            extraBackupJob.AddRestorePoint(files_2);
            repository.CreateRepository(extraBackupJob, files_1);
            repository.CreateRepository(extraBackupJob, files_2);
        }
    }
}