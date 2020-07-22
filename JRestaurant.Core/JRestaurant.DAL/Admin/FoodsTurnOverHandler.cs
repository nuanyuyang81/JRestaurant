using JRestaurant.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class FoodsTurnOverHandler
    {
        /// <summary>
        /// 添加料理营业额记录
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        public static bool AddFoodsTurnOver(FoodsTurnOver ft)
        {
            string cmdline = @"INSERT INTO [dbo].[FoodsTurnOver]
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
                new SqlParameter("@amount", ft.Amount),
                new SqlParameter("@discount", ft.Discount),
                new SqlParameter("@typeid", ft.TypeId),
                new SqlParameter("@adddate", ft.AddDate),
                new SqlParameter("@weekday", DateTime.Parse(ft.AddDate).DayOfWeek),
                new SqlParameter("@ownerid", ft.OwnerId),
                new SqlParameter("@comments", ft.Comments),
                new SqlParameter("@createtime", ft.CreateTime),
                new SqlParameter("@lastupdatetime", ft.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 更改料理营业额记录
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        public static bool UpdateFoodsTurnOver(FoodsTurnOver ft)
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
                new SqlParameter("@id", ft.Id),
                new SqlParameter("@amount", ft.Amount),
                new SqlParameter("@discount", ft.Discount),
                new SqlParameter("@typeid", ft.TypeId),
                new SqlParameter("@adddate", ft.AddDate),
                new SqlParameter("@weekday", DateTime.Parse(ft.AddDate).DayOfWeek),
                new SqlParameter("@ownerid", ft.OwnerId),
                new SqlParameter("@comments", ft.Comments),
                new SqlParameter("@createtime", ft.CreateTime),
                new SqlParameter("@lastupdatetime", ft.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 删除料理营业额记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteFoodsTurnOver(int id)
        {
            return CommonHandler.DeleteById(id, "FoodsTurnOver");
        }

        /// <summary>
        /// 分页获取料理营业额记录
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataSet ListFoodsTurnOverByPage(int index, int pageSize)
        {
            return CommonHandler.ListByPage(index, pageSize, "FoodsTurnOver");
        }


        /// <summary>
        /// 按周获取料理营业额
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static DataSet ListFoodsTurnOverByWeek(int week)
        {
            string cmdline = @"SELECT f.*, a.[UserName], t.[Name] as [TypeName] FROM [dbo].[FoodsTurnOver] f JOIN [dbo].[TurnOverTypes] t ON f.[TypeId]=t.[Id]
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
        /// 获取当日料理营业额
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static DataSet ListFoodsTurnOverToday()
        {
            string cmdline = @"SELECT f.*, a.[UserName], t.[Name] as [TypeName] FROM [dbo].[FoodsTurnOver] f JOIN [dbo].[TurnOverTypes] t ON f.[TypeId]=t.[Id]
                                JOIN [dbo].[Admin] a ON f.[OwnerId] = a.[Id]
                                WHERE f.[AddDate] > @rangeindex AND f.[AddDate] < @rangeend";
            SqlParameter[] parameters =
            {
                new SqlParameter("@rangeindex", Convert.ToDateTime(DateTime.Now.ToString("D").ToString())),
                new SqlParameter("@rangeend", Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("D").ToString()).AddSeconds(-1))
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }
    }
}
