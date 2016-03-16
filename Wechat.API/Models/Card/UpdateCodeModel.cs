using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class UpdateCodeModel
    {
        /// <summary>
        /// 必填，需变更的Code码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 必填，变更后的有效Code码
        /// </summary>
        public string NewCode { get; set; }
        /// <summary>
        /// 非必填，卡券ID。自定义Code码卡券为必填
        /// </summary>
        public string CardId { get; set; }
    }
}
