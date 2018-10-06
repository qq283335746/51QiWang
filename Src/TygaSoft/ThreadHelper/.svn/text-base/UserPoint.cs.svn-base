using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using LotterySln.BLL;
using ThreadHelper;

namespace LotterySln.ThreadHelper
{
    /// <summary>
    /// 用户棋子数多线程操作
    /// </summary>
    public class UserPoint : BaseThread
    {
        Model.UserPoint model;

        public UserPoint()
        {
            
        }

        public UserPoint(Model.UserPoint model)
        {
            this.model = model;
        }

        /// <summary>
        /// 启用多线程操作
        /// </summary>
        public override void ThreadWork()
        {
            base.IsBackground = true;

            BLL.UserPoint bll = new BLL.UserPoint();

            using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                Model.UserPoint uModel = bll.GetModelByUser(this.model.UserID);
                if (uModel != null)
                {
                    bll.Update(this.model);
                }
                else
                {
                    bll.Insert(model);
                }

                scope.Complete();
            }
        }
    }
}
