using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class AdminHandler
    {
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        public static bool AddAdmin(Entities.Admin usr)
        {
            string cmdline = @"INSERT INTO [dbo].[Admin]
                                       ([Id]
                                       ,[Active]
                                       ,[UserName]
                                       ,[PhoneNumber]
                                       ,[PasswordHash]
                                       ,[RoleId]
                                       ,[CreateTime])
                                 VALUES
                                       (@id,@status,@username,@phone,@password,@roleid,@createtime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", usr.Id),
                new SqlParameter("@status", usr.Active),
                new SqlParameter("@username", usr.UserName),
                new SqlParameter("@phone", usr.PhoneNumber),
                new SqlParameter("@password", usr.PasswordHash),
                new SqlParameter("@roleid", usr.RoleId),
                new SqlParameter("@createtime", usr.CreateTime),
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 设置管理员状态为禁用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool InActiveAdmin(Guid id)
        {
            string cmdline = @"UPDATE [dbo].[Users]
                                   SET [Active] = @status
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status", true),
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 通过手机号码判断用户是否已经存在
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool CheckAdminExist(string phone)
        {
            string cmdline = @"SELECT [Id]
                              FROM [dbo].[Admin]
                              WHERE EXIST
                              (SELECT * FROM [dbo].[Users] WHERE [Phone] = @phone)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@phone", phone)
            };
            return SqlHelper.CheckExist(cmdline, parameters);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        public static bool UpdatePassword(Guid userId, string newPassword)
        {
            string cmdline = @"UPDATE [dbo].[Admin]
                                   SET [PasswordHash] = @newpassword
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", userId),
                new SqlParameter("@newpassword", newPassword)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 用户登录尝试操作，返回用户Id，用户名，手机号码，上次登录时间
        /// </summary>
        /// <param name="PhoneNumber">手机号码</param>
        /// <param name="PasswordHash">哈希密码</param>
        /// <returns></returns>
        public static DataSet LogAttempt(string PhoneNumber, string PasswordHash)
        {
            string cmdline = @"SELECT [Id]
                                  ,[UserName]
                                  ,[PhoneNumber]
                                  ,[RoleId]
                               FROM [dbo].[Admin]
                               WHERE [PhoneNumber] = @phonenumber
                               AND [PasswordHash] = @passwordhash";
            SqlParameter[] parameters =
            {
                new SqlParameter("@phonenumber", PhoneNumber),
                new SqlParameter("@passwordhash", PasswordHash)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        /// <summary>
        /// 更新上次登录时间
        /// </summary>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        public static bool UpdateLastLoginTime(Guid userId)
        {
            string cmdline = @"UPDATE [dbo].[Admin]
                                   SET [LastLoginTime] = @lastlogintime
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@lastlogintime", DateTime.Now),
                new SqlParameter("@id", userId)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 更新管理员账户信息
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        public static bool UpdateAdminProfile(Entities.Admin usr)
        {
            string cmdline = @"UPDATE [dbo].[Admin]
                                   SET [UserName] = @username
                                      ,[PhoneNumber] = @phone
                                      ,[RoleId] = @roleid
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@username", usr.UserName),
                new SqlParameter("@phone", usr.PhoneNumber),
                new SqlParameter("@roleid", usr.RoleId),
                new SqlParameter("@id", usr.Id)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 获取用户基础信息，用户名和权限Key
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static DataSet GetUsrInfo(string phone)
        {
            string cmdline = @"SELECT a.[Id] as uid, a.[UserName] as name, r.[Key] as role
                                    FROM [dbo].[Admin] a JOIN [dbo].[Roles] r
                                    ON a.[RoleId] = r.[Id]
                                    WHERE a.[PhoneNumber] = @phone";
            SqlParameter[] parameters =
            {
                new SqlParameter("phone", phone)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        /// <summary>
        /// 获取全部管理员信息
        /// </summary>
        /// <returns></returns>
        public static DataSet ListAllAdmins()
        {
            string cmdline = @"SELECT a.[Id]
                                      ,a.[UserName]
                                      ,a.[PhoneNumber]
                                      ,a.[RoleId]
                                      ,r.[Name] as [RoleName]
                                      ,r.[Key] as [RoleKey]             
                                      ,a.[CreateTime]
                                      ,a.[LastLoginTime]
                                  FROM [dbo].[Admin] a JOIN [dbo].[Roles] r ON a.[RoleId] = r.[Id]";
            return SqlHelper.ExecuteQuery(cmdline);
        }

        /// <summary>
        /// 根据角色获取管理员列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static DataSet ListAdminsByRole(int roleId)
        {
            string cmdline = @"SELECT [Id]
                                      ,[UserName]
                                      ,[PhoneNumber]
                                      ,[RoleId]
                                      ,[CreateTime]
                                      ,[LastLoginTime]
                                  FROM [dbo].[Admin]
                                  WHERE [RoleId] = @role";
            SqlParameter[] parameters =
            {
                new SqlParameter("@role", roleId)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }
    }
}
