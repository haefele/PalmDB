using System;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class UintPalmValue : IPalmValue<uint>
    {
        public int Length { get; }

        public UintPalmValue(int length)
        {
            Guard.NotNegative(length, nameof(length));

            this.Length = length;
        }

        public async Task<uint> ReadValueAsync(AsyncBinaryReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            var data = await reader.ReadAsync(this.Length);
            
            Array.Reverse(data);
            Array.Resize(ref data, 4);

            return BitConverter.ToUInt32(data, 0);
        }

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