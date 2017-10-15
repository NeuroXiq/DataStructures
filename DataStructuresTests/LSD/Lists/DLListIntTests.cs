
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresTests.LSD.Lists
{
    [TestClass]
    public class DLListIntTests : DLListTest<int>
    {
        protected override int CreateSampleValue()
        {
            return 5;
        }

        protected override int CreateDifferentValue()
        {
            return -1;
        }
    }
}
