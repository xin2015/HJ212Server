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
            Flag flag = new Flag(1, 0, 1);
            Assert.AreEqual(flag.ToString(), "5");
        }

        [TestMethod]
        public void TestTryParse()
        {
            string flagString = "4";
            Flag flag;
            if (Flag.TryParse(flagString, out flag))
            {
                Assert.IsTrue(flag.D == 0 && flag.A == 0);
            }
            else
            {
                Assert.Fail();
            }
            flagString = "5";
            if (Flag.TryParse(flagString, out flag))
            {
                Assert.IsTrue(flag.D == 0 && flag.A == 1);
            }
            else
            {
                Assert.Fail();
            }
            flagString = "6";
            if (Flag.TryParse(flagString, out flag))
            {
                Assert.IsTrue(flag.D == 1 && flag.A == 0);
            }
            else
            {
                Assert.Fail();
            }
            flagString = "7";
            if (Flag.TryParse(flagString, out flag))
            {
                Assert.IsTrue(flag.D == 1 && flag.A == 1);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
