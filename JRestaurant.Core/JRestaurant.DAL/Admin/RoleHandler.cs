using JRestaurant.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class RoleHandler
    {
        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool AddRole(Roles role)
        {
            string cmdline = @"INSERT INTO [dbo].[Roles]
                                       ([Active]
                                       ,[Key]
                                       ,[Name]
                                       ,[OwnerId]
                                       ,[Comments]                                
                                       ,[CreateTime]
                                       ,[LastUpdateTime])
                                 VALUES
                                       (@active, @key, @name, @ownerid, @comments, @createtime, @lastupdatetime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@active", role.Active),
                new SqlParameter("@key", role.Key),
                new SqlParameter("@name", role.Name),
                new SqlParameter("@ownerid", role.OwnerId),
                new SqlParameter("@comments", role.Comments),
                new SqlParameter("@createtime", role.CreateTime),
                new SqlParameter("@lastupdatetime", role.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 禁用角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public static bool InActiveRole(int roleId, Guid ownerId)
        {
            return CommonHandler.InActive(roleId, "Roles", ownerId);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool UpdateRoleInfo(Roles role)
        {
            string cmdline = @"UPDATE [dbo].[Roles]
                                   SET [Name] = @name
                                      ,[Key] = @key
                                      ,[OwnerId] = @ownerid
                                      ,[Comments] = @comments
                                      ,[LastUpdateTime] = @lastupdatetime
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", role.Id),
                new SqlParameter("@key", role.Key),
                new SqlParameter("@name", role.Name),
                new SqlParameter("@ownerid", role.OwnerId),
                new SqlParameter("@comments", role.Comments),
                new SqlParameter("@lastupdatetime", role.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        public static DataSet ListAllRoles()
        {
            string cmdline = @"SELECT r.[Id]
                                  ,r.[Key]
                                  ,r.[Name]
                                  ,r.[OwnerId]
                                  ,a.[UserName] as [Owner]
                                  ,r.[Comments]
                                  ,r.[CreateTime]
                                  ,r.[LastUpdateTime]
                              FROM [dbo].[Roles] r JOIN [dbo].[Admin] a ON r.[OwnerId]=a.[Id]";
            return SqlHelper.ExecuteQuery(cmdline);
        }
    }
}
