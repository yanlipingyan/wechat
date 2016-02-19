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
        // 摘要: 
        //     获取卡券jsapiTicket。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        // 返回结果: 卡券jsapiTicket(string)
        //
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

        // 摘要: 
        //     微信卡券JS-SDk签名。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   noncestr:
        //     随机字符串(需要与调用JS接口页面的wx.config中的nonceStr相同)。
        //
        //   timestamp:
        //     时间戳(需要与调用JS接口页面的wx.config中的timestamp相同)。
        //
        //   shopId:
        //     门店ID。
        //
        //   cardType:
        //     卡券类型。
        //
        //   cardId:
        //     卡券id。
        //
        // 返回结果: 签名(string)
        //
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

        // 摘要: 
        //     微信卡券扩展JS-SDk签名。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   noncestr:
        //     随机字符串(需要与调用JS接口页面的wx.config中的nonceStr相同)。
        //
        //   timestamp:
        //     时间戳(需要与调用JS接口页面的wx.config中的timestamp相同)。
        //
        //   url:
        //     url（当前网页的URL，url必须是调用JS接口页面的完整URL，不包含#及其后面部分。）。
        //
        // 返回结果: 签名(string)
        //
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

        #region 卡券操作

        #region 创建卡券
        // 摘要: 
        //     上传卡券Logo。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   data:
        //     post数据（json格式）。
        //
        // 返回结果: 
        //      成功：{"url":"http://mmbiz.qpic.cn/mmbiz/iaL1LJM1mF9aRKPZJkmG8xXhiaHqkKSVMMWeN3hLut7X7hicFNjakmxibMLGWpXrEXB33367o7zHN0CwngnQY7zb7g/0"}
        //      失败：{"errcode":40009,"errmsg":"invalid image size"}
        //
        public static string UploadLogo(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data);
        }

        // 摘要: 
        //     创建卡券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   data:
        //     post数据（json格式）。
        //
        // 返回结果: 
        //      {"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}
        //
        public static string CreateCard(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/card/create?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data.ToString().Replace("\\", "").Replace("\"{", "{").Replace("}\"", "}"));
        }

        // 摘要: 
        //     创建团购券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   model:
        //     团购券实体对象。
        //
        // 返回结果: 
        //      {"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}
        //
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

        // 摘要: 
        //     创建代金券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   model:
        //     代金券实体对象。
        //
        // 返回结果: 
        //      {"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}
        //
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

        // 摘要: 
        //     创建折扣券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   model:
        //     折扣券实体对象。
        //
        // 返回结果: 
        //      {"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}
        //
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

        // 摘要: 
        //     创建礼品券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   model:
        //     礼品券实体对象。
        //
        // 返回结果: 
        //      {"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}
        //
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

        // 摘要: 
        //     创建优惠券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   model:
        //     优惠券实体对象。
        //
        // 返回结果: 
        //      {"errcode":0,"errmsg":"ok","card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"}
        //
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
        // 摘要: 
        //     创建卡券二维码图片。
        //
        //  注释：
        //     这里返回的直接是一张图片，可以直接展示和下载
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   cardId:
        //     卡券id。
        //
        //   openId:
        //     指定领取者的openid，只有该用户能领取。bind_openid字段为true的卡券必须填写，非指定openid不必填写。
        //
        //   code:
        //     卡券Code码,use_custom_code字段为true的卡券必须填写，非自定义code不必填写。
        //
        //   isNeverExpires:
        //     是否绝不过期，True：是；False：会过期。
        //
        //   validTime:
        //     当isNeverExpires为False时需要指定有效时间。
        //
        // 返回结果: 
        //      二维码图片
        //
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

        // 摘要: 
        //     创建货架接口。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   data:
        //     post内容（json字符串）。
        //
        // 返回结果: 
        //{
        //     "errcode":0,
        //     "errmsg":"ok",
        //     "url":"www.test.url",
        //     "page_id":1
        // }
        //
        public static string CreateShelves(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/card/landingpage/create?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data);
        }
        #endregion

        #region 管理卡券
        // 摘要: 
        //     查询卡券信息。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   cardId:
        //     卡券id。
        //
        // 返回结果: 
        //      当check_consume为true时返回数据:
        //        {
        //          "errcode":0,
        //          "errmsg":"ok",
        //          "openid":"oFS7Fjl0WsZ9AMZqrI80nbIq8xrA",
        //          "card":{
        //              "card_id":"pFS7Fjg8kV1IdDz01r4SQwMkuCKc",
        //              "begin_time": 1404205036,
        //              "end_time": 1404205036,
        //              "user_card_status": "NORMAL",
        //              "can_consume":"true"
        //           }
        //        }
        //        或
        //        {
        //           "errcode": 40127,
        //           "errmsg": "invalid user-card status! Hint: the card was given to user, but may be    deleted or set unavailable ! hint: [iHBD40040ent3]"
        //        }
        //      当check_consume为false时返回数据:
        //        {
        //           "errcode":0,
        //           "errmsg":"ok",
        //           "openid":"oFS7Fjl0WsZ9AMZqrI80nbIq8xrA",
        //           "card":{
        //              "card_id":"pFS7Fjg8kV1IdDz01r4SQwMkuCKc",
        //              "begin_time": 1404205036,
        //              "end_time": 1404205036
        //              "can_consume":"true"
        //            }
        //        }
        //        或
        //         {
        //             "errcode":0,
        //              "errmsg":"ok",
        //              "openid":"oFS7Fjl0WsZ9AMZqrI80nbIq8xrA",
        //              "card":{
        //                  "card_id":"pFS7Fjg8kV1IdDz01r4SQwMkuCKc",
        //                  "begin_time": 1404205036,
        //                  "end_time": 1404205036,
        //                  "user_card_status": "GIFTING",
        //                  "can_consume":"false"
        //              }
        //          }
        //
        public static string QueryCode(string appId, string appSecret, string cardId, string code, bool check_consume = false)
        {
            string url = string.Format("https://api.weixin.qq.com/card/code/get?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                card_id = cardId,
                code = code,
                check_consume = check_consume
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        // 摘要: 
        //     查询该用户卡包里属于该appid下的卡券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   openID:
        //     卡券状态。
        //
        //   cardId:
        //     卡券状态，不填写时默认查询当前appid下的卡券。。
        //
        // 返回结果: 
        //      {
        //          "errcode":0,
        //          "errmsg":"ok",
        //          "card_list": [
        //              {"code": "xxx1434079154", "card_id": "xxxxxxxxxx"},
        //              {"code": "xxx1434079155", "card_id": "xxxxxxxxxx"}
        //           ]
        //       }
        //
        public static string QueryCardListForUser(string appId, string appSecret, string openID, string cardId = "")
        {
            string url = string.Format("https://api.weixin.qq.com/card/user/getcardlist?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                openid = openID,
                card_id = cardId
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        // 摘要: 
        //     查询卡券详情。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   cardId:
        //     卡券id。
        //
        // 返回结果: 
        //{
        //   "errcode": 0,
        //   "errmsg": "ok",
        //   "card": {
        //       "card_type":  "GROUPON",
        //           "groupon": {
        //           "base_info": {
        //               "status": 1,
        //               "id": "p1Pj9jr90_SQRaVqYI239Ka1erkI",
        //               "logo_url": "http://mmbiz.qpic.cn/mmbiz/iaL1LJM1mF9aRKPZJkmG8xXhiaHqkKSVMMWeN3hLut7X7hicFNjakmxibMLGWpXrEXB33367o7zHN0CwngnQY7zb7g/0",
        //               "appid": "wx588def6b0089dd48",
        //               "code_type": "CODE_TYPE_TEXT",
        //               "brand_name": "海底捞",
        //               "title": "132元双人火锅套餐",
        //               "sub_title": "DATE_TYPE_FIX_TIME_RANGE",
        //               "date_info": {
        //                   "type": "DATE_TYPE_FIX_TIME_RANGE",
        //                   "begin_timestamp": 1397577600,
        //                   "end_timestamp": 1399910400
        //               },
        //               "color": "#3373bb",
        //               "notice": "使用时向服务员出示此券",
        //               "service_phone": "020-88888888",
        //               "description": "不可与其他优惠同享\n如需团购券发票，请在消费时向商户提出\n店内均可使用，仅限堂食\n餐前不可打包，餐后未吃完，可打包\n本团购券不限人数，建议2人使用，超过建议人数须另收酱料费5元/位\n本单谢绝自带酒水饮料",
        //               "use_limit": 1,
        //               "get_limit": 3,
        //               "can_share": true,
        //               "location_id_list" : [123, 12321, 345345]
        //               "custom_url_name": "立即使用",
        //               "custom_url": "http://www.qq.com",
        //               "custom_url_sub_title": "6个汉字tips",
        //               "promotion_url_name": "更多优惠",
        //               "promotion_url": "http://www.qq.com",
        //               "source": "大众点评"   
        //               "sku": {
        //                   "quantity": 0
        //                   "total_quantity":1000
        //               }
        //           },
        //           "deal_detail": "以下锅底2选1（有菌王锅、麻辣锅、大骨锅、番茄锅、清补凉锅、酸菜鱼锅可选）：\n大锅1份 12元\n小锅2份 16元\n以下菜品2选1\n特级肥牛1份 30元\n洞庭鮰鱼卷1份 20元\n其他\n鲜菇猪肉滑1份 18元\n金针菇1份 16元\n黑木耳1份 9元\n娃娃菜1份 8元\n冬瓜1份 6元\n火锅面2个 6元\n欢乐畅饮2位 12元\n自助酱料2位 10元",
        //       }
        //
        public static string QueryCardDetail(string appId, string appSecret, string cardId)
        {
            string url = string.Format("https://api.weixin.qq.com/card/get?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                card_id = cardId,
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        // 摘要: 
        //     查询状态卡券的列表。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   data:
        //     post数据（json格式字符串）。
        //
        // 返回结果: 
        //  {
        //      "errcode":0,
        //      "errmsg":"ok",
        //      "card_id_list":["ph_gmt7cUVrlRk8swPwx7aDyF-pg"],
        //      "total_num":1
        //  }
        //
        public static string QueryCardList(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/card/batchget?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data);
        }

        // 摘要: 
        //     查询某状态卡券的列表。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   cardStatus:
        //     卡券状态,格式：["","",""]。
        //
        //   offset:
        //     查询卡列表的起始偏移量，从0开始，即offset: 5是指从从列表里的第六个开始读取。。
        //
        //   count:
        //     需要查询的卡片的数量（数量最大50）。
        //
        // 返回结果: 
        //  {
        //      "errcode":0,
        //      "errmsg":"ok",
        //      "card_id_list":["ph_gmt7cUVrlRk8swPwx7aDyF-pg"],//卡券ID列表。
        //      "total_num":1 //该商户名下卡券ID总数
        //  }
        //
        public static string QueryCardList(string appId, string appSecret, string[] cardStatus, int offset = 0, int count = 50)
        {
            var obj = new
            {
                offset = offset < 0 ? 0 : offset,
                count = count > 50 ? 50 : count,
                status_list = cardStatus
            };

            return QueryCardList(appId, appSecret, JsonConvert.SerializeObject(obj));
        }

        // 摘要: 
        //     更新卡券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   data:
        //     post内容（json格式的字符串）。
        //
        // 返回结果: 
        //      {
        //          "errcode":0,
        //          "errmsg":"ok",
        //          "send_check":false
        //      }
        //
        public static string UpdateCard(string appId, string appSecret, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/card/update?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post(url, data);
        }

        // 摘要: 
        //     设置卡券买单接口。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   cardId:
        //     卡券id。
        //
        //   isOpen:
        //     是否开启买单功能，填true/false。
        //
        // 返回结果: 
        //      {
        //          "errcode":0,
        //          "errmsg":"ok"
        //      }
        //
        public static string SetCardCanPay(string appId, string appSecret, string cardId, bool isOpen = true)
        {
            var card = QueryCardDetail(appId, appSecret, cardId);

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

        // 摘要: 
        //     修改库存接口。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   cardId:
        //     卡券id。
        //
        //   increase_stock_value:
        //     增加多少库存，支持不填或填0。
        //
        //   reduce_stock_value:
        //     减少多少库存，可以不填或填0。
        //
        // 返回结果: 
        //      {
        //          "errcode":0,
        //          "errmsg":"ok"
        //      }
        //
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

        // 摘要: 
        //     更改Code接口。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   code:
        //     需变更的Code码。
        //
        //   newCode:
        //     变更后的有效Code码。
        //
        //   cardId:
        //     卡券id，自定义Code码卡券为必填。
        //
        // 返回结果: 
        //       {
        //          "errcode":0,
        //          "errmsg":"ok",
        //       }
        //
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

        // 摘要: 
        //     删除卡券。
        //
        // 注释：
        //     删除卡券不能删除已被用户领取，保存在微信客户端中的卡券。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   cardId:
        //     卡券id。
        //
        // 返回结果: 
        //      {
        //          "errcode":0,
        //          "errmsg":"ok"
        //      }  
        //
        public static string DeleteCard(string appId, string appSecret, string cardId)
        {
            string url = string.Format("https://api.weixin.qq.com/card/delete?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                card_id = cardId
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        // 摘要: 
        //     设置卡券失效接口。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   code:
        //     设置失效的Code码。
        //
        //   cardId:
        //     卡券id,非自定义卡券不需要输入。
        //
        // 返回结果: 
        //      {
        //          "errcode":0,
        //          "errmsg":"ok"
        //      }
        //
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
        // 摘要: 
        //     拉取卡券概况数据接口。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   beginDate:
        //     查询数据的起始时间，格式：2015-06-15。
        //
        //   endDate:
        //     查询数据的截至时间，格式：2015-06-30。特别要注意，这里的结束时间不能是当天的时间。
        //
        //   source:
        //     卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据。
        //
        // 返回结果: 
        //{
        //    "list": [
        //       {
        //           "ref_date": "2015-06-23",
        //           "view_cnt": 1,
        //           "view_user": 1,
        //           "receive_cnt": 1,
        //           "receive_user": 1,
        //           "verify_cnt": 0,
        //           "verify_user": 0,
        //           "given_cnt": 0,
        //           "given_user": 0,
        //           "expire_cnt": 0,
        //           "expire_user": 0
        //       }
        //   ] 
        //}
        //
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

        // 摘要: 
        //     获取免费卡券数据接口。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   beginDate:
        //     查询数据的起始时间，格式：2015-06-15。
        //
        //   endDate:
        //     查询数据的截至时间，格式：2015-06-30。特别要注意，这里的结束时间不能是当天的时间。
        //
        //   source:
        //     卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据。
        //
        //   cardId:
        //     卡券id,填写后，指定拉出该卡券的相关数据。
        //
        // 返回结果: 
        //{
        //    "list": [
        //       {
        //           "ref_date": "2015-06-23",
        //           "card_id": "po8pktyDLmakNY2fn2VyhkiEPqGE",
        //           "card_type":3,
        //           "view_cnt": 1,
        //           "view_user": 1,
        //           "receive_cnt": 1,
        //           "receive_user": 1,
        //           "verify_cnt": 0,
        //           "verify_user": 0,
        //           "given_cnt": 0,
        //           "given_user": 0,
        //           "expire_cnt": 0,
        //           "expire_user": 0
        //       }
        //   ] 
        //}
        //
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

        // 摘要: 
        //     拉取特殊卡券概况数据接口。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   beginDate:
        //     查询数据的起始时间，格式：2015-06-15。
        //
        //   endDate:
        //     查询数据的截至时间，格式：2015-06-30。特别要注意，这里的结束时间不能是当天的时间。
        //
        //   source:
        //     卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据。
        //
        // 返回结果: 
        //{
        //    "list": [
        //       {
        //           "ref_date": "2015-06-23",
        //           "view_cnt": 1,
        //           "view_user": 1,
        //           "receive_cnt": 1,
        //           "receive_user": 1,
        //           "verify_cnt": 0,
        //           "verify_user": 0,
        //           "given_cnt": 0,
        //           "given_user": 0,
        //           "expire_cnt": 0,
        //           "expire_user": 0
        //       }
        //   ] 
        //}
        //
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

        #endregion
    }
}
