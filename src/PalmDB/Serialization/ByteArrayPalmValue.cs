using System;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    /// <summary>
    /// Represents a <see cref="T:byte[]"/> value inside a palm database.
    /// </summary>
    internal class ByteArrayPalmValue : IPalmValue<byte[]>
    {
        /// <summary>
        /// Gets the length of this byte block.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteArrayPalmValue"/> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public ByteArrayPalmValue(int length)
        {
            Guard.NotNegative(length, nameof(length));

            this.Length = length;
        }

        /// <summary>
        /// Reads the <see cref="T:byte[]"/> using the specified <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public Task<byte[]> ReadValueAsync(AsyncBinaryReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            return reader.ReadAsync(this.Length);
        }
        /// <summary>
        /// Writes the specified <paramref name="value"/> using the specified <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public Task WriteValueAsync(AsyncBinaryWriter writer, byte[] value)
        {
            Guard.NotNull(writer, nameof(writer));
            Guard.NotNull(value, nameof(value));

            Array.Resize(ref value, this.Length);

            return writer.WriteAsync(value);
        }
    }
}