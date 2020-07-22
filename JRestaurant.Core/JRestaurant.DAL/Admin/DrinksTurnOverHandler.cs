using JRestaurant.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class DrinksTurnOverHandler
    {
        /// <summary>
        /// 添加酒水营业额记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool AddDrinksTurnOver(DrinksTurnOver dt)
        {
            string cmdline = @"INSERT INTO [dbo].[DrinksTurnOver]
                                        ([Amount]
                                        ,[Discount]
                                        ,[TypeId]
                                        ,[AddDate]
                                        ,[WeekDay]
                                        ,[OwnerId]
                                        ,[Comments]
                                        ,[CreateTime]
                                        ,[LastUpdateTime])
                                    VALUES
                                        (@amount, @discount, @typeid, @adddate, @weekday, @ownerid, @comments, @createtime, @lastupdatetime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@amount", dt.Amount),
                new SqlParameter("@discount", dt.Discount),
                new SqlParameter("@typeid", dt.TypeId),
                new SqlParameter("@adddate", dt.AddDate),
                new SqlParameter("@weekday", DateTime.Parse(dt.AddDate).DayOfWeek),
                new SqlParameter("@ownerid", dt.OwnerId),
                new SqlParameter("@comments", dt.Comments),
                new SqlParameter("@createtime", dt.CreateTime),
                new SqlParameter("@lastupdatetime", dt.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);

        }

        /// <summary>
        /// 更改酒水营业额记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool UpdateDrinksTurnOver(DrinksTurnOver dt)
        {
            string cmdline = @"UPDATE [dbo].[FoodsTurnOver]
                                   SET [Amount] = @amount
                                      ,[Discount] = @discount
                                      ,[TypeId] = @typeid
                                      ,[AddDate] = @adddate
                                      ,[WeekDay] = @weekday
                                      ,[OwnerId] = @ownerid
                                      ,[Comments] = @comments
                                      ,[CreateTime] = @createtime
                                      ,[LastUpdateTime] = @lastupdatetime
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", dt.Id),
                new SqlParameter("@amount", dt.Amount),
                new SqlParameter("@discount", dt.Discount),
                new SqlParameter("@typeid", dt.TypeId),
                new SqlParameter("@adddate", dt.AddDate),
                new SqlParameter("@weekday", DateTime.Parse(dt.AddDate).DayOfWeek),
                new SqlParameter("@ownerid", dt.OwnerId),
                new SqlParameter("@comments", dt.Comments),
                new SqlParameter("@createtime", dt.CreateTime),
                new SqlParameter("@lastupdatetime", dt.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 删除酒水营业额记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteDrinksTurnOver(int id)
        {
            return CommonHandler.DeleteById(id, "DrinksTurnOver");
        }

        /// <summary>
        /// 分页获取酒水营业额记录
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataSet ListDrinksTurnOverByPage(int index, int pageSize)
        {
            return CommonHandler.ListByPage(index, pageSize, "DrinksTurnOver");
        }

        /// <summary>
        /// 按周获取酒水营业额
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static DataSet ListDrinksTurnOverByWeek(int week)
        {
            string cmdline = @"SELECT f.*, a.[UserName], t.[Name] as [TypeName] FROM [dbo].[DrinksTurnOver] f JOIN [dbo].[TurnOverTypes] t ON f.[TypeId]=t.[Id]
                                JOIN [dbo].[Admin] a ON f.[OwnerId] = a.[Id]
                                WHERE f.[AddDate] >= @range AND f.[AddDate] < @rangeend ORDER BY f.[AddDate] asc";
            DateTime weekstart = CommonHandler.GetWeekStartTime();
            SqlParameter[] parameters =
            {
                new SqlParameter("@range", week == 1?weekstart:weekstart.AddDays(-7*(week-1))),
                new SqlParameter("@rangeend", week == 1?weekstart.AddDays(7):weekstart.AddDays(-7*(week-2)))
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }
        /// <summary>
        /// 获取当日酒水营业额
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static DataSet ListDrinksTurnOverToday()
        {
            string cmdline = @"SELECT f.*, a.[UserName], t.[Name] as [TypeName] FROM [dbo].[DrinksTurnOver] f JOIN [dbo].[TurnOverTypes] t ON f.[TypeId]=t.[Id]
                                JOIN [dbo].[Admin] a ON f.[OwnerId] = a.[Id]
                                WHERE f.[AddDate] > @rangeindex AND f.[CreateTime] < @rangeend";
            SqlParameter[] parameters =
            {
                new SqlParameter("@rangeindex", Convert.ToDateTime(DateTime.Now.ToString("D").ToString())),
                new SqlParameter("@rangeend", Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("D").ToString()).AddSeconds(-1))
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

    }
}
