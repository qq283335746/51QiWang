using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheHelper.LotterySln
{
    public class UserTicket : MsSqlCacheDependency
    {
        public UserTicket() : base("UserTicketTableDependency") { }
    }
}
