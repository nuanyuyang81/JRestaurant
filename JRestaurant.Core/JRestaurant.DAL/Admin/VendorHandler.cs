using JRestaurant.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL.Admin
{
    public class VendorHandler
    {
        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public static bool AddVendor(Vendor vendor)
        {
            string cmdline = @"INSERT INTO [dbo].[Vendor]
                                       ([Active]
                                       ,[Name]
                                       ,[AimType]
                                       ,[OwnerId]
                                       ,[Comments]
                                       ,[CreateTime]
                                       ,[LastUpdateTime])
                                 VALUES
                                       (@status,@name,@aimtype,@ownerid,@comments,@createtime,@lastupdatetime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status", vendor.Active),
                new SqlParameter("@name", vendor.Name),
                new SqlParameter("@aimtype", vendor.AimType),
                new SqlParameter("@ownerid", vendor.OwnerId),
                new SqlParameter("@comments", vendor.Comments),
                new SqlParameter("@createtime", vendor.CreateTime),
                new SqlParameter("@lastupdatetime", vendor.LastUpdateTime)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 设置供应商状态为禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public static bool InActiveVendor(int id, Guid ownerId)
        {
            return CommonHandler.InActive(id, "Vendor", ownerId);
        }

        /// <summary>
        /// 更新供应商信息
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public static bool UpdateVendor(Vendor vendor)
        {
            string cmdline = @"UPDATE [dbo].[Vendor]
                                   SET [Name] = @name
                                      ,[AimType] = @aimtype
                                      ,[OwnerId] = @ownerid
                                      ,[Comments] = @comments
                                      ,[LastUpdateTime] = @lastupdatetime
                                 WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@name", vendor.Name),
                new SqlParameter("@aimtype", vendor.AimType),
                new SqlParameter("@ownerid", vendor.OwnerId),
                new SqlParameter("@comments", vendor.Comments),
                new SqlParameter("@lastupdatetime", DateTime.Now),
                new SqlParameter("@id", vendor.Id)
            };
            return SqlHelper.ExecuteNonQuery(cmdline, parameters);
        }

        /// <summary>
        /// 获取所有正常供应商信息
        /// </summary>
        /// <returns></returns>
        public static DataSet ListAllVendors()
        {
            string cmdline = @"SELECT [Id]
                                  ,[Name]
                                  ,[AimType]
                                  ,[OwnerId]
                                  ,[Comments]
                                  ,[CreateTime]
                                  ,[LastUpdateTime]
                              FROM [dbo].[Vendor]
                              WHERE [Active] = @status";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status", true)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }

        /// <summary>
        /// 根据Id获取供应商信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet ListVendorById(int id)
        {
            string cmdline = @"SELECT [Id]
                                    ,[Name]
                                    ,[AimType]
                                    ,[OwnerId]
                                    ,[Comments]
                                    ,[CreateTime]
                                    ,[LastUpdateTime]
                                FROM[dbo].[Vendor]
                                WHERE [Id] = @id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }


        /// <summary>
        /// 根据适用进货类型获取供应商信息
        /// </summary>
        /// <param name="aimType"></param>
        /// <returns></returns>
        public static DataSet ListVendorByAim(int aimType)
        {
            string cmdline = @"SELECT [Id]
                                    ,[Name]
                                    ,[AimType]
                                    ,[OwnerId]
                                    ,[Comments]
                                    ,[CreateTime]
                                    ,[LastUpdateTime]
                                FROM[dbo].[Vendor]
                                WHERE ([AimType] = @aim OR [AimType]=2) AND [Active]=@status";
            SqlParameter[] parameters =
            {
                new SqlParameter("@aim", aimType),
                new SqlParameter("@status", true)
            };
            return SqlHelper.ExecuteQuery(cmdline, parameters);
        }
    }
}
