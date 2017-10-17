using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.LDS.Lists;
//using DataStructures.LSD.Lists;
//using DataStructures.LDS.Lists;

namespace DataStructuresTests.LSD.Lists
{
    [TestClass]
    public class mtfSOListTest : SOListTests<object>
    {
        public object o1 = new object();
        public object o2 = new object();
        public object o3 = new object();

        public override SOListBase<object> GetTestClass()
        {
            return new mtfSOList<object>();
        }

        public override object GetValue1()
        {
            return o1;
        }

        public override object GetValue2()
        {
            return o2;
        }

        public override object GetValue3()
        {
            return o3;
        }
    }
}
