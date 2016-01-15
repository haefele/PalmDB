namespace PalmDB
{
    /// <summary>
    /// A single record inside a palm database.
    /// </summary>
    public class PalmDatabaseRecord
    {
        /// <summary>
        /// Gets or sets the unique identifier of this record.
        /// </summary>
        public uint UniqueId { get; set; }
        /// <summary>
        /// Gets or sets the data of this record.
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// Gets or sets the attributes of this record.
        /// </summary>
        public byte Attributes { get; set; }
    }
}