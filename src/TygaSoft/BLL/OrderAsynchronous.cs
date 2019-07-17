using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotterySln.IBLLStrategy;

namespace LotterySln.BLL
{
    public class OrderAsynchronous : IOrderStrategy
    {
        private static readonly IMessaging.IOrder asynchOrder = MessagingFactory.QueueAccess.CreateOrder();

        public void Insert(Model.UserBetLottery model)
        {
            asynchOrder.Send(model);
        }
    }
}
