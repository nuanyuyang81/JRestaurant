using JRestaurant.Entities;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class LogHandler
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static bool AddLog(Logs log)
        {
            string cmdline = @"INSERT INTO [dbo].[Logs]
                                       ([Action]
                                       ,[Table]
                                       ,[Message]
                                       ,[OwnerId]
                                       ,[CreateTime])
                                 VALUES
                                       (@action, @table, @message, @ownerid, @createtime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@action", log.Action),
                new SqlParameter("@table", log.Table),
                new SqlParameter("@message", log.Message),
                new SqlParameter("@ownerid", log.OwnerId),
                new SqlParameter("@createtime", log.CreateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteLog(int id)
        {
            return CommonHandler.DeleteById(id, "Logs");
        }

        /// <summary>
        /// 分页获取全部种类日志
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataSet ListFullLogsByPage(int index, int pageSize)
        {
            string cmdline = @"SELECT * FROM [dbo].[Logs]
                                    ORDER BY [Id] DESC  
                                    OFFSET @offset ROWS
                                    FETCH NEXT @next ROWS ONLY";
            SqlParameter[] parameters =
            {
                new SqlParameter("@offset", (index - 1) * pageSize),
                new SqlParameter("@next", pageSize)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        /// <summary>
        /// 分页获取不同数据表日志
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataSet ListTableLogsByPage(int index, int pageSize, string table)
        {
            string cmdline = @"SELECT * FROM [dbo].[Logs] WHERE [Table] = @table
                                    ORDER BY [Id] DESC  
                                    OFFSET @offset ROWS
                                    FETCH NEXT @next ROWS ONLY";
            SqlParameter[] parameters =
            {
                new SqlParameter("@offset", (index - 1) * pageSize),
                new SqlParameter("@next", pageSize),
                new SqlParameter("@table", table)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        /// <summary>
        /// 获取全部日志总数
        /// </summary>
        /// <returns></returns>
        public static int GetTotalFullCount()
        {
            return CommonHandler.GetTotalCount("Logs");
        }

        /// <summary>
        /// 获取不同种类日志总数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int GetTotalCountByTable(string table)
        {
            string cmdline = @"SELECT COUNT([Id]) AS [RowCount]
                                  FROM [dbo].[Logs]
                                  WHERE [Table] = @table";
            SqlParameter[] parameters =
            {
                new SqlParameter("@table", table)
            };
            return SqlHelper.GetCount(cmdline, parameters);
        }
    }
}
