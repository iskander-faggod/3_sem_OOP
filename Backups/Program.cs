using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            using (var fileStream = new FileStream("/home/iskander/Desktop/iskander-faggod/Backups/Backups.csproj", FileMode.Create))
            using (var archive = new ZipArchive(fileStream, FileMode.Create))
            {
            }
        }
    }
}