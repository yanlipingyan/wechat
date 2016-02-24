using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    public class WechatResult
    {
        public Enums.WechatReturnCodeEnum errcode { get; set; }
        public string errmsg { get; set; }
    }
}
