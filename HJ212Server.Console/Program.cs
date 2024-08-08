// See https://aka.ms/new-console-template for more information
using HJ212Server.Core;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

using TcpClient client = new TcpClient();
await client.ConnectAsync("localhost", 7025);
Random random = new Random();
string uniqueCode = Guid.NewGuid().ToString();
for (int i = 0; i < 1000000; i++)
{
    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
    Dictionary<string, object> overTime = new Dictionary<string, object>()
    {
        {"OverTime", random.Next() }
    };
    list.Add(overTime);
    Dictionary<string, object> reCount = new Dictionary<string, object>()
    {
        {"ReCount", random.Next() }
    };
    list.Add(reCount);
    Flag flag = new Flag(false, true);
    DataSegment dataSegment = new DataSegment(SystemCode.SurfaceWaterEnvironmentPollutantSource, CommandCode.SetTimeoutPeriodAndRetransmission, "123456", uniqueCode, flag, 0, 0, list);
    string dataSegmentString = dataSegment.ToString();
    await client.Client.SendAsync(Encoding.ASCII.GetBytes(dataSegmentString));
    await Task.Delay(1000);
}