using System;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class BytePalmValue : IPalmValue<byte[]>
    {
        public int Length { get; }

        public BytePalmValue(int length)
        {
            this.Length = length;
        }

        public Task<byte[]> ReadValueAsync(AsyncBinaryReader reader)
        {
            return reader.ReadAsync(this.Length);
        }

        public Task WriteValueAsync(AsyncBinaryWriter writer, byte[] value)
        {
            Array.Resize(ref value, this.Length);

            return writer.WriteAsync(value);
        }
    }
}