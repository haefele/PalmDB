using System.IO;
using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal interface IPalmValue<T>
    {
        Task<T> ReadValueAsync(AsyncBinaryReader reader);
        Task WriteValueAsync(AsyncBinaryWriter writer, T value);
    }
}