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
        public async Task The_ReadAsync_Method_ActuallyWorks()
        {
            var expected = Encoding.UTF8.GetBytes("123456");
            var stream = new MemoryStream(expected);

            var reader = new AsyncBinaryReader(stream);
            var actual = await reader.ReadAsync(expected.Length);

            Assert.Equal(expected, actual);
        }
    }
}
