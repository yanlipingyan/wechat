using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    /// <summary>
    /// 团购券
    /// </summary>
    public class GrouponCardModel : BasicCardModel
    {
        public string DealDetail { get; set; }//团购券专用，团购详情。
    }

    /// <summary>
    /// 代金券
    /// </summary>
    public class CashCardModel : BasicCardModel
    {
        public int LeastCost { get; set; }//代金券专用，表示起用金额（单位为分）,如果无起用门槛则填0。
        public int ReduceCost { get; set; }//代金券专用，表示减免金额。（单位为分）
    }

    /// <summary>
    /// 折扣券
    /// </summary>
    public class DiscountCardModel : BasicCardModel
    {
        public int Discount { get; set; }//折扣券专用，表示打折额度（百分比）。填30就是七折。
    }

    /// <summary>
    /// 礼品券
    /// </summary>
    public class GiftCardModel : BasicCardModel
    {
        public string Gift { get; set; }//礼品券专用，填写礼品的名称。
    }

    /// <summary>
    /// 优惠券
    /// </summary>
    public class GeneralCouponCardModel : BasicCardModel
    {
        public string DefaultDetail { get; set; }//优惠券专用，填写优惠详情。
    }

    /// <summary>
    /// 基类
    /// </summary>
    public class BasicCardModel
    {
        public string LogoUrl { get; set; }//卡券的商户logo，建议像素为300*300。
        public string CodeType { get; set; }//Code展示类型，"CODE_TYPE_TEXT"，文本；"CODE_TYPE_BARCODE"，一维码 ；"CODE_TYPE_QRCODE"，二维码；"CODE_TYPE_ONLY_QRCODE",二维码无code显示；"CODE_TYPE_ONLY_BARCODE",一维码无code显示；
        public string BrandName { get; set; }//商户名字,字数上限为12个汉字。
        public string Title { get; set; }//卡券名，字数上限为9个汉字。(建议涵盖卡券属性、服务及金额)。
        public string SubTitle { get; set; }//券名，字数上限为18个汉字。
        public string Color { get; set; }//券颜色。按色彩规范标注填写Color010-Color100。详情见获取颜色列表接口
        public string Notice { get; set; }//卡券使用提醒，字数上限为16个汉字。
        public string Description { get; set; }//卡券使用说明，字数上限为1024个汉字。

        public int Quantity { get; set; }//卡券库存的数量，上限为100000000。

        public int Type { get; set; }//使用时间的类型，旧文档采用的1和2依然生效。1(DATE_TYPE_FIX_TIME_RANGE)表示固定日期区间，2(DATE_TYPE_FIX_TERM)表示固定时长（自领取后按天算。)	
        public int BeginTimestamp { get; set; }//type为1时专用，表示起用时间。从1970年1月1日00:00:00至起用时间的秒数，最终需转换为字符串形态传入。（东八区时间，单位为秒）
        public int EndTimestamp { get; set; }//type为1时专用，表示结束时间，建议设置为截止日期的23:59:59过期。（东八区时间，单位为秒）
        public int FixedTerm { get; set; }//type为2时专用，表示自领取后多少天内有效，不支持填写0。
        public int FixedBeginTerm { get; set; }//type为2时专用，表示自领取后多少天开始生效，领取后当天生效填写0。（单位为天）


        //以下是不必填信息
        public bool UseCustomCode { get; set; }//是否自定义Code码。填写true或false，默认为false。通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见是否自定义Code码。
        public bool BindOpenId { get; set; }//是否指定用户领取，填写true或false。默认为false。通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取。
        public string ServicePhone { get; set; }//客服电话。
        public int[] LocationIdList { get; set; }//门店位置poiid。调用POI门店管理接口获取门店位置poiid。具备线下门店的商户为必填。
        public string Source { get; set; }//第三方来源名，例如同程旅游、大众点评。
        public string CustomUrlName { get; set; }//自定义跳转外链的入口名字。
        public string CustomUrl { get; set; }//自定义跳转的URL。
        public string CustomUrlSubTitle { get; set; }//显示在入口右侧的提示语。
        public string PromotionUrlName { get; set; }//营销场景的自定义入口名称。
        public string PromotionUrl { get; set; }//入口跳转外链的地址链接。
        public string PromotionUrlSubTitle { get; set; }//显示在营销入口右侧的提示语。。
        public int GetLimit { get; set; }//每人可领券的数量限制,不填写默认为50。
        public bool CanShare { get; set; }//卡券领取页面是否可分享。
        public bool CanGiveFriend { get; set; }//卡券是否可转赠。
    }
}
