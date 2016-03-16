using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class GetUserCardListModel
    {
        /// <summary>
        /// 必填，某领取者的openid
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 非必填，卡券id，不填写时默认查询当前openId下的所有卡券
        /// </summary>
        public string CardId { get; set; }
    }
}
