using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Metrics
{
    public abstract class HostedServiceTemplate : IHostedService
    {
        private readonly CancellationTokenSource source;
        private int interval = 10;
        private Timer timer;
        private GaugeOption option;
         
        public HostedServiceTemplate()
        { }

        protected HostedServiceTemplate(GaugeOption option)
        {
            this.option = option;
            interval = option.delay;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Create new cancel token
            timer = new Timer(ExecuteAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
            return Task.CompletedTask;
        }

        protected abstract void ExecuteAsync(object state);
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Trigger source cancel to stop the on going 
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
