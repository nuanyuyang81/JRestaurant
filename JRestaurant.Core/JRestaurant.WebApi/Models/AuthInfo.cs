using System;

namespace JRestaurant.WebApi.Models
{
    /// <summary>
    /// 身份验证信息 模拟JWT的payload
    /// </summary>
    public class AuthInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 口令过期时间
        /// </summary>
        public DateTime? ExpiryDateTime { get; set; }
    }
}