using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.Toolkit.Helper;

namespace Wechat.WebUI.Controllers
{
    public class CardController : Controller
    {
        public ActionResult Index(string cardId = "pxKfavtut0Y7u8QzQJdIwAuMcvk0")
        {
            ViewBag.CardId = cardId;
            return View();
        }

        public string ShowQrcode(string cardId)
        {
            LogHelper.Error(Wechat.API.Card.ShowCardQrcode(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, cardId));
            return Wechat.API.Card.ShowCardQrcode(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, cardId);
        }

        public ActionResult JSSDK()
        {
            return View();
        }

        public string GetCardSign(string timestamp, string nonceStr, string shopId, string cardType, string cardId)
        {
            return Wechat.API.Card.GetSign(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, nonceStr, timestamp, shopId, cardType, cardId);
        }

        public ActionResult TestCardSign()
        {
            return Content(Wechat.API.Card.GetSign(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret,Wechat.API.Common.GetNonceStr(),Wechat.API.Common.GetTimeStamp()));
        }

        public string GetCardExt(string cardId, string timestamp, string nonceStr, string code, string openId)
        {
            var obj = new
            {
                code = code,
                openid = openId,
                timestamp = timestamp,
                nonce_str = nonceStr,
                signature = Wechat.API.Card.GetCardExtSign(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, cardId, timestamp, nonceStr, code, openId)
            };
            return JsonConvert.SerializeObject(obj);
        }

        public ActionResult QueryDetail(string cardid = "pxKfavtut0Y7u8QzQJdIwAuMcvk0")
        {
            string content = Wechat.API.Card.QueryCardDetail(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, cardid);
            return Content(content);
        }

        public ActionResult QueryList()
        {
            string[] status = new string[] { "CARD_STATUS_VERIFY_OK", "CARD_STATUS_DISPATCH", "CARD_STATUS_VERIFY_FAIL" };
            string content = Wechat.API.Card.QueryCardList(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, status);
            return Content(content);
        }

        public ActionResult QueryListForMe(string openId = "oxKfavv2QQtVecXAmVehYt8fB15s")
        {
            return Content(Wechat.API.Card.QueryCardListForUser(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, openId));
        }

