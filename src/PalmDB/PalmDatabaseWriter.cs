using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PalmDB.Serialization;

namespace PalmDB
{
    /// <summary>
    /// Allows writing of palm databases.
    /// </summary>
    public class PalmDatabaseWriter
    {
        /// <summary>
        /// Writes the specified <paramref name="database"/> into the specified <paramref name="stream"/>.
        /// </summary>
        /// <param name="database">The palm database.</param>
        /// <param name="stream">The stream to write the database to.</param>
        public static async Task WriteAsync(PalmDatabase database, Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));
            Guard.Not(stream.CanSeek == false, nameof(stream.CanSeek));
            Guard.Not(stream.CanWrite == false, nameof(stream.CanWrite));

            var writer = new AsyncBinaryWriter(stream);

            var nameValue = new StringPalmValue(32, Encoding.UTF8, zeroTerminated:true);
            await nameValue.WriteValueAsync(writer, database.Name);

            var attributesValue = new EnumPalmValue<PalmDatabaseAttributes>(2);
            await attributesValue.WriteValueAsync(writer, database.Attributes);

            var versionValue = new UIntPalmValue(2);
            await versionValue.WriteValueAsync(writer, (uint)database.Version);

            var creationDateValue = new DateTimeOffsetPalmValue();
            await creationDateValue.WriteValueAsync(writer, database.CreationDate);

            var modificationDateValue = new DateTimeOffsetPalmValue();
            await modificationDateValue.WriteValueAsync(writer, database.ModificationDate);

            var lastBackupDateValue = new DateTimeOffsetPalmValue();
            await lastBackupDateValue.WriteValueAsync(writer, database.LastBackupDate);

            var modificationNumberValue = new UIntPalmValue(4);
            await modificationNumberValue.WriteValueAsync(writer, database.ModificationNumber);

            var appInfoIdValue = new UIntPalmValue(4);
            await appInfoIdValue.WriteValueAsync(writer, 0); // database.AppInfoId);

            var sortInfoIdValue = new UIntPalmValue(4);
            await sortInfoIdValue.WriteValueAsync(writer, 0); // database.SortInfoId);

            var typeValue = new StringPalmValue(4, Encoding.UTF8, zeroTerminated: false);
            await typeValue.WriteValueAsync(writer, database.Type);

            var creatorValue = new StringPalmValue(4, Encoding.UTF8, zeroTerminated: false);
            await creatorValue.WriteValueAsync(writer, database.Creator);

            var uniqueIdSeedValue = new UIntPalmValue(4);
            await uniqueIdSeedValue.WriteValueAsync(writer, database.UniqueIdSeed);

            var nextRecordListIdValue = new UIntPalmValue(4);
            await nextRecordListIdValue.WriteValueAsync(writer, database.NextRecordListId);

            var numberOfRecordsValue = new UIntPalmValue(2);
            await numberOfRecordsValue.WriteValueAsync(writer, (uint)database.Records.Count);

            var recordUniqueIdsAndOffsetOfDataOffsets = new Dictionary<uint, long>();

            //Write the records
            uint uniqueId = 0;
            foreach (var record in database.Records)
            {
                recordUniqueIdsAndOffsetOfDataOffsets.Add(uniqueId, stream.Position);

                var recordDataOffsetValue = new UIntPalmValue(4);
                await recordDataOffsetValue.WriteValueAsync(writer, 0);

                var recordAttributeValue = new ByteArrayPalmValue(1);
                await recordAttributeValue.WriteValueAsync(writer, new[] {record.Attributes});

                var uniqueIdValue = new UIntPalmValue(3);
                await uniqueIdValue.WriteValueAsync(writer, uniqueId);

                record.UniqueId = uniqueId;

                uniqueId++;
            }

            var fillerValue = new ByteArrayPalmValue(2);
            await fillerValue.WriteValueAsync(writer, new byte[2]);

            //Write the record content
            foreach (var record in database.Records)
            {
                var offsetOfDataOffset = recordUniqueIdsAndOffsetOfDataOffsets[record.UniqueId];
                var dataOffset = (uint)writer.Stream.Position;

                var dataValue = new ByteArrayPalmValue(record.Data.Length);
                await dataValue.WriteValueAsync(writer, record.Data);

                var endOffset = writer.Stream.Position;

                writer.Stream.Seek(offsetOfDataOffset, SeekOrigin.Begin);
                
                var recordDataOffsetValue = new UIntPalmValue(4);
                await recordDataOffsetValue.WriteValueAsync(writer, dataOffset);

                writer.Stream.Seek(endOffset, SeekOrigin.Begin);
            }
        }
    }
}