﻿using System;

namespace JRestaurant.Entities
{
    // 料理营业额
    public class FoodsTurnOver
    {
        // 料理营业额记录唯一Id, 数据库自增长类型
        public int Id
        {
            get; set;
        }
        // 料理营业额 - 金额
        public double Amount
        {
            get; set;
        }
        // 折扣
        public double Discount
        {
            get; set;
        }

        
        public int TypeId
        {
            get;set;
        }
        
        public string Comments
        {
            get;set;
        }

        // 实际营业额日期
        public string AddDate { get; set; } = DateTime.Now.ToLongDateString();
        // 本周第几天
        public DayOfWeek WeekkDay { get; set; } = DateTime.Now.DayOfWeek;

        // 录入人员唯一Id
        public Guid OwnerId
        {
            get; set;
        }
        // 录入时间
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    }

}
