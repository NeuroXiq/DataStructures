using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresTests.LSD.Lists
{
    [TestClass]
    public class DLListObjectTests : DLListTest<object>
    {
        static object Sample = new object();

        protected override object CreateDifferentValue()
        {
            return new object();
        }

        protected override object CreateSampleValue()
        {
            return Sample;
        }
    }
}
