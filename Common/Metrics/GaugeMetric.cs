using App.Metrics;
using App.Metrics.Gauge;
using System.Diagnostics;

namespace Common.Metrics
{
    public class GaugeMetric
    {
        private readonly IMetricsRoot root;
        private readonly GaugeOption option;
        public GaugeMetric(IMetricsRoot root, GaugeOption option)
        {
            this.root = root;
            this.option = option;
        }
        public void SendGaugeData()
        {
            var processPhysicalMemoryGauge = new GaugeOptions
            {
                Name = option.name,
                MeasurementUnit = Unit.MegaBytes
            };

            var process = Process.GetCurrentProcess();
            root.Measure.Gauge.SetValue(processPhysicalMemoryGauge, process.WorkingSet64 / 1024.0 / 1024.0);
        }
    }
}
