using HJ212Server.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HJ212Server.Test
{
    [TestClass]
    public class FlagUnitTest
    {
        [TestMethod]
        public void TestToString()
        {
            Flag flag = new Flag(false, true);
            Assert.AreEqual(flag.ToString(), "5");
        }
    }
}
