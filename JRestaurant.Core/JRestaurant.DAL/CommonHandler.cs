using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL
{
    public class CommonHandler
    {
        /// <summary>
        /// 分页查询信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet ListByPage(int index, int pageSize, string tableName)
        {
            string cmdline = @"SELECT * FROM [dbo].[@table]
                                    ORDER BY [Id] DESC  
                                    OFFSET @offset ROWS
                                    FETCH NEXT @next ROWS ONLY";
            SqlParameter[] parameters =
            {
                new SqlParameter("@offset", (index - 1) * pageSize),
                new SqlParameter("@next", pageSize),
                new SqlParameter("@table", tableName)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        /// <summary>
        /// 获取数据表内总行数
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static int GetTotalCount(string tableName)
        {
            string cmdline = @"SELECT i.ROWS AS [RowCount]
                                FROM sys.tables AS t, sysindexes AS i
                                WHERE t.object_id = i.id AND i.indid <=1 AND t.name = @table";
            using (SqlConnection conn = SqlHelper.DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddWithValue("@table", tableName);
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["RowCount"].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 根据Id删除记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool DeleteById(object id, string tableName)
        {
            string cmdline = "DELETE FROM [dbo].["+tableName+"] WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 设置为禁用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public static bool InActive(object id, string tableName, Guid ownerId)
        {
            string cmdline = "UPDATE [dbo].["+tableName+"]"+
                                   @"SET [Active] = @status
                                      ,[OwnerId] = @ownerId
                                      ,[LastUpdateTime] = @lastupdatetime
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status", false),
                new SqlParameter("@ownerId", ownerId),
                new SqlParameter("@id", id),
                new SqlParameter("@lastupdatetime", DateTime.Now)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// DataSet转化为JOjbect
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static JObject TransferObjFromDS(DataSet ds)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                JArray resultarray = JArray.FromObject(dt, JsonSerializer.CreateDefault(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                if (resultarray.Count>0)
                {
                    JObject obj = (JObject)resultarray[0];
                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// Dataset 转化为JArray
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static JArray TransferArrayFromDS(DataSet ds)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                JArray resultarray = JArray.FromObject(dt, JsonSerializer.CreateDefault(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                return resultarray;
            }
            return null;
        }
    }
}
