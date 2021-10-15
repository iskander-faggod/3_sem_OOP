using System.Collections.Generic;
using System.IO;
using Backups.Tools;

namespace Backups.Entities
{
    public class ObjectJob
    {
        private readonly List<FileDescription> _files;

        public ObjectJob()
        {
            _files = new List<FileDescription>();
        }

        public void AddFile(FileDescription newFile)
        {
            if (newFile is null) throw new BackupsException("NewFile incorrect");
            _files.Add(newFile);
        }

        public void RemoveFile(FileDescription currentFile)
        {
            if (currentFile is null) throw new BackupsException("NewFile incorrect");
            _files.Remove(currentFile);
        }

        public IReadOnlyList<FileDescription> GetObjectJobFilesInfo() => _files;
    }
}