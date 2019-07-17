using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LotterySln.IDAL;

namespace LotterySln.BLL
{
    public class ContentType
    {
        private static readonly IContentType dal = DALFactory.DataAccess.CreateContentType();

        #region 成员方法

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Model.ContentType model)
        {
            return dal.Insert(model);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Model.ContentType model)
        {
            return dal.Update(model);
        }

		/// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public int Delete(string numberId)
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
        public Model.ContentType GetModel(string numberId)
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
        public List<Model.ContentType> GetList(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters)
        {
            return dal.GetList(pageIndex, pageSize, out totalCount, sqlWhere, commandParameters);
        }

        /// <summary>
        /// 获取树json格式字符串
        /// </summary>
        /// <returns></returns>
        public string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            int totalCount = 0;
            List<Model.ContentType> list = dal.GetList(1, 10000, out totalCount,"",null);
            if (list != null && list.Count > 0)
            {
                GetTreeJson(list, Guid.Empty, ref jsonAppend);
            }
            else
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty.ToString("N") + "\",\"text\":\"请选择\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + Guid.Empty.ToString("N") + "\",\"parentName\":\"请选择\"}}]");
            }

            return jsonAppend.ToString();
        }

        /// <summary>
        /// 获取树json格式字符串
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentId"></param>
        /// <param name="jsonAppend"></param>
        private void GetTreeJson(List<Model.ContentType> list, object parentId, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentID.Equals(parentId));
            if (childList.Count > 0)
            {
                int temp = 0;
                foreach (var model in childList)
                {
                    jsonAppend.Append("{\"id\":\"" + model.NumberID + "\",\"text\":\"" + model.TypeName + "\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + model.ParentID + "\",\"parentName\":\"" + model.ParentName + "\"}");
                    if (list.Any(r => r.ParentID.Equals(model.NumberID)))
                    {
                        jsonAppend.Append(",\"children\":");
                        GetTreeJson(list, model.NumberID, ref jsonAppend);
                    }
                    jsonAppend.Append("}");
                    if (temp < childList.Count - 1) jsonAppend.Append(",");
                    temp++;
                }
            }
            jsonAppend.Append("]");
        }  

        #endregion
    }
}