using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messages
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SubscriptionNamespaceAttribute : Attribute
    {
        public SubscriptionNamespaceAttribute(string @namespace)
        {
            Namespace = @namespace;
        }

        public string Namespace { get; }
    }
}