        public string CreateGroupon()
        {
            var model = new Wechat.API.Model.GrouponCardModel()
            {
                logo_url = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                code_type = "CODE_TYPE_BARCODE",
                brand_name = "睿桃",
                title = "20元团购券",
                sub_title = "副标题",
                color = "Color010",
                notice = "购买时出示此券",
                description = "不可与其他优惠同享",
                quantity = 2,
                date_info = JsonConvert.SerializeObject(new
                {
                    type = "DATE_TYPE_FIX_TERM",
                    fixed_term = 15,
                    end_timestamp = 0,
                }),
                use_custom_code = false,
                bind_openid = false,
                service_phone = "0371-86097133",
                location_id_list = new int[] { 123, 12321 },
                source = "",
                custom_url_name = "",
                custom_url = "",
                custom_url_sub_title = "",
                promotion_url_name = "",
                promotion_url = "",
                get_limit = 1,
                can_share = true,
                can_give_friend = true,
                deal_detail = "团购券",
            };
            return Wechat.API.Card.CreateGrouponCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public string CreateCashCard()
        {
            var model = new Wechat.API.Model.CashCardModel()
            {
                logo_url = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                code_type = "CODE_TYPE_BARCODE",
                brand_name = "睿桃",
                title = "20元代金券",
                sub_title = "副标题",
                color = "Color010",
                notice = "购买时出示此券",
                description = "不可与其他优惠同享",
                quantity = 2,
                date_info = JsonConvert.SerializeObject(new
                {
                    type = "DATE_TYPE_FIX_TERM",
                    fixed_term = 15,
                    end_timestamp = 0,
                }),
                use_custom_code = false,
                bind_openid = false,
                service_phone = "0371-86097133",
                location_id_list = new int[] { },
                source = "",
                custom_url_name = "",
                custom_url = "",
                custom_url_sub_title = "",
                promotion_url_name = "",
                promotion_url = "",
                get_limit = 1,
                can_share = true,
                can_give_friend = true,
                least_cost = 100,
                reduce_cost = 20,
            };
            return Wechat.API.Card.CreateCashCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public string CreateDiscountCard()
        {
            var model = new Wechat.API.Model.DiscountCardModel()
            {
                logo_url = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                code_type = "CODE_TYPE_BARCODE",
                brand_name = "睿桃",
                title = "三折折扣券",
                sub_title = "副标题",
                color = "Color010",
                notice = "购买时出示此券",
                description = "不可与其他优惠同享",
                quantity = 2,
                date_info = JsonConvert.SerializeObject(new
                {
                    type = "DATE_TYPE_FIX_TERM",
                    fixed_term = 15,
                    end_timestamp = 0,
                }),
                use_custom_code = false,
                bind_openid = false,
                service_phone = "0371-86097133",
                location_id_list = new int[] { },
                source = "",
                custom_url_name = "",
                custom_url = "",
                custom_url_sub_title = "",
                promotion_url_name = "",
                promotion_url = "",
                get_limit = 1,
                can_share = true,
                can_give_friend = true,
                discount = 70
            };
            return Wechat.API.Card.CreateDiscountCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public string CreateGiftCard()
        {
            var model = new Wechat.API.Model.GiftCardModel()
            {
                logo_url = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                code_type = "CODE_TYPE_QRCODE",
                brand_name = "睿桃",
                title = "礼品券",
                sub_title = "副标题",
                color = "Color010",
                notice = "购买时出示此券",
                description = "不可与其他优惠同享",
                quantity = 2,
                date_info = JsonConvert.SerializeObject(new
                {
                    type = "DATE_TYPE_FIX_TERM",
                    fixed_term = 15,
                    end_timestamp = 0,
                }),
                use_custom_code = false,
                bind_openid = false,
                service_phone = "0371-86097133",
                location_id_list = new int[] { },
                source = "",
                custom_url_name = "",
                custom_url = "",
                custom_url_sub_title = "",
                promotion_url_name = "",
                promotion_url = "",
                get_limit = 1,
                can_share = true,
                can_give_friend = true,
                gift = "可兑换手套一个"
            };
            return Wechat.API.Card.CreateGiftCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public string CreateGeneralCouponCard()
        {
            var model = new Wechat.API.Model.GeneralCouponCardModel()
            {
                logo_url = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                code_type = "CODE_TYPE_QRCODE",
                brand_name = "睿桃",
                title = "30元通用优惠券",
                sub_title = "使用优惠券",
                color = "Color010",
                notice = "购买时出示此券",
                description = "不可与其他优惠同享",
                quantity = 2,
                date_info = JsonConvert.SerializeObject(new
                {
                    type = "DATE_TYPE_FIX_TERM",
                    fixed_term = 15,
                    end_timestamp = 0,
                }),
                use_custom_code = false,
                bind_openid = false,
                service_phone = "0371-86097133",
                location_id_list = new int[] { 123, 256 },
                source = "大众点皮",
                custom_url_name = "外链名字",
                custom_url = "http://www.chihuoqq.com",
                custom_url_sub_title = "",
                promotion_url_name = "营销入口",
                promotion_url = "http://www.chihuoqq.com",
                get_limit = 1,
                can_share = true,
                can_give_friend = true,
                default_detail = "优惠30元"
            };
            return Wechat.API.Card.CreateGeneralCouponCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public ActionResult UpdateStock(string cardId = "pxKfavtut0Y7u8QzQJdIwAuMcvk0")
        {
            return Content(Wechat.API.Card.UpdateCardStock(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, cardId, 5));
        }

        public ActionResult DeleteCard(string cardId = "pxKfavgLcNvXtzLe2LjqiTHy-Chs")
        {
            return Content(Wechat.API.Card.DeleteCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, cardId));
        }

        public ActionResult SetCardCanPay(string cardId = "pxKfavtut0Y7u8QzQJdIwAuMcvk0")
        {
            return Content(Wechat.API.Card.SetCardCanPay(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, cardId));
        }

        //注意：这里的结束时间不能是系统当天的时间
        public ActionResult GetCardStatistical()
        {
            return Content(Wechat.API.Card.GetCardStatistical(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), 1));
        }

        //注意：这里的结束时间不能是系统当天的时间
        public ActionResult GetFreeCardStatistical()
        {
            return Content(Wechat.API.Card.GetFreeCardStatistical(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), 1));
        }

        //注意：这里的结束时间不能是系统当天的时间
        public ActionResult GetSpecialCardStatistical()
        {
            return Content(Wechat.API.Card.GetSpecialCardStatistical(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), 1));
        }

        public ActionResult QueryCode(string cardId = "pxKfavr1cidMqbYHbstctHeMhQfM", string code = "833308329979")
        {
            return Content(Wechat.API.Card.QueryCode(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, cardId, code));
        }

        public ActionResult UpdateCode(string cardId = "pxKfavr1cidMqbYHbstctHeMhQfM", string code = "833308329979")
        {
            return Content(Wechat.API.Card.UpdateCode(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, code, "3495739475"));
        }
    }
}
