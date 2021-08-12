using BeymenCodeCase.Common;
using BeymenCodeCase.Services.Redis;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeymenCodeCase.Services.Services
{
    public class BackgroundServiceTask : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundService> logger;
        private readonly ConfigurationSetting configurationSetting;
        private readonly IRedisService redisService;
        private int number = 0;
        private Timer timer;

        public BackgroundServiceTask(ILogger<BackgroundService> logger,
            IRedisService redisService,
           ConfigurationSetting configurationSetting)
        {
            this.logger = logger;
            this.configurationSetting = configurationSetting;
            this.redisService = redisService;
        }
        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                timer = new Timer(m =>
                {
                    Interlocked.Increment(ref number);
                    logger.LogInformation($"Number is {number}");

                    redisService.InsertOrUpdateRedis();
                }, null, TimeSpan.Zero, TimeSpan.FromSeconds(configurationSetting.refreshTimerIntervalInMs));
            }
           
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Syncronziation Service Stopped");
            return Task.CompletedTask;
        }
    }
}
