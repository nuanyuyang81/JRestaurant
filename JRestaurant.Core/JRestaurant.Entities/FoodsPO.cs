using System;

namespace JRestaurant.Entities
{
    // 料理食材进货
    public class FoodsPO
    {
        // 料理食材进货记录唯一Id, 数据库自增长类型
        public int Id
        {
            get; set;
        }
        // 料理食材进货-金额
        public double Amount
        {
            get; set;
        }
        // 供货商唯一Id
        public int VendorId
        {
            get; set;
        }
        // 录入人员唯一Id
        public Guid OwnerId
        {
            get; set;
        }
        // 实际进货日期
        public string AddDate { get; set; } = DateTime.Now.ToLongDateString();
        // 本周第几天
        public DayOfWeek WeekkDay { get; set; } = DateTime.Now.DayOfWeek;

        public string Comments
        {
            get; set;
        }

        // 录入时间
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    }

}
