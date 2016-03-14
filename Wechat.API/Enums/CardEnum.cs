using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Enums
{
    public enum CardStatusEnum
    {
        正常 = "NORMAL",
        已核销 = "CONSUMED",
        已过期 = "EXPIRE",
        转赠中 = "GIFTING",
        转赠成功 = "GIFT_SUCC",
        转赠超时 = "GIFT_TIMEOUT",
        已删除 = "DELETE",
        已失效 = "UNAVAILABLE",
    }
}
