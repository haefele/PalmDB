using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace PalmDB.Tests
{
    public class PalmDatabaseReaderTests : PalmTestBase
    {
        private readonly ITestOutputHelper _output;

        public PalmDatabaseReaderTests(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public async Task The_ReadAsync_Method_Works()
        {
            var stream = base.GetTestPalmDatabase();

            var database = await PalmDatabaseReader.ReadAsync(stream);

            this._output.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            
            var targetStream = File.Open(".\\test.mobi", FileMode.Create, FileAccess.Write);
            await PalmDatabaseWriter.WriteAsync(database, targetStream);
        }
    }
}
