using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    public class CardShelvesResult : WechatResult
    {
        /// <summary>
        /// 货架链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 货架ID。货架的唯一标识
        /// </summary>
        public string page_id { get; set; }
    }
}
