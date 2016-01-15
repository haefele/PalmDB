using System;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    /// <summary>
    /// Represents a <see cref="int"/> based <see cref="Enum"/> value inside a palm database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class EnumPalmValue<T> : IPalmValue<T>
        where T : struct
    {
        /// <summary>
        /// Gets the length of this enum block.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumPalmValue{T}"/> class.
        /// </summary>
        /// <param name="length">The length of the enum block.</param>
        public EnumPalmValue(int length)
        {
            Guard.NotNegative(length, nameof(length));

            this.Length = length;
        }

        /// <summary>
        /// Reads the <typeparamref name="T"/> using the specified <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public async Task<T> ReadValueAsync(AsyncBinaryReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            var internalValue = new UintPalmValue(this.Length);
            var data = await internalValue.ReadValueAsync(reader);

            return (T)(object)(int)data;
        }
        /// <summary>
        /// Writes the specified <paramref name="value"/> using the specified <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public async Task WriteValueAsync(AsyncBinaryWriter writer, T value)
        {
            Guard.NotNull(writer, nameof(writer));
            Guard.NotNull(value, nameof(value));

            var internalValue = new UintPalmValue(this.Length);
            var data = (uint)(int)(object)value;

            await internalValue.WriteValueAsync(writer, data);
        }
    }
}