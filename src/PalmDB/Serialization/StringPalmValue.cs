using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class StringPalmValue : IPalmValue<string>
    {
        public int Length { get; }
        public Encoding Encoding { get; }
        public bool ZeroTerminated { get; }

        public StringPalmValue(int length, Encoding encoding, bool zeroTerminated)
        {
            Guard.NotNegative(length, nameof(length));
            Guard.NotNull(encoding, nameof(encoding));

            this.Length = length;
            this.Encoding = encoding;
            this.ZeroTerminated = zeroTerminated;
        }

        public async Task<string> ReadValueAsync(AsyncBinaryReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            var data = await reader.ReadAsync(this.Length);

            if (this.ZeroTerminated)
            {
                data = data
                    .TakeWhile(f => f != 0)
                    .ToArray();
            }

            return this.Encoding.GetString(data);
        }

        public Task WriteValueAsync(AsyncBinaryWriter writer, string value)
        {
            Guard.NotNull(writer, nameof(writer));
            Guard.NotNull(value, nameof(value));

            var data = this.Encoding.GetBytes(value);

            //Ensure data has right length
            Array.Resize(ref data, this.Length);

            if (this.ZeroTerminated)
            {
                //Ensure last byte is 0
                data[this.Length - 1] = 0;
            }

            return writer.WriteAsync(data);
        }
    }
}