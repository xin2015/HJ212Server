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
    public class DataSegmentUnitTest
    {
        [TestMethod]
        public void TestToString()
        {
            DataSegment dataSegment = new DataSegment(SystemCode.SurfaceWaterEnvironmentPollutantSource, CommandCode.GetPollutantLiveData, "123456", "010000A8900016F000169DC0", new Flag(1, 0, 1), 0, 0, new List<Dictionary<string, object>>());
            string dataSegmentString = dataSegment.ToString();
        }
    }
}
