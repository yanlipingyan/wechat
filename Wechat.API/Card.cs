using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Wechat.API
{
    // 摘要: 
    //      微信卡券接口调用凭据
    //
    public static class Card
    {
        #region 卡券JsSdk
        /// <summary>
        /// 获取卡券jsapiTicket
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <returns>卡券jsapiTicket(string)</returns>
        public static string GetTicket(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=wx_card", AccessToken.GetToken(appId, appSecret));

            var model = Helper.JsApiTicketHelper.Get("wx_card");

            if (model == null || string.IsNullOrEmpty(model.Ticket) || Common.IsExprie(model.DateTime))
            {
                string result = WechatWebClient.Get(url);

                model.Ticket = JsonConvert.DeserializeObject<dynamic>(result)["ticket"];
                model.DateTime = DateTime.Now;

                Helper.JsApiTicketHelper.Set("wx_card", model);
            }

            return model.Ticket;
        }

        /// <summary>
        /// 微信卡券JS-SDk签名
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="noncestr">随机字符串(需要与调用JS接口页面的wx.config中的nonceStr相同)</param>
        /// <param name="timestamp">时间戳(需要与调用JS接口页面的wx.config中的timestamp相同)</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="cardType">卡券类型</param>
        /// <param name="cardId">卡券id</param>
        /// <returns>签名(string)</returns>
        public static string GetSign(string appId, string appSecret, string noncestr, string timestamp, string shopId = "", string cardType = "", string cardId = "")
        {
            string card_jsapi_ticket = GetTicket(appId, appSecret);

            //1.将所有参数的value值进行字符串的字典序排序
            SortedDictionary<string, object> orderParams = new SortedDictionary<string, object>();

            orderParams.Add(card_jsapi_ticket, "api_ticket");
            orderParams.Add(Models.ApiModel.AppID, "app_id");
            orderParams.Add(noncestr, "nonce_str");
            orderParams.Add(timestamp, "time_stamp");
            if (!string.IsNullOrEmpty(shopId))
                orderParams.Add(shopId, "location_id");
            if (!string.IsNullOrEmpty(cardType))
                orderParams.Add(cardType, "card_type");
            if (!string.IsNullOrEmpty(cardId))
                orderParams.Add(cardId, "card_id");

            //2.将所有参数value值拼接成一个字符串进行sha1加密，得到signature；
            string stringA = "";
            foreach (KeyValuePair<string, object> pair in orderParams)
            {
                stringA += pair.Key;
            }

            //3.对上面的字符串进行sha1签名
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes = UTF8Encoding.Default.GetBytes(stringA);
            byte[] bytesHash = sha1.ComputeHash(bytes);

            return BitConverter.ToString(bytesHash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 微信卡券扩展JS-SDk签名
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="cardId">卡券id</param>
        /// <param name="timestamp">时间戳(需要与调用JS接口页面的wx.config中的timestamp相同)</param>
        /// <param name="noncestr">随机字符串(需要与调用JS接口页面的wx.config中的nonceStr相同)</param>
        /// <param name="code"></param>
        /// <param name="openId"></param>
        /// <returns>签名(string)</returns>
        public static string GetCardExtSign(string appId, string appSecret, string cardId, string timestamp, string noncestr = "", string code = "", string openId = "")
        {
            string cardExt_jsapi_ticket = GetTicket(appId, appSecret);

            //1.将所有参数的value值进行字符串的字典序排序
            SortedDictionary<string, object> orderParams = new SortedDictionary<string, object>();

            orderParams.Add(cardId, "cardId");
            orderParams.Add(cardExt_jsapi_ticket, "api_ticket");
            orderParams.Add(timestamp, "timestamp");

            if (!string.IsNullOrEmpty(noncestr))
                orderParams.Add(noncestr, "nonce_str");
            if (!string.IsNullOrEmpty(code))
                orderParams.Add(code, "code");
            if (!string.IsNullOrEmpty(openId))
                orderParams.Add(openId, "openid");

            //2.将所有参数value值拼接成一个字符串进行sha1加密，得到signature；
            string stringA = "";
            foreach (KeyValuePair<string, object> pair in orderParams)
            {
                stringA += pair.Key;
            }

            //3.对上面的字符串进行sha1签名
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes = UTF8Encoding.Default.GetBytes(stringA);
            byte[] bytesHash = sha1.ComputeHash(bytes);

            return BitConverter.ToString(bytesHash).Replace("-", "").ToLower();
        }
        #endregion

        #region 创建卡券
        /// <summary>
        /// 上传卡券Logo
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="data">post数据（json格式）</param>
        /// <returns>成功：{"url":"http://mmbiz.qpic.cn/mmbiz/iaL1LJM1mF9aRKPZJkmG8xXhiaHqkKSVMMWeN3hLut7X7hicFNjakmxibMLGWpXrEXB33367o7zHN0CwngnQY7zb7g/0"};失败：{"errcode":40009,"errmsg":"invalid image size"}</returns>
        public static string UploadLogo(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data);
        }

        /// <summary>
        /// 创建卡券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="data">post数据（json格式）</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}</returns>
        public static string CreateCard(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/card/create?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data.ToString().Replace("\\", "").Replace("\"{", "{").Replace("}\"", "}"));
        }

        /// <summary>
        /// 创建团购券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="model">团购券model</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}</returns>
        public static string CreateGrouponCard(string appId, string appSecret, Models.GrouponCardModel model)
        {
            object date_info = null;
            if (model.Type == 1)
                date_info = new { type = "DATE_TYPE_FIX_TIME_RANGE", begin_timestamp = model.BeginTimestamp, end_timestamp = model.EndTimestamp };
            else if (model.Type == 2)
                date_info = new { type = "DATE_TYPE_FIX_TERM", fixed_term = model.FixedTerm, fixed_begin_term = model.FixedBeginTerm };

            var obj = new
            {
                card = new
                {
                    card_type = "GROUPON",
                    groupon = new
                    {
                        base_info = new
                        {
                            logo_url = model.LogoUrl,//卡券的商户logo
                            code_type = model.CodeType,//code展示类
                            brand_name = model.BrandName,
                            title = model.Title,
                            sub_title = model.SubTitle,
                            color = model.Color,
                            notice = model.Notice,
                            description = model.Description,
                            sku = new
                            {
                                quantity = model.Quantity
                            },
                            date_info = date_info,

                            //以下是不必填信息
                            use_custom_code = model.UseCustomCode,//是否自定义Code码。填写true或false，默认为false。通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见是否自定义Code码。
                            bind_openid = model.BindOpenId,//是否指定用户领取，填写true或false。默认为false。通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取。
                            service_phone = model.ServicePhone,//客服电话。
                            location_id_list = model.LocationIdList,//门店位置poiid。调用POI门店管理接口获取门店位置poiid。具备线下门店的商户为必填。
                            source = model.Source,//第三方来源名，例如同程旅游、大众点评。
                            custom_url_name = model.CustomUrlName,//自定义跳转外链的入口名字。
                            custom_url = model.CustomUrl,//自定义跳转的URL。
                            custom_url_sub_title = model.CustomUrlSubTitle,//显示在入口右侧的提示语。
                            promotion_url_name = model.PromotionUrlName,//营销场景的自定义入口名称。
                            promotion_url = model.PromotionUrl,//入口跳转外链的地址链接。
                            promotion_url_sub_title = model.PromotionUrlSubTitle,//显示在营销入口右侧的提示语
                            get_limit = model.GetLimit,//每人可领券的数量限制,不填写默认为50。
                            can_share = model.CanShare,//卡券领取页面是否可分享。
                            can_give_friend = model.CanGiveFriend,//卡券是否可转赠。
                        },
                        deal_detail = model.DealDetail
                    }
                }
            };
            return CreateCard(appId, appSecret, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 创建代金券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="model">代金券model</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}</returns>
        public static string CreateCashCard(string appId, string appSecret, Models.CashCardModel model)
        {
            object date_info = null;
            if (model.Type == 1)
                date_info = new { type = "DATE_TYPE_FIX_TIME_RANGE", begin_timestamp = model.BeginTimestamp, end_timestamp = model.EndTimestamp };
            else if (model.Type == 2)
                date_info = new { type = "DATE_TYPE_FIX_TERM", fixed_term = model.FixedTerm, fixed_begin_term = model.FixedBeginTerm };

            var obj = new
            {
                card = new
                {
                    card_type = "CASH",
                    cash = new
                    {
                        base_info = new
                        {
                            logo_url = model.LogoUrl,//卡券的商户logo
                            code_type = model.CodeType,//code展示类
                            brand_name = model.BrandName,
                            title = model.Title,
                            sub_title = model.SubTitle,
                            color = model.Color,
                            notice = model.Notice,
                            description = model.Description,
                            sku = new
                            {
                                quantity = model.Quantity
                            },
                            date_info = date_info,

                            //以下是不必填信息
                            use_custom_code = model.UseCustomCode,//是否自定义Code码。填写true或false，默认为false。通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见是否自定义Code码。
                            bind_openid = model.BindOpenId,//是否指定用户领取，填写true或false。默认为false。通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取。
                            service_phone = model.ServicePhone,//客服电话。
                            location_id_list = model.LocationIdList,//门店位置poiid。调用POI门店管理接口获取门店位置poiid。具备线下门店的商户为必填。
                            source = model.Source,//第三方来源名，例如同程旅游、大众点评。
                            custom_url_name = model.CustomUrlName,//自定义跳转外链的入口名字。
                            custom_url = model.CustomUrl,//自定义跳转的URL。
                            custom_url_sub_title = model.CustomUrlSubTitle,//显示在入口右侧的提示语。
                            promotion_url_name = model.PromotionUrlName,//营销场景的自定义入口名称。
                            promotion_url = model.PromotionUrl,//入口跳转外链的地址链接。
                            promotion_url_sub_title = model.PromotionUrlSubTitle,//显示在营销入口右侧的提示语
                            get_limit = model.GetLimit,//每人可领券的数量限制,不填写默认为50。
                            can_share = model.CanShare,//卡券领取页面是否可分享。
                            can_give_friend = model.CanGiveFriend,//卡券是否可转赠。
                        },
                        least_cost = model.LeastCost,
                        reduce_cost = model.ReduceCost
                    }
                }
            };
            return CreateCard(appId, appSecret, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 创建折扣券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="model">折扣券model</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}</returns>
        public static string CreateDiscountCard(string appId, string appSecret, Models.DiscountCardModel model)
        {
            object date_info = null;
            if (model.Type == 1)
                date_info = new { type = "DATE_TYPE_FIX_TIME_RANGE", begin_timestamp = model.BeginTimestamp, end_timestamp = model.EndTimestamp };
            else if (model.Type == 2)
                date_info = new { type = "DATE_TYPE_FIX_TERM", fixed_term = model.FixedTerm, fixed_begin_term = model.FixedBeginTerm };

            var obj = new
            {
                card = new
                {
                    card_type = "DISCOUNT",
                    discount = new
                    {
                        base_info = new
                        {
                            logo_url = model.LogoUrl,//卡券的商户logo
                            code_type = model.CodeType,//code展示类
                            brand_name = model.BrandName,
                            title = model.Title,
                            sub_title = model.SubTitle,
                            color = model.Color,
                            notice = model.Notice,
                            description = model.Description,
                            sku = new
                            {
                                quantity = model.Quantity
                            },
                            date_info = date_info,

                            //以下是不必填信息
                            use_custom_code = model.UseCustomCode,//是否自定义Code码。填写true或false，默认为false。通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见是否自定义Code码。
                            bind_openid = model.BindOpenId,//是否指定用户领取，填写true或false。默认为false。通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取。
                            service_phone = model.ServicePhone,//客服电话。
                            location_id_list = model.LocationIdList,//门店位置poiid。调用POI门店管理接口获取门店位置poiid。具备线下门店的商户为必填。
                            source = model.Source,//第三方来源名，例如同程旅游、大众点评。
                            custom_url_name = model.CustomUrlName,//自定义跳转外链的入口名字。
                            custom_url = model.CustomUrl,//自定义跳转的URL。
                            custom_url_sub_title = model.CustomUrlSubTitle,//显示在入口右侧的提示语。
                            promotion_url_name = model.PromotionUrlName,//营销场景的自定义入口名称。
                            promotion_url = model.PromotionUrl,//入口跳转外链的地址链接。
                            promotion_url_sub_title = model.PromotionUrlSubTitle,//显示在营销入口右侧的提示语
                            get_limit = model.GetLimit,//每人可领券的数量限制,不填写默认为50。
                            can_share = model.CanShare,//卡券领取页面是否可分享。
                            can_give_friend = model.CanGiveFriend,//卡券是否可转赠。
                        },
                        discount = model.Discount
                    }
                }
            };
            return CreateCard(appId, appSecret, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 创建礼品券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="model">礼品券model</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}</returns>
        public static string CreateGiftCard(string appId, string appSecret, Models.GiftCardModel model)
        {
            object date_info = null;
            if (model.Type == 1)
                date_info = new { type = "DATE_TYPE_FIX_TIME_RANGE", begin_timestamp = model.BeginTimestamp, end_timestamp = model.EndTimestamp };
            else if (model.Type == 2)
                date_info = new { type = "DATE_TYPE_FIX_TERM", fixed_term = model.FixedTerm, fixed_begin_term = model.FixedBeginTerm };

            var obj = new
            {
                card = new
                {
                    card_type = "GIFT",
                    gift = new
                    {
                        base_info = new
                        {
                            logo_url = model.LogoUrl,//卡券的商户logo
                            code_type = model.CodeType,//code展示类
                            brand_name = model.BrandName,
                            title = model.Title,
                            sub_title = model.SubTitle,
                            color = model.Color,
                            notice = model.Notice,
                            description = model.Description,
                            sku = new
                            {
                                quantity = model.Quantity
                            },
                            date_info = date_info,

                            //以下是不必填信息
                            use_custom_code = model.UseCustomCode,//是否自定义Code码。填写true或false，默认为false。通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见是否自定义Code码。
                            bind_openid = model.BindOpenId,//是否指定用户领取，填写true或false。默认为false。通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取。
                            service_phone = model.ServicePhone,//客服电话。
                            location_id_list = model.LocationIdList,//门店位置poiid。调用POI门店管理接口获取门店位置poiid。具备线下门店的商户为必填。
                            source = model.Source,//第三方来源名，例如同程旅游、大众点评。
                            custom_url_name = model.CustomUrlName,//自定义跳转外链的入口名字。
                            custom_url = model.CustomUrl,//自定义跳转的URL。
                            custom_url_sub_title = model.CustomUrlSubTitle,//显示在入口右侧的提示语。
                            promotion_url_name = model.PromotionUrlName,//营销场景的自定义入口名称。
                            promotion_url = model.PromotionUrl,//入口跳转外链的地址链接。
                            promotion_url_sub_title = model.PromotionUrlSubTitle,//显示在营销入口右侧的提示语
                            get_limit = model.GetLimit,//每人可领券的数量限制,不填写默认为50。
                            can_share = model.CanShare,//卡券领取页面是否可分享。
                            can_give_friend = model.CanGiveFriend,//卡券是否可转赠。
                        },
                        gift = model.Gift
                    }
                }
            };
            return CreateCard(appId, appSecret, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 创建优惠券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="model">优惠券model</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}</returns>
        public static string CreateGeneralCouponCard(string appId, string appSecret, Models.GeneralCouponCardModel model)
        {
            object date_info = null;
            if (model.Type == 1)
                date_info = new { type = "DATE_TYPE_FIX_TIME_RANGE", begin_timestamp = model.BeginTimestamp, end_timestamp = model.EndTimestamp };
            else if (model.Type == 2)
                date_info = new { type = "DATE_TYPE_FIX_TERM", fixed_term = model.FixedTerm, fixed_begin_term = model.FixedBeginTerm };


            var obj = new
            {
                card = new
                {
                    card_type = "GENERAL_COUPON",
                    general_coupon = new
                    {
                        base_info = new
                        {
                            logo_url = model.LogoUrl,//卡券的商户logo
                            code_type = model.CodeType,//code展示类
                            brand_name = model.BrandName,
                            title = model.Title,
                            sub_title = model.SubTitle,
                            color = model.Color,
                            notice = model.Notice,
                            description = model.Description,
                            sku = new
                            {
                                quantity = model.Quantity
                            },
                            date_info = date_info,

                            //以下是不必填信息
                            use_custom_code = model.UseCustomCode,//是否自定义Code码。填写true或false，默认为false。通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见是否自定义Code码。
                            bind_openid = model.BindOpenId,//是否指定用户领取，填写true或false。默认为false。通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取。
                            service_phone = model.ServicePhone,//客服电话。
                            location_id_list = model.LocationIdList,//门店位置poiid。调用POI门店管理接口获取门店位置poiid。具备线下门店的商户为必填。
                            source = model.Source,//第三方来源名，例如同程旅游、大众点评。
                            custom_url_name = model.CustomUrlName,//自定义跳转外链的入口名字。
                            custom_url = model.CustomUrl,//自定义跳转的URL。
                            custom_url_sub_title = model.CustomUrlSubTitle,//显示在入口右侧的提示语。
                            promotion_url_name = model.PromotionUrlName,//营销场景的自定义入口名称。
                            promotion_url = model.PromotionUrl,//入口跳转外链的地址链接。
                            promotion_url_sub_title = model.PromotionUrlSubTitle,//显示在营销入口右侧的提示语
                            get_limit = model.GetLimit,//每人可领券的数量限制,不填写默认为50。
                            can_share = model.CanShare,//卡券领取页面是否可分享。
                            can_give_friend = model.CanGiveFriend,//卡券是否可转赠。
                        },
                        default_detail = model.DefaultDetail
                    }
                }
            };
            return CreateCard(appId, appSecret, JsonConvert.SerializeObject(obj));
        }
        #endregion

        #region 投放卡券
        /// <summary>
        /// 创建卡券二维码图片
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="cardId">卡券id</param>
        /// <param name="openId">指定领取者的openid，只有该用户能领取。bind_openid字段为true的卡券必须填写，非指定openid不必填写</param>
        /// <param name="code">卡券Code码,use_custom_code字段为true的卡券必须填写，非自定义code不必填写</param>
        /// <param name="isNeverExpires">是否绝不过期，True：是；False：会过期</param>
        /// <param name="validTime">当isNeverExpires为False时需要指定有效时间</param>
        /// <returns>二维码图片</returns>
        public static string ShowCardQrcode(string appId, string appSecret, string cardId, string openId = "", string code = "", bool isNeverExpires = true, int validTime = 1800)
        {
            if (validTime < 60 || validTime > 1800)
                validTime = 1800;

            string url = string.Format("https://api.weixin.qq.com/card/qrcode/create?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var result = "";

            if (isNeverExpires)
            {
                var obj = new
                {
                    action_name = "QR_CARD",
                    action_info = new
                    {
                        card = new
                        {
                            card_id = cardId,
                            code = code,
                            openid = openId,
                            is_unique_code = false,
                            outer_id = 1
                        }
                    }
                };

                result = WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
            }
            else
            {
                var obj = new
                {
                    action_name = "QR_CARD",
                    expire_seconds = validTime,
                    action_info = new
                    {
                        card = new
                        {
                            card_id = cardId,
                            code = code,
                            openid = openId,
                            is_unique_code = false,
                            outer_id = 1
                        }
                    }
                };

                result = WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
            }

            var ticket = JsonConvert.DeserializeObject<dynamic>(result)["ticket"].ToString();

            return string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", Uri.EscapeDataString(ticket));
        }

        /// <summary>
        /// 创建货架接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="data">post内容（json字符串）</param>
        /// <returns>{ "errcode":0,"errmsg":"ok","url":"www.test.url","page_id":1}</returns>
        public static string CreateShelves(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/card/landingpage/create?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data);
        }
        #endregion

        #region 核销卡券
        #endregion

        #region 管理卡券
        /// <summary>
        /// 查询卡券信息
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="code">单张卡券的唯一标准</param>
        /// <param name="cardId">卡券ID代表一类卡券</param>
        /// <param name="check_consume">是否校验code核销状态，填入true和false时的code异常状态返回数据不同</param>
        /// <returns>ResultModels.GetCodeResult</returns>
        public static ResultModels.GetCodeResult GetCode(string appId, string appSecret, string code, string cardId = "", bool check_consume = false)
        {
            string url = string.Format("https://api.weixin.qq.com/card/code/get?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                card_id = cardId,
                code = code,
                check_consume = check_consume
            };

            return WechatWebClient.Post<ResultModels.GetCodeResult>(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 查询该用户卡包里属于该appid下的卡券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="openID">某领取者的openid</param>
        /// <param name="cardId">卡券id，不填写时默认查询当前openId下的所有卡券</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_list": [{"code": "xxx1434079154", "card_id": "xxxxxxxxxx"},{"code": "xxx1434079155", "card_id": "xxxxxxxxxx"}]}</returns>
        public static string GetCardListForUser(string appId, string appSecret, string openID, string cardId = "")
        {
            string url = string.Format("https://api.weixin.qq.com/card/user/getcardlist?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                openid = openID,
                card_id = cardId
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 查询卡券详情
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="cardId">卡券id</param>
        /// <returns></returns>
        public static string GetCardDetail(string appId, string appSecret, string cardId)
        {
            string url = string.Format("https://api.weixin.qq.com/card/get?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                card_id = cardId,
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 查询状态卡券的列表
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="data">post数据（json格式字符串）</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_id_list":["ph_gmt7cUVrlRk8swPwx7aDyF-pg"],"total_num":1}</returns>
        public static string GetCardList(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/card/batchget?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data);
        }

        /// <summary>
        /// 查询某状态卡券的列表
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="cardStatus">卡券状态,格式：["","",""]</param>
        /// <param name="offset">查询卡列表的起始偏移量，从0开始，即offset: 5是指从从列表里的第六个开始读取</param>
        /// <param name="count">需要查询的卡片的数量（数量最大50）</param>
        /// <returns>{"errcode":0,"errmsg":"ok","card_id_list":["ph_gmt7cUVrlRk8swPwx7aDyF-pg"],"total_num":1}</returns>
        public static string GetCardList(string appId, string appSecret, string[] cardStatus, int offset = 0, int count = 50)
        {
            var obj = new
            {
                offset = offset < 0 ? 0 : offset,
                count = count > 50 ? 50 : count,
                status_list = cardStatus
            };

            return GetCardList(appId, appSecret, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 更新卡券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="data">post内容（json格式的字符串）</param>
        /// <returns>{"errcode":0,"errmsg":"ok","send_check":false}</returns>
        public static string UpdateCard(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/card/update?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data);
        }

        /// <summary>
        /// 设置卡券买单接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="cardId">卡券id</param>
        /// <param name="isOpen">是否开启买单功能，填true/false</param>
        /// <returns>{"errcode":0,"errmsg":"ok"}</returns>
        public static string SetCardCanPay(string appId, string appSecret, string cardId, bool isOpen = true)
        {
            var card = GetCardDetail(appId, appSecret, cardId);

            var result = JsonConvert.DeserializeObject<dynamic>(card);

            var card_type = result["card"]["card_type"].ToString().ToLower();

            JArray ja = (JArray)result["card"][card_type]["base_info"]["location_id_list"];

            if (ja != null && ja.Count() != 0)
            {
                string url = string.Format("https://api.weixin.qq.com/card/paycell/set?access_token={0}", AccessToken.GetToken(appId, appSecret));

                var obj = new
                {
                    card_id = cardId,
                    is_open = isOpen
                };

                return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
            }

            return "该卡券未设置门店，不可设置买单";
        }

        /// <summary>
        /// 修改库存接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="cardId">卡券id</param>
        /// <param name="increase_stock_value">增加多少库存，支持不填或填0</param>
        /// <param name="reduce_stock_value">减少多少库存，可以不填或填0</param>
        /// <returns>{"errcode":0,"errmsg":"ok"}</returns>
        public static string UpdateCardStock(string appId, string appSecret, string cardId, int increase_stock_value = 0, int reduce_stock_value = 0)
        {
            string url = string.Format("https://api.weixin.qq.com/card/modifystock?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                card_id = cardId,
                increase_stock_value = increase_stock_value,
                reduce_stock_value = reduce_stock_value,
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 更改Code接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="code">需变更的Code码</param>
        /// <param name="newCode">变更后的有效Code码</param>
        /// <param name="cardId">卡券id，自定义Code码卡券为必填</param>
        /// <returns>{"errcode":0,"errmsg":"ok"}</returns>
        public static string UpdateCode(string appId, string appSecret, string code, string newCode, string cardId = "")
        {
            string url = string.Format("https://api.weixin.qq.com/card/code/update?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                code = code,
                card_id = cardId,
                new_code = newCode
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 删除卡券
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="cardId">卡券id</param>
        /// <returns>{"errcode":0,"errmsg":"ok"}</returns>
        public static string DeleteCard(string appId, string appSecret, string cardId)
        {
            string url = string.Format("https://api.weixin.qq.com/card/delete?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                card_id = cardId
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 设置卡券失效接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="code">设置失效的Code码</param>
        /// <param name="cardId">卡券id,非自定义卡券不需要输入</param>
        /// <returns>{"errcode":0,"errmsg":"ok"}</returns>
        public static string SetCardUnavailable(string appId, string appSecret, string code, string cardId = "")
        {
            string url = string.Format("https://api.weixin.qq.com/card/code/unavailable?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                code = code,
                card_id = cardId
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }
        #endregion

        #region 卡券统计
        /// <summary>
        /// 拉取卡券概况数据接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="beginDate">查询数据的起始时间，格式：2015-06-15</param>
        /// <param name="endDate">查询数据的截至时间，格式：2015-06-30。特别要注意，这里的结束时间不能是当天的时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns>{"list": [{"ref_date": "2015-06-23","view_cnt": 1,"view_user": 1,"receive_cnt": 1,"receive_user": 1,"verify_cnt": 0,"verify_user": 0,"given_cnt": 0,"given_user": 0,"expire_cnt": 0,"expire_user": 0 }] }</returns>
        public static string GetCardStatistical(string appId, string appSecret, DateTime beginDate, DateTime endDate, int source)
        {
            int interval = (endDate - beginDate).Days;

            endDate = interval > 62 ? beginDate.AddDays(62) : endDate;

            string url = string.Format("https://api.weixin.qq.com/datacube/getcardbizuininfo?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                begin_date = beginDate.ToString("yyyy-MM-dd"),
                end_date = endDate.ToString("yyyy-MM-dd"),
                cond_source = source
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 获取免费卡券数据接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="beginDate">查询数据的起始时间，格式：2015-06-15</param>
        /// <param name="endDate">查询数据的截至时间，格式：2015-06-30。特别要注意，这里的结束时间不能是当天的时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <param name="cardId">卡券id,填写后，指定拉出该卡券的相关数据</param>
        /// <returns>{"list": [{"ref_date": "2015-06-23","card_id": "po8pktyDLmakNY2fn2VyhkiEPqGE","card_type":3,"view_cnt": 1,"view_user": 1,"receive_cnt": 1,"receive_user": 1,"verify_cnt": 0,"verify_user": 0,"given_cnt": 0,"given_user": 0,expire_cnt": 0,"expire_user": 0}] }</returns>
        public static string GetFreeCardStatistical(string appId, string appSecret, DateTime beginDate, DateTime endDate, int source, string cardId = "")
        {
            int interval = (endDate - beginDate).Days;

            endDate = interval > 62 ? beginDate.AddDays(62) : endDate;

            string url = string.Format("https://api.weixin.qq.com/datacube/getcardcardinfo?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                begin_date = beginDate.ToString("yyyy-MM-dd"),
                end_date = endDate.ToString("yyyy-MM-dd"),
                cond_source = source,
                card_id = cardId
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 拉取特殊卡券概况数据接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="beginDate">查询数据的起始时间，格式：2015-06-15</param>
        /// <param name="endDate">查询数据的截至时间，格式：2015-06-30。特别要注意，这里的结束时间不能是当天的时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns>{"list": [{"ref_date": "2015-06-23","view_cnt": 1,"view_user": 1,"receive_cnt": 1,"receive_user": 1,"verify_cnt": 0,"verify_user": 0,"given_cnt": 0,"given_user": 0,"expire_cnt": 0,"expire_user": 0}]}</returns>
        public static string GetSpecialCardStatistical(string appId, string appSecret, DateTime beginDate, DateTime endDate, int source)
        {
            int interval = (endDate - beginDate).Days;

            endDate = interval > 62 ? beginDate.AddDays(62) : endDate;

            string url = string.Format("https://api.weixin.qq.com/datacube/getcardmembercardinfo?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                begin_date = beginDate.ToString("yyyy-MM-dd"),
                end_date = endDate.ToString("yyyy-MM-dd"),
                cond_source = source
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }
        #endregion
    }
}
