using System;
using Backups.Entities;

namespace Backups.Algorithms.Intrerfaces
{
    public interface IAlgorithm
    {
        public string CompressFileToZip(string pointPath, string fileName);
        public void SaveFile(string pointPath, BackUpJob backUpJob);
    }
}