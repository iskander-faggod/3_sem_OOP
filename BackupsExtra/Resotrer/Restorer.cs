using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Tools;

namespace BackupsExtra.Resotrer
{
    public class Restorer
    {
        private readonly RestorePoint _point;

        public Restorer(RestorePoint point, List<FileDescription> files)
        {
            _point = point ?? throw new BackupsException("Invalid point");
        }

        public void Restore()
        {
            string fullName = Directory
                .CreateDirectory($" RestoredPoint|{DateTime.Now:h:mm:ss}")
                .FullName;
            try
            {
                if (_point.GetBackupAlgorithm() is not SingleStorage &&
                   _point.GetBackupAlgorithm() is not SplitStorage) return;

                Directory.CreateDirectory(fullName);
                foreach (Storage storage in _point.GetStorages())
                {
                    File.WriteAllBytes(fullName + "Temp", storage.GetStorageBytesInfo());
                    ZipArchive zip = ZipFile.Open(fullName + "Temp", ZipArchiveMode.Read);
                    zip.ExtractToDirectory(fullName);
                }

                Logger.Logger.Log($"{fullName} was restored");
            }
            catch (Exception error)
            {
                Logger.Logger.Log($"Invalid operation | {error}");
            }
        }
    }
}