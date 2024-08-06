// See https://aka.ms/new-console-template for more information
using HJ212Server.Core;
using System.Data;

List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
Dictionary<string, object> overTime = new Dictionary<string, object>()
{
    {"OverTime", 5 }
};
list.Add(overTime);
Dictionary<string, object> reCount = new Dictionary<string, object>()
{
    {"ReCount", 3 }
};
list.Add(reCount);
Flag flag = new Flag(false, true);
DataSegment dataSegment = new DataSegment(SystemCode.SurfaceWaterEnvironmentPollutantSource, CommandCode.SetTimeoutPeriodAndRetransmission, "123456", "010000A8900016F000169DC0", flag, 0, 0, list);
string dataSegmentString = dataSegment.ToString();

Console.ReadLine();