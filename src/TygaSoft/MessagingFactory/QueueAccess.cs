using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace LotterySln.MessagingFactory
{
    public sealed class QueueAccess
    {
        // Look up the Messaging implementation we should be using
        private static readonly string[] path = ConfigurationManager.AppSettings["OrderMessaging"].Split(new char[]{','});

        private QueueAccess() { }

        public static IMessaging.IOrder CreateOrder()
        {
            string className = path[0] + ".Order";
            return (IMessaging.IOrder)Assembly.Load(path[1]).CreateInstance(className);
        }
    }
}
