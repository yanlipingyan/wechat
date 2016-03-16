using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Enums
{
    public enum CardStatusEnum
    {
        NORMAL,//正常
        CONSUMED,//已核销
        EXPIRE,//已过期
        GIFTING,//转赠中
        GIFT_SUCC,//转赠成功
        GIFT_TIMEOUT,//转赠超时
        DELETE,//已删除
        UNAVAILABLE//已失效
    }

    public enum CardSceneEnum
    {
        SCENE_NEAR_BY,//附近
        SCENE_MENU,//自定义菜单
        SCENE_QRCODE,//二维码
        SCENE_ARTICLE,//公众号文章
        SCENE_H5,//h5页面
        SCENE_IVR,//自动回复
        SCENE_CARD_CUSTOM_CELL//卡券自定义cell
    }

    public enum CardTypeEnum
    {
        折扣券 = 0,
        代金券 = 1,
        礼品券 = 2,
        优惠券 = 3,
        团购券 = 4,
    }

    public enum CardSourceEnum
    {
        公众平台创建的卡券数据 = 0,
        API创建的卡券数据 = 1,
    }
}
