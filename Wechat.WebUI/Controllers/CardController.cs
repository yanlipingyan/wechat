﻿using Newtonsoft.Json;
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
            return Content(Wechat.API.Card.GetSign(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, Wechat.API.Common.GetNonceStr(), Wechat.API.Common.GetTimeStamp()));
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
            return Wechat.API.Card.CreateGrouponCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public string CreateCashCard()
        {
            var model = new Wechat.API.Model.CashCardModel()
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
            return Wechat.API.Card.CreateCashCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public string CreateDiscountCard()
        {
            var model = new Wechat.API.Model.DiscountCardModel()
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
            return Wechat.API.Card.CreateDiscountCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public string CreateGiftCard()
        {
            var model = new Wechat.API.Model.GiftCardModel()
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
            return Wechat.API.Card.CreateGiftCard(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, model);
        }

        public string CreateGeneralCouponCard()
        {
            var model = new Wechat.API.Model.GeneralCouponCardModel()
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
                CustomUrl = "http://www.chihuoqq.com",
                CustomUrlSubTitle = "",
                PromotionUrlName = "营销入口",
                PromotionUrl = "http://www.chihuoqq.com",
                PromotionUrlSubTitle = "",
                GetLimit = 1,
                CanShare = true,
                CanGiveFriend = true,
                DefaultDetail = "优惠30元"
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
