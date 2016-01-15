using System.IO;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    /// <summary>
    /// A simple class that can be used to write to a <see cref="Stream"/> asynchronously.
    /// </summary>
    internal class AsyncBinaryWriter
    {
        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncBinaryWriter"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public AsyncBinaryWriter(Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));

            this.Stream = stream;
        }

        /// <summary>
        /// Writes the specified <paramref name="data"/> asynchronously to the underlying <see cref="Stream"/>.
        /// </summary>
        /// <param name="data">The data.</param>
        public Task WriteAsync(byte[] data)
        {
            Guard.NotNull(data, nameof(data));

            return this.Stream.WriteAsync(data, 0, data.Length);
        }
    }
}