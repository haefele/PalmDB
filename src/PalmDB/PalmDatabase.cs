using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmDB
{
    public class PalmDatabase
    {
        public string Name { get; set; }
        public PalmDatabaseAttributes Attributes { get; set; }
        public short Version { get; set; }
        public DateTimeOffset? CreationDate { get; set; }
        public DateTimeOffset? ModificationDate { get; set; }
        public DateTimeOffset? LastBackupDate { get; set; }
        public uint ModificationNumber { get; set; }
        public uint AppInfoId { get; set; }
        public uint SortInfoId { get; set; }
        public string Type { get; set; }
        public string Creator { get; set; }
        public uint UniqueIdSeed { get; set; }
        public uint NextRecordListId { get; set; }
        public List<PalmDatabaseRecord> Records { get; set; } 
    }

    public class PalmDatabaseRecord
    {
        public uint UniqueId { get; set; }
        public byte[] Data { get; set; }   
        public byte Attributes { get; set; }
    }
}
