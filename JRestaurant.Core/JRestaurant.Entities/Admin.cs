using System;

namespace JRestaurant.Entities
{
    // 用户信息
    public class Admin
    {
        // 用户信息
        public Guid Id { get; set; } = Guid.NewGuid();

        // 用户是否处于正常使用状态
        public bool Active { get; set; } = true;
        // 用户名
        public string UserName
        {
            get; set;
        }
        // 用户手机号码
        public string PhoneNumber
        {
            get; set;
        }
        // 用户密码，哈希加密
        public string PasswordHash
        {
            get; set;
        }
        // 用户角色唯一Id
        public int RoleId
        {
            get; set;
        }
        // 添加用户时间
        public DateTime CreateTime { get; set; } = DateTime.Now;

        // 用户上次登录时间
        public DateTime LastLoginTime { get; set; } = DateTime.Now;
    }
}
