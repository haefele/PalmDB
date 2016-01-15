using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmDB.Tests
{
    public abstract class PalmTestBase
    {
        protected Stream GetTestPalmDatabase()
        {
            return this.GetType().Assembly.GetManifestResourceStream("PalmDB.Tests.testbook.mobi");
        }
    }
}
