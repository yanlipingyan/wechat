using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class SetCardInvalidModel
    {
        /// <summary>
        /// 必填，设置失效的Code码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 非必填，在自定义code时必填，卡券ID
        /// </summary>
        public string CardId { get; set; }
    }
}
