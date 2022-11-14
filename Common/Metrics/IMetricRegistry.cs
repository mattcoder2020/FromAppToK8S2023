namespace Common.Metrics
{
    public interface IMetricRegistry
    {
        void IncrementCount(string name);
    }
}