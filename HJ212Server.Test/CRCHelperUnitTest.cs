using HJ212Server.Core;
using System.Text;

namespace HJ212Server.Test
{
    [TestClass]
    public class CRCHelperUnitTest
    {
        [TestMethod]
        public void TestComputeCRC16()
        {
            string dataSegmentString = "QN=20160801085857223;ST=32;CN=1062;PW=100000;MN=010000A8900016F000169DC0;Flag=5;CP=&&RtdInterval=30&&";
            Assert.AreEqual(dataSegmentString.Length, 101);
            int crc = CRCHelper.ComputeCRC16(Encoding.ASCII.GetBytes(dataSegmentString));
            Assert.AreEqual<string>(crc.ToString("X4"), "1C80");
        }
    }
}