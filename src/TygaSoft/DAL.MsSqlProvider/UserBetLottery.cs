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
    public class UserBetLottery : IUserBetLottery
    {
        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Model.UserBetLottery model)
        {
            if (model == null) return -1;

            string cmdText = "insert into [UserBetLottery] (UserID,RunLotteryID,TotalPointNum,ItemAppend,BetNumAppend,WinPointNum,LastUpdatedDate) values (@UserID,@RunLotteryID,@TotalPointNum,@ItemAppend,@BetNumAppend,@WinPointNum,@LastUpdatedDate)";
            //创建查询命令参数集
            SqlParameter[] parms = {
                                     new SqlParameter("@UserID",SqlDbType.UniqueIdentifier), 
                                     new SqlParameter("@RunLotteryID",SqlDbType.UniqueIdentifier), 
                                     new SqlParameter("@TotalPointNum",SqlDbType.Decimal), 
                                     new SqlParameter("@ItemAppend",SqlDbType.NVarChar,100), 
                                     new SqlParameter("@BetNumAppend",SqlDbType.NVarChar,200), 
                                     new SqlParameter("@WinPointNum",SqlDbType.Decimal), 
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)  
                                   };
            parms[0].Value = Guid.Parse(model.UserID.ToString());
            parms[1].Value = Guid.Parse(model.RunLotteryID.ToString());
            parms[2].Value = model.TotalPointNum;
            parms[3].Value = model.ItemAppend;
            parms[4].Value = model.BetNumAppend;
            parms[5].Value = model.WinPointNum;
            parms[6].Value = model.LastUpdatedDate;

            //执行数据库操作
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Model.UserBetLottery model)
        {
            if (model == null) return -1;

            //定义查询命令
            string cmdText = @"update [UserBetLottery] set UserID = @UserID,RunLotteryID = @RunLotteryID,TotalPointNum = @TotalPointNum,ItemAppend = @ItemAppend,BetNumAppend = @BetNumAppend,WinPointNum = @WinPointNum,LastUpdatedDate = @LastUpdatedDate where NumberID = @NumberID";

            //创建查询命令参数集
            SqlParameter[] parms = {
                                     new SqlParameter("@NumberID",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@UserID",SqlDbType.UniqueIdentifier), 
                                     new SqlParameter("@RunLotteryID",SqlDbType.UniqueIdentifier), 
                                     new SqlParameter("@TotalPointNum",SqlDbType.Decimal), 
                                     new SqlParameter("@ItemAppend",SqlDbType.NVarChar,100), 
                                     new SqlParameter("@BetNumAppend",SqlDbType.NVarChar,200), 
                                     new SqlParameter("@WinPointNum",SqlDbType.Decimal), 
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = Guid.Parse(model.NumberID.ToString());
            parms[1].Value = Guid.Parse(model.UserID.ToString());
            parms[2].Value = Guid.Parse(model.RunLotteryID.ToString());
            parms[3].Value = model.TotalPointNum;
            parms[4].Value = model.ItemAppend;
            parms[5].Value = model.BetNumAppend;
            parms[6].Value = model.WinPointNum;
            parms[7].Value = model.LastUpdatedDate;

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

            string cmdText = "delete from UserBetLottery where NumberID = @NumberID";
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
                sb.Append(@"delete from [UserBetLottery] where NumberID = @NumberID" + n + " ;");
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
        public Model.UserBetLottery GetModel(string numberId)
        {
            Model.UserBetLottery model = null;

            string cmdText = @"select top 1 NumberID,UserID,RunLotteryID,TotalPointNum,ItemAppend,BetNumAppend,WinPointNum,LastUpdatedDate from [UserBetLottery] where NumberID = @NumberID order by LastUpdatedDate desc ";
            SqlParameter parm = new SqlParameter("@NumberID", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(numberId);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new Model.UserBetLottery();
                        model.NumberID = reader["NumberID"].ToString();
                        model.UserID = reader["UserID"].ToString();
                        model.RunLotteryID = reader["RunLotteryID"].ToString();
                        model.TotalPointNum = decimal.Parse(reader["TotalPointNum"].ToString());
                        model.ItemAppend = reader["ItemAppend"].ToString();
                        model.BetNumAppend = reader["BetNumAppend"].ToString();
                        model.WinPointNum = decimal.Parse(reader["WinPointNum"].ToString());
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
        public List<Model.UserBetLottery> GetList(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters)
        {
            //获取数据集总数
            string cmdText = "select count(*) from [UserBetLottery] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.LastUpdatedDate desc) as RowNumber,t1.NumberID,t1.UserID,t1.RunLotteryID,t1.TotalPointNum,t1.ItemAppend,t1.BetNumAppend,t1.WinPointNum,t1.LastUpdatedDate from [UserBetLottery] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<Model.UserBetLottery> list = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters))
            {
                if (reader != null && reader.HasRows)
                {
                    list = new List<Model.UserBetLottery>();

                    while (reader.Read())
                    {
                        Model.UserBetLottery model = new Model.UserBetLottery();
                        model.NumberID = reader["NumberID"].ToString();
                        model.UserID = reader["UserID"].ToString();
                        model.RunLotteryID = reader["RunLotteryID"].ToString();
                        model.TotalPointNum = decimal.Parse(reader["TotalPointNum"].ToString());
                        model.ItemAppend = reader["ItemAppend"].ToString();
                        model.BetNumAppend = reader["BetNumAppend"].ToString();
                        model.WinPointNum = decimal.Parse(reader["WinPointNum"].ToString());
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
            string cmdText = "select count(*) from [UserBetLottery] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.LastUpdatedDate desc) as RowNumber,t1.NumberID,t1.UserID,t1.RunLotteryID,t1.TotalPointNum,t1.ItemAppend,t1.BetNumAppend,t1.WinPointNum,t1.LastUpdatedDate from [UserBetLottery] t1 ";
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
        public bool IsExist(string name, string numberId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public List<Model.UserBetLottery> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] commandParameters)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            string cmdText = @"select * from(select row_number() over(order by t1.LastUpdatedDate desc) as RowNumber,t1.NumberID,t1.UserID,t1.RunLotteryID,t1.TotalPointNum,t1.ItemAppend,t1.BetNumAppend,t1.WinPointNum,t1.LastUpdatedDate from [UserBetLottery] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<Model.UserBetLottery> list = new List<Model.UserBetLottery>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.UserBetLottery model = new Model.UserBetLottery();
                        model.NumberID = reader["NumberID"].ToString();
                        model.UserID = reader["UserID"].ToString();
                        model.RunLotteryID = reader["RunLotteryID"].ToString();
                        model.TotalPointNum = decimal.Parse(reader["TotalPointNum"].ToString());
                        model.ItemAppend = reader["ItemAppend"].ToString();
                        model.BetNumAppend = reader["BetNumAppend"].ToString();
                        model.WinPointNum = decimal.Parse(reader["WinPointNum"].ToString());
                        model.LastUpdatedDate = DateTime.Parse(reader["LastUpdatedDate"].ToString());

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }
}
