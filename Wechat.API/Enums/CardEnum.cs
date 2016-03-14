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
        已失效 = "UNAVAILABLE"
    }

    public enum CardSceneEnum
    {
        附近 = "SCENE_NEAR_BY",
        自定义菜单 = "SCENE_MENU",
        二维码 = "SCENE_QRCODE",
        公众号文章 = "SCENE_ARTICLE",
        h5页面 = "SCENE_H5",
        自动回复 = "SCENE_IVR",
        卡券自定义cell = "SCENE_CARD_CUSTOM_CELL"
    }
}
