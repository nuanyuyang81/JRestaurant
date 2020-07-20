using JRestaurant.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class FoodsPOHandler
    {
        /// <summary>
        /// 添加料理进货记录
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static bool AddFoodsPO(FoodsPO fp)
        {
            string cmdline = @"INSERT INTO [dbo].[FoodsPO]
                                       ([Amount]
                                       ,[VendorId]
                                       ,[OwnerId]
                                       ,[AddDate]
                                       ,[WeekDay]
                                       ,[Comments]
                                       ,[CreateTime]
                                       ,[LastUpdateTime])
                                 VALUES
                                       (@amount, @vendorid, @ownerid, @adddate, @weekday, @comments, @createtime, @lastupdatetime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@amount", fp.Amount),
                new SqlParameter("@vendorid", fp.VendorId),
                new SqlParameter("@ownerid", fp.OwnerId),
                new SqlParameter("@adddate", fp.AddDate),
                new SqlParameter("@weekday", DateTime.Parse(fp.AddDate).DayOfWeek),
                new SqlParameter("@comments", fp.Comments),
                new SqlParameter("@createtime", fp.CreateTime),
                new SqlParameter("@lastupdatetime", fp.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 更新料理进货记录
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static bool UpdateFoodsPO(FoodsPO fp)
        {
            string cmdline = @"UPDATE [dbo].[FoodsPO]
                                   SET [Amount] = @amount
                                      ,[VendorId] = @vendorid
                                      ,[OwnerId] = @ownerid
                                      ,[AddDate] = @adddate
                                      ,[WeekDay] = @weekday
                                      ,[Comments] = @comments
                                      ,[LastUpdateTime] = @lastupdatetime
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@amount", fp.Amount),
                new SqlParameter("@vendorid", fp.VendorId),
                new SqlParameter("@ownerid", fp.OwnerId),
                new SqlParameter("@adddate", fp.AddDate),
                new SqlParameter("@weekday", DateTime.Parse(fp.AddDate).DayOfWeek),
                new SqlParameter("@comments", fp.Comments),
                new SqlParameter("@lastupdatetime", DateTime.Now),
                new SqlParameter("@id", fp.Id)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 删除料理进货记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteFoodsPO(int id)
        {
            return CommonHandler.DeleteById(id, "FoodsPO");
        }
        /// <summary>
        /// 分页获取料理进货记录
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataSet ListFoodsPOByPage(int index, int pageSize)
        {
            return CommonHandler.ListByPage(index, pageSize, "FoodsPO");
        }

        /// <summary>
        /// 按周获取料理进货记录
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static DataSet ListFoodsPOByWeek(int week)
        {
            string cmdline = @"SELECT f.*, v.[Name] as [VendorName], a.[UserName] FROM [dbo].[FoodsPO] f JOIN [dbo].[Vendor] v ON f.[VendorId] = v.[Id]
                                JOIN [dbo].[Admin] a ON f.[OwnerId] = a.[Id]
                                WHERE f.[CreateTime] > @range";
            SqlParameter[] parameters =
            {
                new SqlParameter("@range", DateTime.Now.AddDays(-7 * week))
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        public static DataSet GetFoodsPOByToDay()
        {
            string cmdline = @"SELECT f.*, v.[Name] as [VendorName], a.[UserName] FROM [dbo].[FoodsPO] f JOIN [dbo].[Vendor] v ON f.[VendorId] = v.[Id]
                                JOIN [dbo].[Admin] a ON f.[OwnerId] = a.[Id]
                                WHERE f.[CreateTime] > @rangeindex AND f.[CreateTime] < @rangeend";
            SqlParameter[] parameters =
            {
                new SqlParameter("@rangeindex", Convert.ToDateTime(DateTime.Now.ToString("D").ToString())),
                new SqlParameter("@rangeend", Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("D").ToString()).AddSeconds(-1))
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }
    }
}
