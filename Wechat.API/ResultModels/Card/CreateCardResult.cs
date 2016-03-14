using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    public class CreateCardResult : WechatResult
    {
        /// <summary>
        /// 卡券id
        /// </summary>
        public string card_id { get; set; }
    }
}
