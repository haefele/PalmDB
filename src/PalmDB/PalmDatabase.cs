using System;
using System.Collections.Generic;

namespace PalmDB
{
    /// <summary>
    /// A palm database.
    /// For more information on the properties see http://wiki.mobileread.com/wiki/PDB#Palm_Database_Format.
    /// </summary>
    public class PalmDatabase
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the database attributes.
        /// </summary>
        public PalmDatabaseAttributes Attributes { get; set; }
        /// <summary>
        /// Gets or sets the file version.
        /// This is usually 0.
        /// </summary>
        public short Version { get; set; }
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }
        /// <summary>
        /// Gets or sets the modification date.
        /// </summary>
        public DateTimeOffset? ModificationDate { get; set; }
        /// <summary>
        /// Gets or sets the last backup date.
        /// </summary>
        public DateTimeOffset? LastBackupDate { get; set; }
        /// <summary>
        /// Gets or sets the modification number.
        /// This is usually 0.
        /// </summary>
        public uint ModificationNumber { get; set; }
        /// <summary>
        /// Gets or sets the application information identifier.
        /// </summary>
        public uint AppInfoId { get; set; }
        /// <summary>
        /// Gets or sets the sort information identifier.
        /// </summary>
        public uint SortInfoId { get; set; }
        /// <summary>
        /// Gets or sets the type of this database.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the creator of this database..
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier seed.
        /// This is usually 0.
        /// </summary>
        public uint UniqueIdSeed { get; set; }
        /// <summary>
        /// Gets or sets the next record list identifier.
        /// This is usually 0.
        /// </summary>
        public uint NextRecordListId { get; set; }
        /// <summary>
        /// Gets or sets the records in this database.
        /// </summary>
        public List<PalmDatabaseRecord> Records { get; set; } 
    }
}
