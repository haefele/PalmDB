using System;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class ByteArrayPalmValue : IPalmValue<byte[]>
    {
        public int Length { get; }

        public ByteArrayPalmValue(int length)
        {
            Guard.NotNegative(length, nameof(length));

            this.Length = length;
        }

        public Task<byte[]> ReadValueAsync(AsyncBinaryReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            return reader.ReadAsync(this.Length);
        }

        public Task WriteValueAsync(AsyncBinaryWriter writer, byte[] value)
        {
            Guard.NotNull(writer, nameof(writer));
            Guard.NotNull(value, nameof(value));

            Array.Resize(ref value, this.Length);

            return writer.WriteAsync(value);
        }
    }
}