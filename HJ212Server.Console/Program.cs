// See https://aka.ms/new-console-template for more information
using HJ212Server.Core;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
string uniqueCode = Guid.NewGuid().ToString();
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
DataSegment dataSegment = new DataSegment(SystemCode.SurfaceWaterEnvironmentPollutantSource, CommandCode.SetTimeoutPeriodAndRetransmission, "123456", uniqueCode, flag, 0, 0, list);
int count = 10000000;
for (int i = 0; i < 3; i++)
{
    Stopwatch sw = new Stopwatch();
    sw.Start();
    for (int j = 0; j < count; j++)
    {
        string s = dataSegment.ToString();
    }
    sw.Stop();
    Console.WriteLine(sw.Elapsed);
}
Console.ReadLine();



//using TcpClient client = new TcpClient();
//await client.ConnectAsync("localhost", 7025);
//Random random = new Random();
//string uniqueCode = Guid.NewGuid().ToString();
//for (int i = 0; i < 1000000; i++)
//{
//    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
//    Dictionary<string, object> overTime = new Dictionary<string, object>()
//    {
//        {"OverTime", random.Next() }
//    };
//    list.Add(overTime);
//    Dictionary<string, object> reCount = new Dictionary<string, object>()
//    {
//        {"ReCount", random.Next() }
//    };
//    list.Add(reCount);
//    Flag flag = new Flag(false, true);
//    DataSegment dataSegment = new DataSegment(SystemCode.SurfaceWaterEnvironmentPollutantSource, CommandCode.SetTimeoutPeriodAndRetransmission, "123456", uniqueCode, flag, 0, 0, list);
//    string dataSegmentString = dataSegment.ToString();
//    await client.Client.SendAsync(Encoding.ASCII.GetBytes(dataSegmentString));
//    await Task.Delay(1000);
//}
//random.ToString();
