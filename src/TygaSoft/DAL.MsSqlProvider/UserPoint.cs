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
    public class UserPoint : IUserPoint
    {
        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Model.UserPoint model)
        {
            if (model == null) return -1;

            string cmdText = "insert into [UserPoint] (UserID,PointNum,LastUpdatedDate) values (@UserID,@PointNum,@LastUpdatedDate)";
            //创建查询命令参数集
            SqlParameter[] parms = {
                                     new SqlParameter("@UserID",SqlDbType.UniqueIdentifier), 
                                     new SqlParameter("@PointNum",SqlDbType.Decimal), 
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)  
                                   };
            parms[0].Value = Guid.Parse(model.UserID.ToString());
            parms[1].Value = model.PointNum;
            parms[2].Value = model.LastUpdatedDate;

            //执行数据库操作
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Model.UserPoint model)
        {
            if (model == null || model.PointNum < 0) return -1;

            //定义查询命令
            string cmdText = @"update [UserPoint] set UserID = @UserID,PointNum = @PointNum,LastUpdatedDate = @LastUpdatedDate where NumberID = @NumberID";

            //创建查询命令参数集
            SqlParameter[] parms = {
                                     new SqlParameter("@NumberID",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@UserID",SqlDbType.UniqueIdentifier), 
                                     new SqlParameter("@PointNum",SqlDbType.Decimal), 
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = Guid.Parse(model.NumberID.ToString());
            parms[1].Value = Guid.Parse(model.UserID.ToString());
            parms[2].Value = model.PointNum;
            parms[3].Value = model.LastUpdatedDate;

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

            string cmdText = "delete from UserPoint where NumberID = @NumberID";
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
                sb.Append(@"delete from [UserPoint] where NumberID = @NumberID" + n + " ;");
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
        public Model.UserPoint GetModel(string numberId)
        {
            Model.UserPoint model = null;

            string cmdText = @"select top 1 NumberID,UserID,PointNum,LastUpdatedDate from [UserPoint] where NumberID = @NumberID order by LastUpdatedDate desc ";
            SqlParameter parm = new SqlParameter("@NumberID", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(numberId);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new Model.UserPoint();
                        model.NumberID = reader["NumberID"].ToString();
                        model.UserID = reader["UserID"].ToString();
                        model.PointNum = Decimal.Parse(reader["PointNum"].ToString());
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
        public IList<Model.UserPoint> GetList(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters)
        {
            //获取数据集总数
            string cmdText = "select count(*) from [UserPoint] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.CreateDate desc) as RowNumber,t1.NumberID,t1.UserID,t1.PointNum,t1.LastUpdatedDate from [UserPoint] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<Model.UserPoint> list = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters))
            {
                if (reader != null && reader.HasRows)
                {
                    list = new List<Model.UserPoint>();

                    while (reader.Read())
                    {
                        Model.UserPoint model = new Model.UserPoint();
                        model.NumberID = reader["NumberID"].ToString();
                        model.UserID = reader["UserID"].ToString();
                        model.PointNum = Decimal.Parse(reader["PointNum"].ToString());
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
            string cmdText = "select count(*) from [UserPoint] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.LastUpdatedDate desc) as RowNumber,t1.NumberID,t1.UserID,t1.PointNum,t1.LastUpdatedDate,u.UserName from [UserPoint] t1 
                       left join Aspnet_Users u on u.UserId = t1.UserId";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            return SqlHelper.ExecuteDataset(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// 获取当前用户的点子数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.UserPoint GetModelByUser(object userId)
        {
            Model.UserPoint model = null;

            string cmdText = @"select top 1 NumberID,UserID,PointNum,LastUpdatedDate from [UserPoint] where UserID = @UserID order by LastUpdatedDate desc ";
            SqlParameter parm = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(userId.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new Model.UserPoint();
                        model.NumberID = reader["NumberID"].ToString();
                        model.UserID = reader["UserID"].ToString();
                        model.PointNum = Decimal.Parse(reader["PointNum"].ToString());
                        model.LastUpdatedDate = DateTime.Parse(reader["LastUpdatedDate"].ToString());

                    }
                }
            }

            return model;
        }
    }
}
