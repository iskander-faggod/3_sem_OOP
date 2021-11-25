using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Repositories.Interfaces;
using Backups.Tools;

namespace Backups.Repositories
{
    public class FilesRepository : IRepository
    {
        private readonly string _path;

        public FilesRepository(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new BackupsException("FileRepository filepath is invalid");
            _path = path;
        }

        public void CreateRepository(BackUpJob backUpJob, List<FileDescription> files)
        {
            RestorePoint lastRestorePoint = backUpJob.GetLastRestorePoint();
            string dirPath = Path.Join(_path, "/", backUpJob.GetBackUpName(), "/", $"{DateTime.Now:F}");
            Directory.CreateDirectory(dirPath);
            foreach (Storage storage in lastRestorePoint.GetStorages())
            {
                byte[] bytes = storage.GetStorageBytesInfo();
                string name = storage.GetStorageName();
                File.WriteAllBytes(Path.Join(dirPath, "/", $"{name}.zip"), bytes);
            }
        }

        public string GetPath() => _path;
    }
}