using System;
using System.Linq;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class DateTimeOffsetPalmValue : IPalmValue<DateTimeOffset?>
    {
        public async Task<DateTimeOffset?> ReadValueAsync(AsyncBinaryReader reader)
        {
            var internalValue = new UintPalmValue(4);
            var value = await internalValue.ReadValueAsync(reader);

            if (value == 0)
                return null;

            uint highBit = 1U << 31;

            var isPalmDate = (value & highBit) > 0;

            return isPalmDate 
                ? new DateTimeOffset(1904, 1, 1, 0, 0, 0, TimeSpan.Zero).AddSeconds(value) 
                : DateTimeOffset.FromUnixTimeSeconds(value);
        }

        public async Task WriteValueAsync(AsyncBinaryWriter writer, DateTimeOffset? value)
        {
            var secondsSinceUnixTimestamp = value != null
                ? (uint)(value.Value - new DateTime(1970, 1, 1)).TotalSeconds
                : 0U;

            var internalValue = new UintPalmValue(4);
            await internalValue.WriteValueAsync(writer, secondsSinceUnixTimestamp);
        }
    }
}