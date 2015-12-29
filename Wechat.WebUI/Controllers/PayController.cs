﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThoughtWorks.QRCode.Codec;

namespace Wechat.WebUI.Controllers
{
    public class PayController : Controller
    {
        #region JSAPI--公众号支付
        /// <summary>
        /// 公众号支付页面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GongZhongHaoPay(string id, string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var token = Wechat.API.OAuth2.GetToken(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, code);

                ViewBag.Oauth2Token = JsonConvert.DeserializeObject<dynamic>(token)["access_token"];
                ViewBag.AppId = Wechat.API.Model.ApiModel.AppID;

                //将此微信用户openId保存到cookie里
                HttpCookie cookie = new HttpCookie("OpenId");//初使化并设置Cookie的名称
                cookie.Value = JsonConvert.DeserializeObject<dynamic>(token)["openid"];
                cookie.Expires = DateTime.Now.AddDays(1);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                return View(new Models.Product() { Id = "1", Title = "测试商品", Description = "优惠大酬宾，赶紧的~", Price = 1 });
            }

            return Redirect(Wechat.API.OAuth2.GetCode(Wechat.API.Model.ApiModel.AppID, Request.Url.AbsoluteUri, "snsapi_base", "state"));
        }

        /// <summary>
        /// 公众号支付
        /// </summary>
        /// <param name="title">商品标题</param>
        /// <param name="description">商品描述</param>
        /// <param name="price">商品价格</param>
        /// <returns>返回的是json字符串</returns>
        [HttpPost]
        public string JsPay(string title, string description, int price)
        {
            //统一下单
            SortedDictionary<string, object> orderParams = new SortedDictionary<string, object>();

            orderParams.Add("appid", Wechat.API.Model.ApiModel.AppID);
            orderParams.Add("attach", title);
            orderParams.Add("body", description);
            orderParams.Add("mch_id", Wechat.API.Model.ApiModel.MchID);
            orderParams.Add("nonce_str", Wechat.API.Common.GetNonceStr());
            orderParams.Add("notify_url", "http://ylp.dmeiyang.com/pay/payresultnotify");
            orderParams.Add("openid", System.Web.HttpContext.Current.Request.Cookies["OpenId"].Value.ToString());
            orderParams.Add("out_trade_no", Wechat.API.Pay.GetOutTradeNo(Wechat.API.Model.ApiModel.MchID));
            orderParams.Add("spbill_create_ip", "171.8.215.143");
            orderParams.Add("total_fee", price);
            orderParams.Add("trade_type", "JSAPI");
            orderParams.Add("sign", Wechat.API.Pay.GetSign(orderParams, Wechat.API.Model.ApiModel.MchAPISecret));

            var result = Wechat.API.Pay.UnifiedOrder(orderParams);

            //统一下单失败，返回错误结果给微信平台
            if (result["return_code"].ToString() != "SUCCESS")
                return JsonConvert.SerializeObject(Wechat.API.Pay.ErrorInfo("统一下单失败"));

            //获取H5调起JS API参数
            SortedDictionary<string, object> apiParams = new SortedDictionary<string, object>();

            apiParams.Add("appId", Wechat.API.Model.ApiModel.AppID);
            apiParams.Add("timeStamp", Wechat.API.Common.GetTimeStamp());
            apiParams.Add("nonceStr",Wechat.API.Common.GetNonceStr());
            apiParams.Add("package", "prepay_id=" + result["prepay_id"].ToString());
            apiParams.Add("signType", "MD5");
            apiParams.Add("paySign", Wechat.API.Pay.GetSign(apiParams, Wechat.API.Model.ApiModel.MchAPISecret));

            return JsonConvert.SerializeObject(apiParams);
        }
        #endregion

        #region NATIVE--原生扫码支付

        #region 模式一

        //注：模式一的支付需要配置回调URL

        /// <summary>
        /// 扫码支付-模式一
        /// </summary>
        /// <returns></returns>
        public ActionResult SaoMaPayOne()
        {
            return View();
        }

        /// <summary>
        /// 扫码支付模式一生成二维码
        /// </summary>
        /// <param name="id">订单号</param>
        public void NativePayOne(string id)
        {
            SortedDictionary<string, object> orderParams = new SortedDictionary<string, object>();
            orderParams.Add("appid", Wechat.API.Model.ApiModel.AppID);//公众帐号id
            orderParams.Add("mch_id", Wechat.API.Model.ApiModel.MchID);//商户号
            orderParams.Add("time_stamp",Wechat.API.Common.GetTimeStamp());//时间戳
            orderParams.Add("nonce_str",Wechat.API.Common.GetNonceStr());//随机字符串
            orderParams.Add("product_id", id);//商品订单号
            orderParams.Add("sign", Wechat.API.Pay.GetSign(orderParams, Wechat.API.Model.ApiModel.MchAPISecret));//签名

            //预支付URL
            var url = Wechat.API.Pay.GetPayUrlForNativeOne(orderParams);

            //初始化二维码生成工具
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeScale = 4;

            //将字符串生成二维码图片
            Bitmap image = qrCodeEncoder.Encode(url, Encoding.Default);

            //保存为PNG到内存流  
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);

            //输出二维码图片
            Response.BinaryWrite(ms.GetBuffer());
            Response.End();
        }

        /// <summary>
        /// 扫码支付回调URL---扫码支付模式一
        /// </summary>
        /// <returns></returns>
        public string CallbackUrl()
        {
            var result = Wechat.API.Pay.GetNotifyData();

            if (string.IsNullOrEmpty(result["openid"].ToString()) || string.IsNullOrEmpty(result["product_id"].ToString()))
                return Wechat.API.Common.SortedDictionaryToXml(Wechat.API.Pay.ErrorInfo("回调数据异常"));

            //商户根据productid【在生成二维码时传的是订单号就是订单号，是商品号就是商品号，要对应起来】生成商户系统的订单
            //:TODO

            //统一下单
            var orderParams = new SortedDictionary<string, object>();
            orderParams.Add("appid", Wechat.API.Model.ApiModel.AppID);
            orderParams.Add("attach", "微信扫码支付");
            orderParams.Add("body", "扫码支付一测试");
            orderParams.Add("mch_id", Wechat.API.Model.ApiModel.MchID);
            orderParams.Add("nonce_str",Wechat.API.Common.GetNonceStr());
            orderParams.Add("notify_url", "http://ylp.dmeiyang.com/pay/payresultnotify");//支付成功后的回调URl
            orderParams.Add("openid", result["openid"].ToString());
            orderParams.Add("product_id", result["product_id"].ToString());//trade_type=NATIVE，此参数必传。此id为二维码中包含的商品ID，商户自行定义。
            orderParams.Add("out_trade_no", Wechat.API.Pay.GetOutTradeNo(Wechat.API.Model.ApiModel.MchID));//result["product_id"].ToString()
            orderParams.Add("spbill_create_ip", "171.8.215.143");
            orderParams.Add("total_fee", 1);
            orderParams.Add("trade_type", "NATIVE");
            orderParams.Add("sign", Wechat.API.Pay.GetSign(orderParams, Wechat.API.Model.ApiModel.MchAPISecret));

            var orderResult = Wechat.API.Pay.UnifiedOrder(orderParams);

            //统一下单失败，返回错误结果给微信平台
            if (orderResult["return_code"].ToString() != "SUCCESS")
                return Wechat.API.Common.SortedDictionaryToXml(Wechat.API.Pay.ErrorInfo("统一下单失败"));

            //统一下单成功,则返回成功结果给微信支付后台
            var resultParams = new SortedDictionary<string, object>();
            resultParams.Add("return_code", "SUCCESS");
            resultParams.Add("return_msg", "OK");
            resultParams.Add("appid", Wechat.API.Model.ApiModel.AppID);
            resultParams.Add("mch_id", Wechat.API.Model.ApiModel.MchID);
            resultParams.Add("nonce_str",Wechat.API.Common.GetNonceStr());
            resultParams.Add("prepay_id", orderResult["prepay_id"].ToString());
            resultParams.Add("result_code", "SUCCESS");
            resultParams.Add("err_code_des", "OK");
            resultParams.Add("sign", Wechat.API.Pay.GetSign(resultParams, Wechat.API.Model.ApiModel.MchAPISecret));

            return Wechat.API.Common.SortedDictionaryToXml(resultParams);
        }
        #endregion

        #region 模式二

        //注：模式二的支付不需要配置回调URL

        /// <summary>
        /// 扫码支付-模式二
        /// </summary>
        /// <returns></returns>
        public ActionResult SaoMaPayTwo()
        {
            return View();
        }

        /// <summary>
        /// 扫码支付模式二生成二维码
        /// </summary>
        /// <param name="id">商品id</param>
        public void NativePayTwo(string id)
        {
            //商户根据productid【在生成二维码时传的是订单号就是订单号，是商品号就是商品号，要对应起来】生成商户系统的订单
            //:TODO

            //统一下单
            SortedDictionary<string, object> orderParams = new SortedDictionary<string, object>();
            orderParams.Add("appid", Wechat.API.Model.ApiModel.AppID);
            orderParams.Add("attach", "微信扫码支付");//附加数据
            orderParams.Add("body", "扫码支付二测试");//商品描述
            orderParams.Add("mch_id", Wechat.API.Model.ApiModel.MchID);
            orderParams.Add("nonce_str",Wechat.API.Common.GetNonceStr());
            orderParams.Add("notify_url", "http://ylp.dmeiyang.com/pay/payresultnotify");//支付成功后的回调URl
            orderParams.Add("product_id", id);//商品标记
            orderParams.Add("out_trade_no", Wechat.API.Pay.GetOutTradeNo(Wechat.API.Model.ApiModel.MchID));//随机字符串
            orderParams.Add("spbill_create_ip", "171.8.215.143");
            orderParams.Add("total_fee", 1);//总金额
            orderParams.Add("trade_type", "NATIVE");//商品订单号
            orderParams.Add("sign", Wechat.API.Pay.GetSign(orderParams, Wechat.API.Model.ApiModel.MchAPISecret));

            //直接支付URL
            var url = Wechat.API.Pay.GetPayUrlForNativeTwo(orderParams);

            //初始化二维码生成工具
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeScale = 4;

            //将字符串生成二维码图片
            Bitmap image = qrCodeEncoder.Encode(url, Encoding.Default);

            //保存为PNG到内存流  
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);

            //输出二维码图片
            Response.BinaryWrite(ms.GetBuffer());
            Response.End();
        }
        #endregion

        #endregion

        /// <summary>
        /// 支付成功后的回调
        /// </summary>
        /// <returns>string</returns>
        public string PayResultNotify()
        {
            var result = Wechat.API.Pay.PayResultNotify();

            return Wechat.API.Common.SortedDictionaryToXml(result);
        }

    }
}
