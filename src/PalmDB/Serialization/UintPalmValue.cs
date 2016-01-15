using System;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    /// <summary>
    /// Represents a <see cref="uint"/> value inside a palm database.
    /// </summary>
    internal class UIntPalmValue : IPalmValue<uint>
    {
        /// <summary>
        /// Gets the length of this uint block.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UIntPalmValue"/> class.
        /// </summary>
        /// <param name="length">The length of the uint block.</param>
        public UIntPalmValue(int length)
        {
            Guard.NotNegative(length, nameof(length));

            this.Length = length;
        }

        /// <summary>
        /// Reads the <see cref="uint"/> using the specified <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public async Task<uint> ReadValueAsync(AsyncBinaryReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            var data = await reader.ReadAsync(this.Length);
            
            Array.Reverse(data);
            Array.Resize(ref data, 4);

            return BitConverter.ToUInt32(data, 0);
        }
        /// <summary>
        /// Writes the specified <paramref name="value"/> using the specified <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public async Task WriteValueAsync(AsyncBinaryWriter writer, uint value)
        {
            Guard.NotNull(writer, nameof(writer));

            var data = BitConverter.GetBytes(value);
            Array.Reverse(data);
            Array.Resize(ref data, this.Length);

            await writer.WriteAsync(data);
        }
    }
}