using System.IO;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class AsyncBinaryWriter
    {
        public Stream Stream { get; }

        public AsyncBinaryWriter(Stream stream)
        {
            this.Stream = stream;
        }

        public Task WriteAsync(byte[] data)
        {
            return this.Stream.WriteAsync(data, 0, data.Length);
        }
    }
}