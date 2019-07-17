using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using LotterySln.IDAL;
using LotterySln.Model;

namespace LotterySln.BLL
{
    public class Order
    {
        private static readonly IBLLStrategy.IOrderStrategy orderInsertStrategy = LoadInsertStrategy();
        private static readonly IUserBetLottery dal = DALFactory.DataAccess.CreateUserBetLottery();
        private static readonly IMessaging.IOrder orderQueue = MessagingFactory.QueueAccess.CreateOrder();

        public void Insert(Model.UserBetLottery model)
        {
            orderInsertStrategy.Insert(model);
        }

        /// <summary>
        /// Method to process asynchronous order from the queue
        /// </summary>
        public Model.UserBetLottery ReceiveFromQueue(int timeout)
        {
            return orderQueue.Receive(timeout);
        }

        private static IBLLStrategy.IOrderStrategy LoadInsertStrategy()
        {
            string path = ConfigurationManager.AppSettings["OrderStrategyAssembly"];
            string className = ConfigurationManager.AppSettings["OrderStrategyClass"];

            return (IBLLStrategy.IOrderStrategy)Assembly.Load(path).CreateInstance(className);
        }
    }
}
