using System.IO;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    /// <summary>
    /// A simple class that can be used to read from a <see cref="Stream"/> asynchronously.
    /// </summary>
    internal class AsyncBinaryReader
    {
        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncBinaryReader"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public AsyncBinaryReader(Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));

            this.Stream = stream;
        }

        /// <summary>
        /// Reads the specified number of bytes (<paramref name="length"/>) from the underlying <see cref="Stream"/>.
        /// </summary>
        /// <param name="length">The number of bytes to read.</param>
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