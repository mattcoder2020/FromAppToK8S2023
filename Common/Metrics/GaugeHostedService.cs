using Microsoft.Extensions.Logging;
using System;

namespace Common.Metrics
{
    public class GaugeHostedService : HostedServiceTemplate
    {
        ILogger<GaugeHostedService> log;
        GaugeMetric gaugeMetric;
        GaugeOption option;
        
        public GaugeHostedService(ILogger<GaugeHostedService> log, GaugeMetric gaugeMetric, GaugeOption option) : base(option)
        {
            this.log = log;
            this.gaugeMetric = gaugeMetric;
            this.option = option;
        }
        protected override void ExecuteAsync(object state)
        {
            //log.LogInformation(DateTime.Now.ToLongTimeString());
            gaugeMetric.SendGaugeData();
        }
    }
}
