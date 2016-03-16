using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YLP.Tookit.Helper;
using Wechat.API;
using Wechat.API.Models;
using Newtonsoft.Json;
using Wechat.API.Enums;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class CardController : Controller
    {
        //
        // GET: /Test/Card/

        public ActionResult Index(string cardId = "pxKfavtut0Y7u8QzQJdIwAuMcvk0")
        {
            ViewBag.CardId = cardId;
            return View();
        }

        public ActionResult JSSDK()
        {
            return View();
        }

        public string ShowQrcode(string cardId)
        {
            Log4NetHelper.Error(Card.ShowCardQrcode(ApiModel.AppID, ApiModel.AppSecret, cardId));
            return Card.ShowCardQrcode(ApiModel.AppID, ApiModel.AppSecret, cardId);
        }



        public string GetCardSign(string timestamp, string nonceStr, string shopId, string cardType, string cardId)
        {
            return Card.GetSign(ApiModel.AppID, ApiModel.AppSecret, nonceStr, timestamp, shopId, cardType, cardId);
        }

        public ActionResult TestCardSign()
        {
            return Content(Card.GetSign(ApiModel.AppID, ApiModel.AppSecret, Common.GetNonceStr(), Common.GetTimeStamp()));
        }

        public string GetCardExt(string cardId, string timestamp, string nonceStr, string code, string openId)
        {
            var obj = new
            {
                code = code,
                openid = openId,
                timestamp = timestamp,
                nonce_str = nonceStr,
                signature = Card.GetCardExtSign(ApiModel.AppID, ApiModel.AppSecret, cardId, timestamp, nonceStr, code, openId)
            };
            return JsonConvert.SerializeObject(obj);
        }

        public ActionResult QueryDetail(string cardid = "pxKfavtut0Y7u8QzQJdIwAuMcvk0")
        {
            string content = Card.GetCardDetail(ApiModel.AppID, ApiModel.AppSecret, cardid);
            return Content(content);
        }

        public ActionResult QueryList()
        {
            string[] status = new string[] { "CARD_STATUS_VERIFY_OK", "CARD_STATUS_DISPATCH", "CARD_STATUS_VERIFY_FAIL" };
            return Content(JsonConvert.SerializeObject(Card.GetCardList(ApiModel.AppID, ApiModel.AppSecret, new GetCardListModel() { StatusList = status })));
        }

        public ActionResult QueryListForMe(string openId = "oxKfavv2QQtVecXAmVehYt8fB15s")
        {
            return Content(JsonConvert.SerializeObject(Card.GetUserCardList(ApiModel.AppID, ApiModel.AppSecret, new GetUserCardListModel() { OpenID = openId })));
        }

        public string CreateGroupon()
        {
            var model = new GrouponCardModel()
            {
                LogoUrl = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                CodeType = "CODE_TYPE_BARCODE",
                BrandName = "睿桃",
                Title = "20元团购券",
                SubTitle = "副标题",
                Color = "Color010",
                Notice = "购买时出示此券",
                Description = "不可与其他优惠同享",
                Quantity = 2,
                Type = 2,
                FixedTerm = 15,
                FixedBeginTerm = 0,
                UseCustomCode = false,
                BindOpenId = false,
                ServicePhone = "0371-86097133",
                LocationIdList = new int[] { 123, 12321 },
                Source = "",
                CustomUrlName = "",
                CustomUrl = "",
                CustomUrlSubTitle = "",
                PromotionUrlName = "",
                PromotionUrl = "",
                PromotionUrlSubTitle = "",
                GetLimit = 1,
                CanShare = true,
                CanGiveFriend = true,
                DealDetail = "团购券",
            };
            return JsonConvert.SerializeObject(Card.CreateGrouponCard(ApiModel.AppID, ApiModel.AppSecret, model));
        }

        public string CreateCashCard()
        {
            var model = new CashCardModel()
            {
                LogoUrl = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                CodeType = "CODE_TYPE_BARCODE",
                BrandName = "睿桃",
                Title = "20元代金券",
                SubTitle = "副标题",
                Color = "Color010",
                Notice = "购买时出示此券",
                Description = "不可与其他优惠同享",
                Quantity = 2,
                Type = 2,
                FixedTerm = 15,
                FixedBeginTerm = 0,
                UseCustomCode = false,
                BindOpenId = false,
                ServicePhone = "0371-86097133",
                LocationIdList = new int[] { },
                Source = "",
                CustomUrlName = "",
                CustomUrl = "",
                CustomUrlSubTitle = "",
                PromotionUrlName = "",
                PromotionUrl = "",
                PromotionUrlSubTitle = "",
                GetLimit = 1,
                CanShare = true,
                CanGiveFriend = true,
                LeastCost = 100,
                ReduceCost = 20,
            };
            return JsonConvert.SerializeObject(Card.CreateCashCard(ApiModel.AppID, ApiModel.AppSecret, model));
        }

        public string CreateDiscountCard()
        {
            var model = new DiscountCardModel()
            {
                LogoUrl = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                CodeType = "CODE_TYPE_BARCODE",
                BrandName = "睿桃",
                Title = "三折折扣券",
                SubTitle = "副标题",
                Color = "Color010",
                Notice = "购买时出示此券",
                Description = "不可与其他优惠同享",
                Quantity = 2,
                Type = 2,
                FixedTerm = 15,
                FixedBeginTerm = 0,
                UseCustomCode = false,
                BindOpenId = false,
                ServicePhone = "0371-86097133",
                LocationIdList = new int[] { },
                Source = "",
                CustomUrlName = "",
                CustomUrl = "",
                CustomUrlSubTitle = "",
                PromotionUrlName = "",
                PromotionUrl = "",
                PromotionUrlSubTitle = "",
                GetLimit = 1,
                CanShare = true,
                CanGiveFriend = true,
                Discount = 70
            };
            return JsonConvert.SerializeObject(Card.CreateDiscountCard(ApiModel.AppID, ApiModel.AppSecret, model));
        }

        public string CreateGiftCard()
        {
            var model = new GiftCardModel()
            {
                LogoUrl = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                CodeType = "CODE_TYPE_QRCODE",
                BrandName = "睿桃",
                Title = "礼品券",
                SubTitle = "副标题",
                Color = "Color010",
                Notice = "购买时出示此券",
                Description = "不可与其他优惠同享",
                Quantity = 2,
                Type = 2,
                FixedTerm = 15,
                FixedBeginTerm = 0,
                UseCustomCode = false,
                BindOpenId = false,
                ServicePhone = "0371-86097133",
                LocationIdList = new int[] { },
                Source = "",
                CustomUrlName = "",
                CustomUrl = "",
                CustomUrlSubTitle = "",
                PromotionUrlName = "",
                PromotionUrl = "",
                PromotionUrlSubTitle = "",
                GetLimit = 1,
                CanShare = true,
                CanGiveFriend = true,
                Gift = "可兑换手套一个"
            };
            return JsonConvert.SerializeObject(Card.CreateGiftCard(ApiModel.AppID, ApiModel.AppSecret, model));
        }

        public string CreateGeneralCouponCard()
        {
            var model = new GeneralCouponCardModel()
            {
                LogoUrl = "http://mmbiz.qpic.cn/mmbiz/47BVHoK4A4kMQbicSLdYUciaI6rqWEv9x2MKOmZXPQYbWMFrDvXRny9LiaEtcVicoqFfbfJrgr7PUicS2DMHucmJZ1w/0",
                CodeType = "CODE_TYPE_QRCODE",
                BrandName = "睿桃",
                Title = "30元通用优惠券",
                SubTitle = "使用优惠券",
                Color = "Color010",
                Notice = "购买时出示此券",
                Description = "不可与其他优惠同享",
                Quantity = 2,
                Type = 2,
                FixedTerm = 15,
                FixedBeginTerm = 0,
                UseCustomCode = false,
                BindOpenId = false,
                ServicePhone = "0371-86097133",
                LocationIdList = new int[] { 123, 256 },
                Source = "大众点皮",
                CustomUrlName = "外链名字",
                CustomUrl = "http://www.liblog.cn",
                CustomUrlSubTitle = "",
                PromotionUrlName = "营销入口",
                PromotionUrl = "http://www.liblog.cn",
                PromotionUrlSubTitle = "",
                GetLimit = 1,
                CanShare = true,
                CanGiveFriend = true,
                DefaultDetail = "优惠30元"
            };
            return JsonConvert.SerializeObject(Card.CreateGeneralCouponCard(ApiModel.AppID, ApiModel.AppSecret, model));
        }

        public ActionResult UpdateStock(string cardId = "pxKfavtut0Y7u8QzQJdIwAuMcvk0")
        {
            return Content(JsonConvert.SerializeObject(Card.UpdateCardStock(ApiModel.AppID, ApiModel.AppSecret, new UpdateCardStockModel() { CardId = cardId, IncreaseStockValue = 5 })));
        }

        public ActionResult DeleteCard(string cardId = "pxKfavgLcNvXtzLe2LjqiTHy-Chs")
        {
            return Content(JsonConvert.SerializeObject(Card.DeleteCard(ApiModel.AppID, ApiModel.AppSecret, cardId)));
        }

        public ActionResult SetCardCanPay(string cardId = "pxKfavtut0Y7u8QzQJdIwAuMcvk0")
        {
            return Content(JsonConvert.SerializeObject(Card.SetCardCanPay(ApiModel.AppID, ApiModel.AppSecret, cardId)));
        }

        //注意：这里的结束时间不能是系统当天的时间
        public ActionResult GetCardStatistical()
        {
            return Content(JsonConvert.SerializeObject(Card.GetCardStatistical(ApiModel.AppID, ApiModel.AppSecret, new GetCardStatisticalModel() { BeginDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(-1), CondSource = CardSourceEnum.公众平台创建的卡券数据 })));
        }

        //注意：这里的结束时间不能是系统当天的时间
        public ActionResult GetFreeCardStatistical()
        {
            return Content(JsonConvert.SerializeObject(Card.GetFreeCardStatistical(ApiModel.AppID, ApiModel.AppSecret, new GetCardStatisticalModel() { BeginDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(-1), CondSource = CardSourceEnum.公众平台创建的卡券数据 })));
        }

        //注意：这里的结束时间不能是系统当天的时间
        public ActionResult GetSpecialCardStatistical()
        {
            return Content(JsonConvert.SerializeObject(Card.GetSpecialCardStatistical(ApiModel.AppID, ApiModel.AppSecret, new GetCardStatisticalModel() { BeginDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(-1), CondSource = CardSourceEnum.公众平台创建的卡券数据 })));
        }

        public ActionResult QueryCode(string cardId = "pxKfavr1cidMqbYHbstctHeMhQfM", string code = "833308329979")
        {
            return Content(JsonConvert.SerializeObject(Card.GetCode(ApiModel.AppID, ApiModel.AppSecret, new GetCodeModel() { Code = code, CardId = cardId })));
        }

        public ActionResult UpdateCode(string cardId = "pxKfavr1cidMqbYHbstctHeMhQfM", string code = "833308329979")
        {
            return Content(JsonConvert.SerializeObject(Card.UpdateCode(ApiModel.AppID, ApiModel.AppSecret, new UpdateCodeModel() { Code = code, NewCode = "3495739475" })));
        }

        //public ActionResult DecryptCode(string code) 
        //{

        //}
    }
}
