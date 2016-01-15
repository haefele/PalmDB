using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    /// <summary>
    /// Represents a <see cref="string"/> value inside a palm database.
    /// </summary>
    internal class StringPalmValue : IPalmValue<string>
    {
        /// <summary>
        /// Gets the length of this string block.
        /// </summary>
        public int Length { get; }
        /// <summary>
        /// Gets the encoding used to encode and decode the string.
        /// </summary>
        public Encoding Encoding { get; }
        /// <summary>
        /// Gets a value indicating whether the string is terminated with a zero-byte.
        /// </summary>
        public bool ZeroTerminated { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringPalmValue"/> class.
        /// </summary>
        /// <param name="length">The length of the string block.</param>
        /// <param name="encoding">The encoding used to encode and decode the string.</param>
        /// <param name="zeroTerminated">Whether the string is terminated with a zero-byte.</param>
        public StringPalmValue(int length, Encoding encoding, bool zeroTerminated)
        {
            Guard.NotNegative(length, nameof(length));
            Guard.NotNull(encoding, nameof(encoding));

            this.Length = length;
            this.Encoding = encoding;
            this.ZeroTerminated = zeroTerminated;
        }

        /// <summary>
        /// Reads the <see cref="string"/> using the specified <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
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
        /// <summary>
        /// Writes the specified <paramref name="value"/> using the specified <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
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