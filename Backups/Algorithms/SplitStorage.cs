using System.IO;
using System.IO.Compression;
using Backups.Algorithms.Intrerfaces;
using Backups.Entities;

namespace Backups.Algorithms
{
    public class SplitStorage : IAlgorithm
    {
        public string CompressFileToZip(string pointPath, string fileName)
        {
            return $"{pointPath}/{fileName}";
        }

        public void SaveFile(string pointPath, BackUpJob backUpJob)
        {
            string compressFile = CompressFileToZip(pointPath, "CompressedFile");
            foreach (var file in backUpJob.GetBackUpFiles())
            {
            }
        }
    }
}