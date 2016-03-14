using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class GetCodeModel
    {
        /// <summary>
        /// 必填，单张卡券的唯一标准
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 非必填，卡券ID代表一类卡券
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 非必填，卡券ID代表一类卡券
        /// </summary>
        public bool CheckConsume { get; set; }
    }
}
