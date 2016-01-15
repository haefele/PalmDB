using System;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    /// <summary>
    /// Represents a <see cref="T:DateTimeOffset?"/> value inside a palm database.
    /// </summary>
    internal class DateTimeOffsetPalmValue : IPalmValue<DateTimeOffset?>
    {
        /// <summary>
        /// Gets a value indicating whether the <see cref="DateTimeOffset"/> should be written as a unix timestamp.
        /// </summary>
        public bool WriteAsUnixTimeStamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeOffsetPalmValue"/> class.
        /// </summary>
        /// <param name="writeAsUnixTimeStamp">Whether the <see cref="DateTimeOffset"/> should be written as a unix timestamp.</param>
        public DateTimeOffsetPalmValue(bool writeAsUnixTimeStamp = true)
        {
            this.WriteAsUnixTimeStamp = writeAsUnixTimeStamp;
        }

        /// <summary>
        /// Reads the <see cref="T:DateTimeOffset?"/> using the specified <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public async Task<DateTimeOffset?> ReadValueAsync(AsyncBinaryReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

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
        /// <summary>
        /// Writes the specified <paramref name="value"/> using the specified <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public async Task WriteValueAsync(AsyncBinaryWriter writer, DateTimeOffset? value)
        {
            Guard.NotNull(writer, nameof(writer));

            DateTimeOffset baseDateTime = this.WriteAsUnixTimeStamp
                ? new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)
                : new DateTimeOffset(1904, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var secondsSinceUnixTimestamp = value != null
                ? (uint)(value.Value - baseDateTime).TotalSeconds
                : 0U;

            var internalValue = new UintPalmValue(4);
            await internalValue.WriteValueAsync(writer, secondsSinceUnixTimestamp);
        }
    }
}