using App.Metrics;
using App.Metrics.Counter;


namespace Common.Metrics
{
    public class MetricRegistry : IMetricRegistry
    {
        private readonly IMetricsRoot root;

        private readonly string name;

        public MetricRegistry(IMetricsRoot root) => this.root = root;

        public void IncrementCount(string name)
        {
            CounterOptions option = new CounterOptions { Name = name };
            root.Measure.Counter.Increment(option);
        }
    }
}
