using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LotterySln.IDAL
{
    public interface IRunLottery
    {
        #region 成员方法

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(Model.RunLottery model);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(Model.RunLottery model);

		/// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        int Delete(string numberId);

        /// <summary>
        /// 批量删除数据（启用事务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool DeleteBatch(IList<string> list);

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        Model.RunLottery GetModel(string numberId);

		/// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        List<Model.RunLottery> GetList(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters);

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        DataSet GetDataSet(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters);

        /// <summary>
        /// 获取开奖记录数据集
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        DataSet GetRunLotteryItem(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters);

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        List<Model.RunLottery> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}