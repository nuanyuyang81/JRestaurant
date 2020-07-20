using JRestaurant.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class DrinksPOHandler
    {
        /// <summary>
        /// 添加酒水进货记录
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static bool AddDrinksPO(DrinksPO dp)
        {
            string cmdline = @"INSERT INTO [dbo].[DrinksPO]
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
                new SqlParameter("@amount", dp.Amount),
                new SqlParameter("@vendorid", dp.VendorId),
                new SqlParameter("@ownerid", dp.OwnerId),
                new SqlParameter("@adddate", dp.AddDate),
                new SqlParameter("@weekday", DateTime.Parse(dp.AddDate).DayOfWeek),
                new SqlParameter("@comments", dp.Comments),
                new SqlParameter("@createtime", dp.CreateTime),
                new SqlParameter("@lastupdatetime", dp.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 更新酒水进货记录
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static bool UpdateDrinksPO(DrinksPO dp)
        {
            string cmdline = @"UPDATE [dbo].[DrinksPO]
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
                new SqlParameter("@amount", dp.Amount),
                new SqlParameter("@vendorid", dp.VendorId),
                new SqlParameter("@ownerid", dp.OwnerId),
                new SqlParameter("@adddate", dp.AddDate),
                new SqlParameter("@weekday", DateTime.Parse(dp.AddDate).DayOfWeek),
                new SqlParameter("@comments", dp.Comments),
                new SqlParameter("@lastupdatetime", DateTime.Now),
                new SqlParameter("@id", dp.Id)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 删除酒水进货记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteDrinksPO(int id)
        {
            return CommonHandler.DeleteById(id, "DrinksPO");
        }
        /// <summary>
        /// 分页获取酒水进货记录
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataSet ListDrinksPOByPage(int index, int pageSize)
        {
            return CommonHandler.ListByPage(index, pageSize, "DrinksPO");
        }

        /// <summary>
        /// 按周获取酒水进货记录
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static DataSet ListDrinksPOByWeek(int week)
        {
            string cmdline = @"SELECT f.*, v.[Name] as [VendorName], a.[UserName] FROM [dbo].[DrinksPO] f JOIN [dbo].[Vendor] v ON f.[VendorId] = v.[Id]
                                JOIN [dbo].[Admin] a ON f.[OwnerId] = a.[Id]
                                WHERE f.[CreateTime] > @range";
            SqlParameter[] parameters =
            {
                new SqlParameter("@range", DateTime.Now.AddDays(-7 * week))
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        public static DataSet GetDrinksPOByToDay()
        {
            string cmdline = @"SELECT f.*, v.[Name] as [VendorName], a.[UserName] FROM [dbo].[DrinksPO] f JOIN [dbo].[Vendor] v ON f.[VendorId] = v.[Id]
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
