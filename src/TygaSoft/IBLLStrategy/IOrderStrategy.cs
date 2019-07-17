using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotterySln.IBLLStrategy
{
    public interface IOrderStrategy
    {
        void Insert(Model.UserBetLottery order);
    }
}
