using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotterySln.IBLLStrategy;

namespace LotterySln.BLL
{
    public class OrderSynchronous : IOrderStrategy
    {
        private static readonly IDAL.IUserBetLottery dal = DALFactory.DataAccess.CreateUserBetLottery();

        public void Insert(Model.UserBetLottery model)
        {
            dal.Insert(model);
        }
    }
}
