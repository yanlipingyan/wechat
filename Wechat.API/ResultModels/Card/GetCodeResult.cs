using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    public class GetCodeResult : WechatResult
    {
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 卡券详情
        /// </summary>
        public GetCode card { get; set; }
    }

    public class GetCode
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 起始使用时间
        /// </summary>
        public string begin_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 当前code对应卡券的状态:code未被添加或被转赠领取的情况则统一报错：invalid serial code
        /// </summary>
        public Enums.CardStatusEnum user_card_status { get; set; }
        /// <summary>
        /// 是否可以核销，true为可以核销，false为不可核销
        /// </summary>
        public string can_consume { get; set; }
    }
}
