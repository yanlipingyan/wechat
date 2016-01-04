using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Wechat.API
{
    public class JsSdk
    {
        // 摘要: 
        //     获取jsapiTicket。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        // 返回结果: jsapiTicket(string)
        //
        public static string GetTicket(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", AccessToken.GetToken(appId, appSecret));

            var model = Helper.JsApiTicketHelper.Get("jsapi");

            if (model == null || string.IsNullOrEmpty(model.Ticket) || Common.IsExprie(model.DateTime))
            {
                string result = WebHttpClient.Get(url);

                model.Ticket = JsonConvert.DeserializeObject<dynamic>(result)["ticket"];
                model.DateTime = DateTime.Now;

                Helper.JsApiTicketHelper.Set("jsapi", model);
            }

            return model.Ticket;
        }

        // 摘要: 
        //     微信JS-SDk签名。
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
        public static string GetSign(string appId, string appSecret, string noncestr, string timestamp, string url)
        {
            string jsapi_ticket = JsSdk.GetTicket(appId, appSecret);

            url = url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url;

            //1.对所有待签名参数按照字段名的ASCII 码从小到大排序（字典序）后
            SortedDictionary<string, object> orderParams = new SortedDictionary<string, object>();

            orderParams.Add("noncestr", noncestr);
            orderParams.Add("timestamp", timestamp);
            orderParams.Add("url", url);
            orderParams.Add("jsapi_ticket", jsapi_ticket);

            //2.将orderParams里面的键值对使用URL键值对的格式（即key1=value1&key2=value2…）拼接成字符串
            string stringA = Common.SortedDictionaryToUrl(orderParams);

            //3.对上面的字符串进行sha1签名
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes = UTF8Encoding.Default.GetBytes(stringA);
            byte[] bytesHash = sha1.ComputeHash(bytes);

            return BitConverter.ToString(bytesHash).Replace("-", "").ToLower();
        }
    }
}
