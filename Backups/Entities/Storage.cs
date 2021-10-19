using System;
using System.IO.Compression;
using Backups.Tools;

namespace Backups.Entities
{
    public class Storage
    {
        private readonly byte[] _bytesInfo;
        private readonly string _name;

        public Storage(byte[] bytesInfo, string name)
        {
            _bytesInfo = bytesInfo;
            _name = name;
        }

        public byte[] GetStorageBytesInfo() => _bytesInfo.Clone() as byte[];
        public string GetStorageName() => _name;
    }
}