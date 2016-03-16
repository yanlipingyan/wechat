using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    public class GetCardStatisticalResult : WechatResult
    {
        public List<CardStatistical> list { get; set; }
    }
    public class CardStatistical
    {
        /// <summary>
        /// 日期信息
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 卡券id
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 卡券类型
        /// </summary>
        public Enums.CardTypeEnum card_type { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int view_cnt { get; set; }
        /// <summary>
        /// 浏览人数
        /// </summary>
        public int view_user { get; set; }
        /// <summary>
        /// 领取次数
        /// </summary>
        public int receive_cnt { get; set; }
        /// <summary>
        /// 领取人数
        /// </summary>
        public int receive_user { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int verify_cnt { get; set; }
        /// <summary>
        /// 使用人数
        /// </summary>
        public int verify_user { get; set; }
        /// <summary>
        /// 转赠次数
        /// </summary>
        public int given_cnt { get; set; }
        /// <summary>
        /// 转赠人数
        /// </summary>
        public int given_user { get; set; }
        /// <summary>
        /// 过期次数
        /// </summary>
        public int expire_cnt { get; set; }
        /// <summary>
        /// 过期人数
        /// </summary>
        public int expire_user { get; set; }
        /// <summary>
        /// 激活人数
        /// </summary>
        public int active_user { get; set; }
        /// <summary>
        /// 有效会员总人数
        /// </summary>
        public int total_user { get; set; }
        /// <summary>
        /// 历史领取会员卡总人数
        /// </summary>
        public int total_receive_user { get; set; }
    }
}
