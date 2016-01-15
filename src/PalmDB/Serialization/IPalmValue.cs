using System.IO;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    /// <summary>
    /// Handles reading and writing of the specified <typeparamref name="T"/> in a palm database file.
    /// </summary>
    /// <typeparam name="T">The type of value this class can read and write.</typeparam>
    internal interface IPalmValue<T>
    {
        /// <summary>
        /// Reads the <typeparamref name="T"/> using the specified <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        Task<T> ReadValueAsync(AsyncBinaryReader reader);
        /// <summary>
        /// Writes the specified <paramref name="value"/> using the specified <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        Task WriteValueAsync(AsyncBinaryWriter writer, T value);
    }
}