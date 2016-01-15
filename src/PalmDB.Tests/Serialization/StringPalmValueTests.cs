using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PalmDB.Serialization;
using Xunit;

namespace PalmDB.Tests.Serialization
{
    public class StringPalmValueTests : PalmTestBase
    {
        [Fact]
        public async Task The_ReadValueAsync_Method_Works()
        {
            var expected = "123456";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(expected));
            var reader = new AsyncBinaryReader(stream);

            var value = new StringPalmValue(expected.Length, Encoding.UTF8, true);
            var actual = await value.ReadValueAsync(reader);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task The_ReadValueAsync_Method_Works_With_TestFile()
        {
            var expected = "The_Wits_and-_Grace_Wharton";

            var stream = base.GetTestPalmDatabase();
            var reader = new AsyncBinaryReader(stream);

            var value = new StringPalmValue(32, Encoding.UTF8, true);
            var actual = await value.ReadValueAsync(reader);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task The_ReadValueAsync_Method_Reads_Until_First_Zero()
        {
            var expected = "123456";
            var bytes = Encoding.UTF8.GetBytes(expected);
            Array.Resize(ref bytes, 10);
            var stream = new MemoryStream(bytes);
            var reader = new AsyncBinaryReader(stream);

            var value = new StringPalmValue(expected.Length, Encoding.UTF8, true);
            var actual = await value.ReadValueAsync(reader);

            Assert.Equal(expected, actual);
        }
        

        [Fact]
        public async Task The_WriteValueAsync_Method_Works()
        {
            var expected = Encoding.UTF8.GetBytes("123456");
            Array.Resize(ref expected, 7);
            var stream = new MemoryStream();
            var writer = new AsyncBinaryWriter(stream);

            var value = new StringPalmValue(7, Encoding.UTF8, true);
            await value.WriteValueAsync(writer, "123456");
            var actual = stream.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task The_WriteValueAsync_Method_Paddes_With_Zeroes()
        {
            var expected = Encoding.UTF8.GetBytes("123456");
            Array.Resize(ref expected, 7);
            var stream = new MemoryStream();
            var writer = new AsyncBinaryWriter(stream);

            var value = new StringPalmValue(7, Encoding.UTF8, true);
            await value.WriteValueAsync(writer, "123456");
            var actual = stream.ToArray();

            Assert.Equal(expected, actual);
        }
    }
}