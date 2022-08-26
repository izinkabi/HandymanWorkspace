using Handyman_UI_Provider.Hubs.Order;
using Microsoft.AspNetCore.SignalR;

namespace Handyman_UI_Provider.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
        //private readonly IHubContext<Order, IOrderClient> _clockHub;

        //public Worker(ILogger<Worker> logger, IHubContext<ClockHub, IClock> clockHub)
        //{
        //    _logger = logger;
        //    _clockHub = clockHub;
        //}

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
        //        await _clockHub.Clients.All.ShowTime(DateTime.Now);
        //        await Task.Delay(1000, stoppingToken);
        //    }
        //}
    }
}
