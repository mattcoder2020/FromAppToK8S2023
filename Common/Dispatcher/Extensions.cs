using Autofac;
using Common.Dispacher;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dispatcher
{
    public static class Extensions
    {
        public static void AddDispatcher(this ContainerBuilder builder)
        {
            builder.RegisterType<Dispatcher>().As<IDispatcher>();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
           // builder.RegisterType<Dispatcher>().As<IDispatcher>();
        }
    }
}
