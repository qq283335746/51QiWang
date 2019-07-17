using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LotterySln.IDAL;

namespace LotterySln.BLL
{
    public class ContentDetail
    {
        private static readonly IContentDetail dal = DALFactory.DataAccess.CreateContentDetail();

        #region 成员方法

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Model.ContentDetail model)
        {
            return dal.Insert(model);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Model.ContentDetail model)
        {
            return dal.Update(model);
        }

		/// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public int Delete(object numberId)
        {
            return dal.Delete(numberId);
        }

        /// <summary>
        /// 批量删除数据（启用事务）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteBatch(IList<string> list)
        {
            return dal.DeleteBatch(list);
        }

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public Model.ContentDetail GetModel(object numberId)
        {
            return dal.GetModel(numberId);
        }

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet GetDataSet(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters)
        {
            return dal.GetDataSet(pageIndex, pageSize, out totalCount, sqlWhere, commandParameters);
        }

		/// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public IList<Model.ContentDetail> GetList(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters)
        {
            return dal.GetList(pageIndex, pageSize, out totalCount, sqlWhere, commandParameters);
        }

        /// <summary>
        /// 获取当前内容类型的内容明细
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetKeyValueByType(string typeName)
        {
            return dal.GetKeyValueByType(typeName);
        }

        /// <summary>
        /// 获取当前内容类型ID的子项
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetKeyValueByTypeID(object typeId)
        {
            return dal.GetKeyValueByTypeID(typeId);
        }

        /// <summary>
        /// 获取特定于当前起始位置和结束位置的数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet GetDataSet(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] commandParameters)
        {
            return dal.GetDataSet(pageIndex, pageSize, sqlWhere, commandParameters);
        }

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public IList<Model.ContentDetail> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        #endregion
    }
}