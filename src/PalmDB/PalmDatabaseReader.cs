using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PalmDB.Serialization;

namespace PalmDB
{
    public class PalmDatabaseReader
    {
        public static async Task<PalmDatabase> ReadAsync(Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));

            var database = new PalmDatabase();
            var reader = new AsyncBinaryReader(stream);

            var nameValue = new StringPalmValue(32, Encoding.UTF8, zeroTerminated:true);
            database.Name = await nameValue.ReadValueAsync(reader);

            var attributesValue = new EnumPalmValue<PalmDatabaseAttributes>(2);
            database.Attributes = await attributesValue.ReadValueAsync(reader);
            
            var versionValue = new UIntPalmValue(2);
            database.Version = (short)await versionValue.ReadValueAsync(reader);

            var creationDateValue = new DateTimeOffsetPalmValue();
            database.CreationDate = await creationDateValue.ReadValueAsync(reader);

            var modificationDateValue = new DateTimeOffsetPalmValue();
            database.ModificationDate  = await modificationDateValue.ReadValueAsync(reader);

            var lastBackupDateValue = new DateTimeOffsetPalmValue();
            database.LastBackupDate = await lastBackupDateValue.ReadValueAsync(reader);

            var modificationNumberValue = new UIntPalmValue(4);
            database.ModificationNumber = await modificationNumberValue.ReadValueAsync(reader);

            var appInfoIdValue = new UIntPalmValue(4);
            database.AppInfoId = await appInfoIdValue.ReadValueAsync(reader);

            var sortInfoIdValue = new UIntPalmValue(4);
            database.SortInfoId = await sortInfoIdValue.ReadValueAsync(reader);

            var typeValue = new StringPalmValue(4, Encoding.UTF8, zeroTerminated:false);
            database.Type = await typeValue.ReadValueAsync(reader);

            var creatorValue = new StringPalmValue(4, Encoding.UTF8, zeroTerminated:false);
            database.Creator = await creatorValue.ReadValueAsync(reader);

            var uniqueIdSeedValue = new UIntPalmValue(4);
            database.UniqueIdSeed = await uniqueIdSeedValue.ReadValueAsync(reader);

            var nextRecordListIdValue = new UIntPalmValue(4);
            database.NextRecordListId = await nextRecordListIdValue.ReadValueAsync(reader);
            
            uint numberOfRecords = await new UIntPalmValue(2).ReadValueAsync(reader);
            
            var recordAndDataOffsets = new List<Tuple<PalmDatabaseRecord, uint>>();

            for (int i = 0; i < numberOfRecords; i++)
            {
                var recordDataOffset = await new UIntPalmValue(4).ReadValueAsync(reader);
                var recordAttribute = await new ByteArrayPalmValue(1).ReadValueAsync(reader);
                var uniqueId = await new UIntPalmValue(3).ReadValueAsync(reader);

                var record = new PalmDatabaseRecord
                {
                    UniqueId = uniqueId,
                    Attributes = recordAttribute[0],
                };

                recordAndDataOffsets.Add(Tuple.Create(record, recordDataOffset));
            }

            for (int i = 0; i < numberOfRecords; i++)
            {
                var currentRecord = recordAndDataOffsets[i];
                var nextRecord = i == numberOfRecords - 1 ? null : recordAndDataOffsets[i + 1];

                var startOffset = currentRecord.Item2;
                var nextStartOffset = nextRecord?.Item2 ?? (uint)stream.Length;

                stream.Seek(startOffset, SeekOrigin.Begin);

                var data = await new ByteArrayPalmValue((int)(nextStartOffset - startOffset)).ReadValueAsync(reader);
                currentRecord.Item1.Data = data;
            }

            database.Records = recordAndDataOffsets
                .Select(f => f.Item1)
                .ToList();

            return database;
        }
    }
}
