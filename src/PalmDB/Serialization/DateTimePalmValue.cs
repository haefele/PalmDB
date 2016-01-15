using System;
using System.Linq;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class DateTimePalmValue : IPalmValue<DateTime?>
    {
        public async Task<DateTime?> ReadValueAsync(AsyncBinaryReader reader)
        {
            var internalValue = new UintPalmValue(4);
            var value = await internalValue.ReadValueAsync(reader);

            if (value == 0)
                return null;
            
            uint epochDifference = 2082844800U;
            uint highBit = 2147483648;

            var isPalmDate = (value & highBit) > 0;
            if (isPalmDate)
            {
                value -= epochDifference;
            }

            return new DateTime(1970, 1, 1).AddSeconds(value);
        }

        public async Task WriteValueAsync(AsyncBinaryWriter writer, DateTime? value)
        {
            var secondsSinceUnixTimestamp = value != null
                ? (uint)(value.Value - new DateTime(1970, 1, 1)).TotalSeconds
                : 0U;

            var internalValue = new UintPalmValue(4);
            await internalValue.WriteValueAsync(writer, secondsSinceUnixTimestamp);
        }
    }
}