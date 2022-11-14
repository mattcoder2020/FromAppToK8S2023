using RawRabbit.Configuration;

namespace Common.RabbitMQ
{
    public class RabbitMqOptions : RawRabbitConfiguration
    {
        public bool Enable { get; set; }
        public string Namespace { get; set; }
        public int Retries { get; set; }
        public int RetryInterval { get; set; }
    }
}