using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    public class UpdateCardResult : WechatResult
    {
        public bool send_check { get; set; }
    }
}
