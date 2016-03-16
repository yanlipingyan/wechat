using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class UpdateCardStockModel
    {
        /// <summary>
        /// 必填，卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 非必填，增加多少库存，支持不填或填0
        /// </summary>
        public int IncreaseStockValue { get; set; }

        /// <summary>
        /// 非必填，减少多少库存，可以不填或填0
        /// </summary>
        public int ReduceStockValue { get; set; }
    }
}
