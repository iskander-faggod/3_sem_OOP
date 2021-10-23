using System.IO;
using Backups.Tools;

namespace Backups.Entities
{
    public class FileDescription
    {
        private readonly string _fileName;
        private readonly string _filePath;

        public FileDescription(string fileName, string filePath)
        {
            if (string.IsNullOrEmpty(fileName)) throw new BackupsException("FileName incorrect");
            if (string.IsNullOrEmpty(filePath)) throw new BackupsException("FilePath incorrect");
            /*if (File.Exists($"/home/iskander/Desktop/iskander-faggod/Backups/Source/{filePath}"))
                throw new BackupsException("File with this FileName is already exists");*/

            _fileName = fileName;
            _filePath = filePath;
        }

        public string GetFileName() => _fileName;
        public string GetFilePath() => _filePath;
        public string GetFileFullPath() => _filePath + _fileName;
    }
}