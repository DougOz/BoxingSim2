using Microsoft.VisualStudio.TestTools.UnitTesting;
using Weebul.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Scripting.Tests
{
    [TestClass()]
    public class Class1Tests
    {
        [TestMethod()]
        public void DoSomethingTest()
        {
            Class1 c1 = new Class1();
            c1.DoSomething();

            int i = int.Parse("1)");
        }
    }
}