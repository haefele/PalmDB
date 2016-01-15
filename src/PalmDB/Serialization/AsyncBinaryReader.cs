using System.IO;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class AsyncBinaryReader
    {
        public Stream Stream { get; }

        public AsyncBinaryReader(Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));

            this.Stream = stream;
        }

        public async Task<byte[]> ReadAsync(int length)
        {
            Guard.NotNegative(length, nameof(length));

            var data = new byte[length];

            int currentPosition = 0;

            while (currentPosition < length)
            {
                int numberOfBytesRead = await this.Stream.ReadAsync(data, 0, length);
                currentPosition += numberOfBytesRead;
            }

            return data;
        }
    }
}