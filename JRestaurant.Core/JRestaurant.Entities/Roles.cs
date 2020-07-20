using System;

namespace JRestaurant.Entities
{
    //用户角色
    public class Roles
    {
        // 用户角色唯一Id, 数据库自增长类型
        public int Id
        {
            get; set;
        }
        // 角色 Key，admin / manager / staff

        public string Key
        {
            get;set;
        }
        // 角色名称
        public string Name
        {
            get; set;
        }
        public bool Active { get; set; } = true;
        // 角色创建人员唯一Id
        public Guid OwnerId
        {
            get; set;
        }
        public string Comments
        {
            get;set;
        }
        // 创建时间
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    }

}
