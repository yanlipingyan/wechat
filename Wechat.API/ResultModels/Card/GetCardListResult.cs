using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    public class GetCardListResult : WechatResult
    {
        public string[] card_id_list { get; set; }

        public int total_num { get; set; }
    }
}
