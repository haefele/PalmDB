using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PalmDB.Serialization;
using Xunit;

namespace PalmDB.Tests.Serialization
{
    public class AsyncBinaryWriterTests
    {
        [Fact]
        public void The_Constructor_Throws_When_Stream_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new AsyncBinaryWriter(null);
            });
        }

        [Fact]
        public async Task The_WriteAsync_Method_Works()
        {
            var stream = new MemoryStream();
            var writer = new AsyncBinaryWriter(stream);
            var expected = Encoding.UTF8.GetBytes("123456");

            await writer.WriteAsync(expected);
            var actual = stream.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task The_WriteAsync_Method_Throws_If_Data_Is_Null()
        {
            var writer = new AsyncBinaryWriter(new MemoryStream());

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await writer.WriteAsync(null);
            });
        }
    }
}