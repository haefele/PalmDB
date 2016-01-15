using System.IO;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class AsyncBinaryWriter
    {
        public Stream Stream { get; }

        public AsyncBinaryWriter(Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));

            this.Stream = stream;
        }

        public Task WriteAsync(byte[] data)
        {
            Guard.NotNull(data, nameof(data));

            return this.Stream.WriteAsync(data, 0, data.Length);
        }
    }
}