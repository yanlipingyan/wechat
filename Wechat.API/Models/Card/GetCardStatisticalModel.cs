using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class GetCardStatisticalModel
    {
        /// <summary>
        /// 查询数据的起始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 查询数据的截至时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据
        /// </summary>
        public Enums.CardSourceEnum CondSource { get; set; }
        /// <summary>
        /// 卡券ID。填写后，指定拉出该卡券的相关数据。
        /// </summary>
        public string CardId { get; set; }
    }
}
