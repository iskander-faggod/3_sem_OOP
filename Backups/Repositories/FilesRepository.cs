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
            backUpJob.AddPoint(files);
            RestorePoint lastRestorePoint = backUpJob.GetLastRestorePoint();
            Directory.CreateDirectory(_path + "/" + backUpJob.GetBackUpName() + "/" + $"{DateTime.Now:F}");
            foreach (Storage storage in lastRestorePoint.GetStorages())
            {
                byte[] bytes = storage.GetStorageBytesInfo();
                string name = storage.GetStorageName();

                // TODO: Use platform specific path separator and string interpolation
                File.WriteAllBytes(_path + "/" + backUpJob.GetBackUpName() + "/" + $"{DateTime.Now:F}" + "/" + name + ".zip", bytes);
            }
        }
    }
}