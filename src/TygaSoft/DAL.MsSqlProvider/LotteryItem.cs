using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LotterySln.IDAL;
using DBUtility;

namespace LotterySln.DAL.MsSqlProvider
{
    public class LotteryItem : ILotteryItem
    {
        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Model.LotteryItem model)
        {
            if (model == null) return -1;

            //判断当前记录是否存在，如果存在则返回;
            if (IsExist(model.ItemName, null)) return 110;

            string cmdText = "insert into [LotteryItem] (ItemName,ItemCode,ImageUrl,FixRatio,LastUpdatedDate) values (@ItemName,@ItemCode,@ImageUrl,@FixRatio,@LastUpdatedDate)";
            //创建查询命令参数集
            SqlParameter[] parms = {
                                     new SqlParameter("@ItemName",SqlDbType.NVarChar,50), 
                                     new SqlParameter("@ItemCode",SqlDbType.NVarChar,50), 
                                     new SqlParameter("@ImageUrl",SqlDbType.NVarChar,300), 
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                     new SqlParameter("@FixRatio",SqlDbType.Decimal)
                                   };
            parms[0].Value = model.ItemName;
            parms[1].Value = model.ItemCode;
            parms[2].Value = model.ImageUrl;
            parms[3].Value = model.LastUpdatedDate;
            parms[4].Value = model.FixRatio;

            //执行数据库操作
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Model.LotteryItem model)
        {
            if (model == null) return -1;

            if (IsExist(model.ItemName, model.NumberID)) return 110;

            //定义查询命令
            string cmdText = @"update [LotteryItem] set ItemName = @ItemName,ItemCode = @ItemCode,ImageUrl = @ImageUrl,FixRatio = @FixRatio,LastUpdatedDate = @LastUpdatedDate where NumberID = @NumberID";

            //创建查询命令参数集
            SqlParameter[] parms = {
                                     new SqlParameter("@NumberID",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@ItemName",SqlDbType.NVarChar,50), 
                                     new SqlParameter("@ItemCode",SqlDbType.NVarChar,50), 
                                     new SqlParameter("@ImageUrl",SqlDbType.NVarChar,300), 
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                     new SqlParameter("@FixRatio",SqlDbType.Decimal)
                                   };
            parms[0].Value = Guid.Parse(model.NumberID.ToString());
            parms[1].Value = model.ItemName;
            parms[2].Value = model.ItemCode;
            parms[3].Value = model.ImageUrl;
            parms[4].Value = model.LastUpdatedDate;
            parms[5].Value = model.FixRatio;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public int Delete(string numberId)
        {
            if (string.IsNullOrEmpty(numberId)) return -1;

            string cmdText = "delete from LotteryItem where NumberID = @NumberID";
            SqlParameter parm = new SqlParameter("@NumberID", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(numberId);

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm);
        }

        /// <summary>
        /// 批量删除数据（启用事务）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteBatch(IList<string> list)
        {
            if (list == null || list.Count == 0) return false;

            bool result = false;
            StringBuilder sb = new StringBuilder();
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from [LotteryItem] where NumberID = @NumberID" + n + " ;");
                SqlParameter parm = new SqlParameter("@NumberID" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.SqlProviderConnString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null);
                        tran.Commit();
                        if (effect > 0) result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public Model.LotteryItem GetModel(string numberId)
        {
            Model.LotteryItem model = null;

            string cmdText = @"select top 1 NumberID,ItemName,ItemCode,ImageUrl,FixRatio,LastUpdatedDate from [LotteryItem] where NumberID = @NumberID order by LastUpdatedDate desc ";
            SqlParameter parm = new SqlParameter("@NumberID", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(numberId);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new Model.LotteryItem();
                        model.NumberID = reader["NumberID"].ToString();
                        model.ItemName = reader["ItemName"].ToString();
                        model.ItemCode = reader["ItemCode"].ToString();
                        model.ImageUrl = reader["ImageUrl"].ToString();
                        model.FixRatio = decimal.Parse(reader["FixRatio"].ToString());
                        model.LastUpdatedDate = DateTime.Parse(reader["LastUpdatedDate"].ToString());
                    }
                }
            }

            return model;
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
        public List<Model.LotteryItem> GetList(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters)
        {
            //获取数据集总数
            string cmdText = "select count(*) from [LotteryItem] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.LastUpdatedDate desc) as RowNumber,t1.NumberID,t1.ItemName,t1.ItemCode,t1.ImageUrl,FixRatio,t1.LastUpdatedDate from [LotteryItem] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<Model.LotteryItem> list = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters))
            {
                if (reader != null && reader.HasRows)
                {
                    list = new List<Model.LotteryItem>();

                    while (reader.Read())
                    {
                        Model.LotteryItem model = new Model.LotteryItem();
                        model.NumberID = reader["NumberID"].ToString();
                        model.ItemName = reader["ItemName"].ToString();
                        model.ItemCode = reader["ItemCode"].ToString();
                        model.ImageUrl = reader["ImageUrl"].ToString();
                        model.FixRatio = decimal.Parse(reader["FixRatio"].ToString());
                        model.LastUpdatedDate = DateTime.Parse(reader["LastUpdatedDate"].ToString());

                        list.Add(model);
                    }
                }
            }

            return list;
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
            //获取数据集总数
            string cmdText = "select count(*) from [LotteryItem] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.LastUpdatedDate desc) as RowNumber,t1.NumberID,t1.ItemName,t1.ItemCode,t1.ImageUrl,FixRatio,t1.LastUpdatedDate from [LotteryItem] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            return SqlHelper.ExecuteDataset(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// 是否存在对应数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public bool IsExist(string name, object numberId)
        {
            bool isExist = false;
            int totalCount = -1;

            ParamsHelper parms = new ParamsHelper();

            string cmdText = "select count(*) from [LotteryItem] where ItemName = @ItemName";
            if (numberId != null)
            {
                cmdText = "select count(*) from [LotteryItem] where ItemName = @ItemName and NumberID <> @NumberID ";
                SqlParameter parm1 = new SqlParameter("@NumberID", SqlDbType.UniqueIdentifier);
                parm1.Value = Guid.Parse(numberId.ToString());
                parms.Add(parm1);
            }
            SqlParameter parm = new SqlParameter("@ItemName", SqlDbType.NVarChar, 50);
            parm.Value = name;
            parms.Add(parm);

            object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms.ToArray());
            if (obj != null) totalCount = Convert.ToInt32(obj);
            if (totalCount > 0) isExist = true;

            return isExist;
        }

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public List<Model.LotteryItem> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            string cmdText = @"select * from(select row_number() over(order by t1.LastUpdatedDate desc) as RowNumber,t1.NumberID,t1.ItemName,t1.ItemCode,t1.ImageUrl,FixRatio,t1.LastUpdatedDate from [LotteryItem] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<Model.LotteryItem> list = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    list = new List<Model.LotteryItem>();

                    while (reader.Read())
                    {
                        Model.LotteryItem model = new Model.LotteryItem();
                        model.NumberID = reader["NumberID"].ToString();
                        model.ItemName = reader["ItemName"].ToString();
                        model.ItemCode = reader["ItemCode"].ToString();
                        model.ImageUrl = reader["ImageUrl"].ToString();
                        model.FixRatio = decimal.Parse(reader["FixRatio"].ToString());
                        model.LastUpdatedDate = DateTime.Parse(reader["LastUpdatedDate"].ToString());

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }
}
