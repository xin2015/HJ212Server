using HJ212Server.Core;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HJ212Server.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly int _port;
        private readonly int _maxClientCount;
        private readonly int _bufferSize;
        private int _currentClientCount;
        private readonly string _password;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            if (!int.TryParse(_configuration["Port"], out _port))
            {
                _port = 7025;
            }
            if (!int.TryParse(_configuration["MaxClientCount"], out _maxClientCount))
            {
                _maxClientCount = 100;
            }
            if (!int.TryParse(_configuration["BufferSize"], out _bufferSize))
            {
                _bufferSize = 1024;
            }
            _currentClientCount = 0;
            _password = _configuration["Password"] ?? "123456";
            if (string.IsNullOrEmpty(_password))
            {
                _password = "123456";
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Task acceptTask = AcceptAsync(stoppingToken);
                //Task sendTask = SendAsync(stoppingToken);
                await acceptTask;
                //await sendTask;
            }
        }

        protected virtual async Task AcceptAsync(CancellationToken stoppingToken)
        {
            try
            {
                using TcpListener listener = new TcpListener(IPAddress.Any, _port);
                listener.Start();
                List<Task> taskList = new List<Task>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (_currentClientCount != _maxClientCount)
                    {
                        TcpClient client = await listener.AcceptTcpClientAsync();
                        Interlocked.Increment(ref _currentClientCount);
                        taskList.Add(Task.Run(() => ReceiveAsync(client, stoppingToken), stoppingToken));
                        _logger.LogInformation("Accept {0} TcpClients and {1} TcpClients active.", taskList.Count, _currentClientCount);
                    }
                }
                Task.WaitAll(taskList.ToArray());
                listener.Stop();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AcceptAsync error");
            }
        }


        protected virtual async Task ReceiveAsync(TcpClient client, CancellationToken stoppingToken)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                byte[] bytes = new byte[_bufferSize];
                int i;
                string message;
                int tailIndex;
                int communicationPacketLength;
                while (!stoppingToken.IsCancellationRequested)
                {
                    i = await client.Client.ReceiveAsync(bytes, stoppingToken);
                    if (i == 0)
                    {
                        _logger.LogWarning("ReceiveAsync 0 bytes.");
                        break;
                    }
                    else
                    {
                        message = Encoding.ASCII.GetString(bytes, 0, i);
                        _logger.LogInformation(message);
                        stringBuilder.Append(message);
                        message = stringBuilder.ToString();
                        tailIndex = message.IndexOf(CommunicationPacket.TailConst);
                        while (tailIndex >= 0)
                        {
                            communicationPacketLength = tailIndex + CommunicationPacket.TailConst.Length;
                            CommunicationPacket communicationPacket;
                            //if (CommunicationPacket.TryParse(message.Substring(0, communicationPacketLength), out communicationPacket))
                            //{
                            //    // TODO
                            //    stringBuilder.Remove(0, communicationPacketLength);
                            //    message = stringBuilder.ToString();
                            //    tailIndex = message.IndexOf(CommunicationPacket.TailConst);
                            //}
                        }
                        if (tailIndex > 0)
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ReceiveAsync error.");
            }
            finally
            {
                client.Dispose();
                Interlocked.Decrement(ref _currentClientCount);
            }
        }

        //protected virtual async Task SendAsync(CancellationToken stoppingToken)
        //{
        //    try
        //    {
        //        List<Task> taskList = new List<Task>();
        //        int i = 0;
        //        while (!stoppingToken.IsCancellationRequested && i++ < _maxClientCount)
        //        {
        //            taskList.Add(SendSlaveDeviceTimeCalibrationAsync(stoppingToken));
        //        }
        //        Task.WaitAll(taskList.ToArray());
        //        await Task.CompletedTask;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "SendAsync error.");
        //    }
        //}

        //protected virtual async Task SendSlaveDeviceTimeCalibrationAsync(CancellationToken stoppingToken)
        //{
        //    try
        //    {
        //        using TcpClient client = new TcpClient();
        //        await client.ConnectAsync("localhost", _port);
        //        string uniqueCode = Guid.NewGuid().ToString();
        //        byte[] bytes;
        //        Flag flag = new Flag(false, true);
        //        DataSegment dataSegment;
        //        int i = 0;
        //        while (!stoppingToken.IsCancellationRequested && i++ < 1000)
        //        {
        //            dataSegment = new DataSegment(SystemCode.SystemInteraction, CommandCode.SlaveDeviceTimeCalibration, _password, uniqueCode, flag, 0, 0, new List<Dictionary<string, object>>());
        //            bytes = Encoding.ASCII.GetBytes(dataSegment.ToString() ?? "Nothing");
        //            await client.Client.SendAsync(bytes, stoppingToken);
        //            await Task.Delay(1000);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "SendSlaveDeviceTimeCalibrationAsync error.");
        //    }
        //}
    }
}
