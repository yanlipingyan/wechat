using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class GetCardListModel
    {
        /// <summary>
        /// 必填，查询卡列表的起始偏移量，从0开始，即offset: 5是指从从列表里的第六个开始读取。
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 必填，需要查询的卡片的数量（数量最大50）。
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 非必填，支持开发者拉出指定状态的卡券列表，例：仅拉出通过审核的卡券。
        /// </summary>
        public string[] StatusList { get; set; }
    }
}
