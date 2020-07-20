using System;

namespace JRestaurant.Entities
{
    // 供应商
    public class Vendor
    {
        // 供应商唯一Id, 数据库自增长类型
        public int Id
        {
            get; set;
        }
        /// <summary>
        /// 供应商状态，正常 或 禁用, 默认为正常
        /// </summary>
        public bool Active { get; set; } = true;


        // 供应商 名字
        public string Name
        {
            get; set;
        }

        // 分类，即哪类进货记录里面显示的供应商
        public Aim AimType
        {
            get; set;
        }
        // 录入人员唯一Id
        public Guid OwnerId
        {
            get; set;
        }
        //备注，描述
        public string Comments
        {
            get; set;
        }
        // 创建时间
        public DateTime CreateTime { get; set; } = DateTime.Now;
        // 最后更新时间
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    }

}
