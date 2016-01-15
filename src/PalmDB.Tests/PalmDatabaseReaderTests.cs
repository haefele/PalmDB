using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PalmDB.Tests
{
    public class PalmDatabaseReaderTests : PalmTestBase
    {
        [Fact]
        public async Task The_ReadAsync_Method_Works()
        {
            var stream = base.GetTestPalmDatabase();

            var database = await PalmDatabaseReader.ReadAsync(stream);
            
        }
    }
}
