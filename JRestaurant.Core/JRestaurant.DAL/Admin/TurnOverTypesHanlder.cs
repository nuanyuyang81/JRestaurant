using JRestaurant.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class TurnOverTypesHanlder
    {
        /// <summary>
        /// 添加营业额分类信息， 
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        public static bool AddTurnOverTypes(TurnOverTypes tt)
        {
            string cmdline = @"INSERT INTO [dbo].[TurnOverTypes]
                                    ([Name]
                                    ,[Active]
                                    ,[AimType]
                                    ,[OwnerId]
                                    ,[Comments]
                                    ,[CreateTime]
                                    ,[LastUpdateTime])
                                 VALUES
                                     (@name, @active, @aimtype, @ownerid, @comments, @createtime, @lastupdatetime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@name", tt.Name),
                new SqlParameter("@active", tt.Active),
                new SqlParameter("@aimtype", tt.AimType),
                new SqlParameter("@ownerid", tt.OwnerId),
                new SqlParameter("@comments", tt.Comments),
                new SqlParameter("@createtime", tt.CreateTime),
                new SqlParameter("@lastupdatetime", tt.LastUpdateTime)
            };

            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 禁用营业额种类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public static bool InActiveTurnOverTypes(int id, Guid ownerId)
        {
            string cmdline = @"UPDATE [dbo].[TurnOverTypes]
                                   SET [Active] = @status
                                      ,[OwnerId] = @ownerid
                                      ,[LastUpdateTime] = @lastupdatetime
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status", false),
                new SqlParameter("@ownerid", ownerId),
                new SqlParameter("@lastupdatetime", DateTime.Now)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 更新营业额分类信息
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        public static bool UpdateTurnOverTypes(TurnOverTypes tt)
        {
            string cmdline = @"UPDATE [dbo].[TurnOverTypes]
                                       SET [Name] = @name
                                          ,[AimType] = @aimtype
                                          ,[OwnerId] = @ownerid
                                          ,[Comments] = @comments
                                          ,[LastUpdateTime] = @lastupdatetime
                                     WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@name", tt.Name),
                new SqlParameter("@aimtype", tt.AimType),
                new SqlParameter("@ownerid", tt.OwnerId),
                new SqlParameter("@comments", tt.Comments),
                new SqlParameter("@lastupdatetime", DateTime.Now),
                new SqlParameter("@id", tt.Id)
            };

            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 获取所有正常状态的营业额分类信息
        /// </summary>
        /// <returns></returns>
        public static DataSet ListAllTurnOverTypes()
        {
            string cmdline = @"SELECT [Id]
                                      ,[Name]
                                      ,[AimType]
                                      ,[OwnerId]
                                      ,[Comments]
                                      ,[CreateTime]
                                      ,[LastUpdateTime]
                                  FROM [dbo].[TurnOverTypes]
                                  WHERE [Active] = @status";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status", true)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        /// <summary>
        /// 根据适用营业额场景获取营业额分类信息
        /// </summary>
        /// <param name="aim"></param>
        /// <returns></returns>
        public static DataSet ListTurnOverTypeByAim(int aim)
        {
            string cmdline = @"SELECT [Id]
                                      ,[Name]
                                      ,[AimType]
                                      ,[OwnerId]
                                      ,[Comments]
                                      ,[CreateTime]
                                      ,[LastUpdateTime]
                                  FROM [dbo].[TurnOverTypes]
                                  WHERE [Active] = @status AND ([AimType]=@aim OR [AimType]=2)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status", true),
                new SqlParameter("@aim", aim)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }
    }
}
