using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Consul
{
    public class ConsulOptions
    {
        public bool enabled { get; set; }
        public string url { get; set; }
        public string service { get; set; }
        public string address { get; set; }
        public int port { get; set; }
        public bool pingEnabled { get; set; }
        public string pingEndpoint { get; set; }
        public int pingInterval { get; set; }
        public int removeAfterInterval { get; set; }
        public int requestRetries { get; set; }
    }


}
