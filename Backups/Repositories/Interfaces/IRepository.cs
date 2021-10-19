using System.Collections.Generic;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.Repositories.Interfaces
{
    public interface IRepository
    {
        void CreateRepository(BackUpJob backUpJob, List<FileDescription> files);
    }
}