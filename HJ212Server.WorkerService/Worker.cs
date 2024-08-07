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
                listener.Start(_maxClientCount);
                List<Task> taskList = new List<Task>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    taskList.Add(ReceiveAsync(client, stoppingToken));
                    _logger.LogInformation("Accept {0} TcpClients.", taskList.Count);
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
                byte[] bytes = new byte[1024];
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
                        message = Encoding.ASCII.GetString(bytes);
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
            }
        }
    }
}
