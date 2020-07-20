﻿using System;

namespace JRestaurant.Entities
{
    // 营业额种类 ，如：KOL&招待 / 股东 / 雪茄
    public class TurnOverTypes
    {
        // 营业额种类唯一Id, 数据库自增长类型
        public int Id
        {
            get; set;
        }
        // 种类名称
        public string Name
        {
            get; set;
        }
        // 状态，正常 或  禁用
        public bool Active { get; set; } = true;

        // 分类，即哪类营业额里面显示的TurnOver Types
        public Aim AimType
        {
            get; set;
        }
        // 录入人员唯一Id
        public Guid OwnerId
        {
            get; set;
        }
        // 说明，描述
        public string Comments
        {
            get; set;
        }
        // 录入时间
        public DateTime CreateTime { get; set; } = DateTime.Now;
        // 上次更新时间
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    }
    // 分类
    public enum Aim
    {
        // 料理
        Foods = 0,
        // 酒水
        Drinks = 1,
        //通用
        Both = 2
    }
}
