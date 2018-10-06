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
    public class RunLottery : IRunLottery
    {
        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Model.RunLottery model)
        {
            if (model == null) return -1;

            string cmdText = "insert into [RunLottery] (LotteryNum,LastUpdatedDate,Status,RunDate) values (@LotteryNum,@LastUpdatedDate,@Status,@RunDate)";
            //创建查询命令参数集
            SqlParameter[] parms = {
                                     new SqlParameter("@LotteryNum",SqlDbType.NVarChar,50), 
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                     new SqlParameter("@Status",SqlDbType.TinyInt),
                                     new SqlParameter("@RunDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.LotteryNum;
            parms[1].Value = model.LastUpdatedDate;
            parms[2].Value = model.Status;
            parms[3].Value = model.RunDate;

            //执行数据库操作
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Model.RunLottery model)
        {
            if (model == null) return -1;

            //定义查询命令
            string cmdText = @"update [RunLottery] set LotteryNum = @LotteryNum,Status = @Status,LastUpdatedDate = @LastUpdatedDate,BetNum = @BetNum,TotalPointNum = @TotalPointNum,WinnerNum = @WinnerNum,WinPointNum = @WinPointNum where NumberID = @NumberID";

            //创建查询命令参数集
            SqlParameter[] parms = {
                                     new SqlParameter("@NumberID",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@LotteryNum",SqlDbType.NVarChar,50), 
                                     new SqlParameter("@Status",SqlDbType.TinyInt),
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                     new SqlParameter("@BetNum",SqlDbType.Int),
                                     new SqlParameter("@TotalPointNum",SqlDbType.Decimal),
                                     new SqlParameter("@WinnerNum",SqlDbType.Int),
                                     new SqlParameter("@WinPointNum",SqlDbType.Decimal),
                                   };
            parms[0].Value = Guid.Parse(model.NumberID.ToString());
            parms[1].Value = model.LotteryNum;
            parms[2].Value = model.Status;
            parms[3].Value = model.LastUpdatedDate;
            parms[4].Value = model.BetNum;
            parms[5].Value = model.TotalPointNum;
            parms[6].Value = model.WinnerNum;
            parms[7].Value = model.WinPointNum;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public int Delete(string numberId)
        {
            Guid gId = Guid.Empty;
            Guid.TryParse(numberId, out gId);
            if (gId == Guid.Empty) return -1;

            string cmdText = "delete from RunLottery where NumberID = @NumberID";
			SqlParameter parm = new SqlParameter("@NumberID",SqlDbType.UniqueIdentifier);
            parm.Value = gId;

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
                sb.Append(@"delete from [RunLottery] where NumberID = @NumberID"+n+" ;");
                SqlParameter parm = new SqlParameter("@NumberID"+n+"", SqlDbType.UniqueIdentifier);
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
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(),parms != null ? parms.ToArray():null);
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
        public Model.RunLottery GetModel(string numberId)
        {
            Model.RunLottery model = null;

            string cmdText = @"select top 1 NumberID,Period,LotteryNum,LastUpdatedDate,Status,RunDate,BetNum,TotalPointNum,WinnerNum,WinPointNum from [RunLottery] where NumberID = @NumberID ";
            SqlParameter parm = new SqlParameter("@NumberID", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(numberId);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
					    model = new Model.RunLottery();
                        model.NumberID = reader["NumberID"].ToString();
                        model.Period = int.Parse(reader["Period"].ToString());
                        model.LotteryNum = reader["LotteryNum"].ToString();
                        model.LastUpdatedDate = DateTime.Parse(reader["LastUpdatedDate"].ToString());
                        model.RunDate = DateTime.Parse(reader["RunDate"].ToString());
                        model.BetNum = Int32.Parse(reader["BetNum"].ToString());
                        model.TotalPointNum = decimal.Parse(reader["TotalPointNum"].ToString());
                        model.WinnerNum = Int32.Parse(reader["WinnerNum"].ToString());
                        model.WinPointNum = decimal.Parse(reader["WinPointNum"].ToString());
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
        public List<Model.RunLottery> GetList(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] cmdParms)
        {
            //获取数据集总数
            string cmdText = "select count(*) from [RunLottery] t1 left join dbo.LotteryItem t2 on t2.ItemCode = t1.LotteryNum ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.Period desc) as RowNumber,t1.NumberID,t1.Period,t1.LotteryNum,t1.LastUpdatedDate,t1.Status,t1.RunDate,t1.BetNum,t1.TotalPointNum,t1.WinnerNum,t1.WinPointNum,t2.ItemName from [RunLottery] t1 
                      left join dbo.LotteryItem t2 on t2.ItemCode = t1.LotteryNum";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<Model.RunLottery> list = new List<Model.RunLottery>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.RunLottery model = new Model.RunLottery();
                        model.NumberID = Guid.Parse(reader["NumberID"].ToString());
                        model.Period = int.Parse(reader["Period"].ToString());
                        model.LotteryNum = reader["LotteryNum"].ToString();
                        model.RunDate = DateTime.Parse(reader["RunDate"].ToString());
                        model.LastUpdatedDate = DateTime.Parse(reader["LastUpdatedDate"].ToString());
                        model.Status = short.Parse(reader["Status"].ToString());
                        model.RunDate = DateTime.Parse(reader["RunDate"].ToString());
                        model.BetNum = Int32.Parse(reader["BetNum"].ToString());
                        model.TotalPointNum = decimal.Parse(reader["TotalPointNum"].ToString());
                        model.WinnerNum = Int32.Parse(reader["WinnerNum"].ToString());
                        model.WinPointNum = decimal.Parse(reader["WinPointNum"].ToString());
                        model.ItemName = reader["ItemName"].ToString();

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
            string cmdText = "select count(*) from [RunLottery] t1 ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.Period desc) as RowNumber,t1.NumberID,t1.Period,t1.LotteryNum,t1.LastUpdatedDate,t1.Status,t1.RunDate,t1.BetNum,t1.TotalPointNum,t1.WinnerNum,t1.WinPointNum from [RunLottery] t1 ";
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
        /// 获取开奖记录数据集
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet GetRunLotteryItem(int pageIndex, int pageSize, out int totalCount, string sqlWhere, params SqlParameter[] commandParameters)
        {
            //获取数据集总数
            string cmdText = "select count(*) from RunLottery t1 left join dbo.LotteryItem t2 on t2.ItemCode = t1.LotteryNum ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
            //返回分页数据
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            cmdText = @"select * from(select row_number() over(order by t1.Period desc) as RowNumber,t1.NumberID, t1.Period,t2.ItemName,t1.LastUpdatedDate,t1.Status,t1.RunDate,t1.BetNum,t1.TotalPointNum,t1.WinnerNum,t1.WinPointNum from RunLottery t1
                      left join dbo.LotteryItem t2 on t2.ItemCode = t1.LotteryNum ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            return SqlHelper.ExecuteDataset(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public List<Model.RunLottery> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;
            string cmdText = @"select * from(select row_number() over(order by t1.Period desc) as RowNumber,t1.NumberID,t1.Period,t1.LotteryNum,t1.LastUpdatedDate,t1.Status,t1.RunDate,t1.BetNum,t1.TotalPointNum,t1.WinnerNum,t1.WinPointNum,t2.ItemName from [RunLottery] t1 
                              left join dbo.LotteryItem t2 on t2.ItemCode = t1.LotteryNum";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<Model.RunLottery> list = new List<Model.RunLottery>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.RunLottery model = new Model.RunLottery();
                        model.NumberID = Guid.Parse(reader["NumberID"].ToString());
                        model.Period = int.Parse(reader["Period"].ToString());
                        model.LotteryNum = reader["LotteryNum"].ToString();
                        model.RunDate = DateTime.Parse(reader["RunDate"].ToString());
                        model.LastUpdatedDate = DateTime.Parse(reader["LastUpdatedDate"].ToString());
                        model.Status = short.Parse(reader["Status"].ToString());
                        model.RunDate = DateTime.Parse(reader["RunDate"].ToString());
                        model.BetNum = Int32.Parse(reader["BetNum"].ToString());
                        model.TotalPointNum = decimal.Parse(reader["TotalPointNum"].ToString());
                        model.WinnerNum = Int32.Parse(reader["WinnerNum"].ToString());
                        model.WinPointNum = decimal.Parse(reader["WinPointNum"].ToString());
                        model.ItemName = reader["ItemName"].ToString();

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }
}
