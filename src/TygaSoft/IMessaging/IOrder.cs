using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotterySln.IMessaging
{
    public interface IOrder
    {
        /// <summary>
        /// Method to retrieve order information from a messaging queue
        /// </summary>
        /// <returns>All information about an order</returns>
        Model.UserBetLottery Receive();

        /// <summary>
        /// Method to retrieve order information from a messaging queue
        /// </summary>
        /// <returns>All information about an order</returns>
        Model.UserBetLottery Receive(int timeout);

        /// <summary>
        /// Method to send an order to a message queue for later processing
        /// </summary>
        /// <param name="body">All information about an order</param>
        void Send(Model.UserBetLottery orderMessage);
    }
}
