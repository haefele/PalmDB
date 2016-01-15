using System.Threading.Tasks;

namespace PalmDB.Serialization
{
    internal class EnumPalmValue<T> : IPalmValue<T>
        where T : struct
    {
        public int Length { get; }

        public EnumPalmValue(int length)
        {
            this.Length = length;
        }

        public async Task<T> ReadValueAsync(AsyncBinaryReader reader)
        {
            var internalValue = new UintPalmValue(this.Length);
            var data = await internalValue.ReadValueAsync(reader);

            return (T)(object)(int)data;
        }

        public async Task WriteValueAsync(AsyncBinaryWriter writer, T value)
        {
            var internalValue = new UintPalmValue(this.Length);
            var data = (uint)(int)(object)value;

            await internalValue.WriteValueAsync(writer, data);
        }
    }
}