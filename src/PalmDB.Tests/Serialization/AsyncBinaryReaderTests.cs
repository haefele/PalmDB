using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmDB.Serialization;
using Xunit;

namespace PalmDB.Tests.Serialization
{
    public class AsyncBinaryReaderTests
    {
        [Fact]
        public void The_Constructor_Throws_When_Stream_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new AsyncBinaryReader(null);
            });
        }

        [Fact]
        public async Task The_ReadAsync_Method_Works()
        {
            var expected = Encoding.UTF8.GetBytes("123456");
            var stream = new MemoryStream(expected);
            var reader = new AsyncBinaryReader(stream);

            var actual = await reader.ReadAsync(expected.Length);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task The_ReadAsync_Method_Throws_When_Length_Is_Negative()
        {
            var reader = new AsyncBinaryReader(new MemoryStream());

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await reader.ReadAsync(-1);
            });
        }
    }
}
