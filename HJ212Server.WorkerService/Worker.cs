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
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await AcceptAsync(stoppingToken);
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
                        taskList.Add(ReceiveAsync(client, stoppingToken));
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
                byte[] bytes = new byte[_bufferSize];
                int i;
                string message;
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
    }
}
