using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API
{
    // 摘要:
    //      获取微信服务器IP地址
    //  
    public static class WechatServer
    {
        // 摘要: 
        //     获取微信服务器Ip地址列表。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        // 返回结果: Ip地址列表(string[])
        //
        public static string[] GetIP(string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}";//获取微信服务器Ip地址的url。

            string result = WebHttpClient.Get(string.Format(url, AccessToken.GetToken(appId, appSecret)));

            return JsonConvert.DeserializeObject<dynamic>(result)["ip_list"];
        }

        // 摘要: 
        //     获取是否是微信内置浏览器。
        //
        // 返回结果: True，是；False，否
        //
        public static bool GetIsWx()
        {
            return System.Web.HttpContext.Current.Request.UserAgent.ToString().ToLower().Contains("micromessenger");
        }

        // 摘要: 
        //     获取微信版本号。
        //
        // 返回结果: 版本号
        //
        public static string GetVersion()
        {
            var userAgent = System.Web.HttpContext.Current.Request.UserAgent.ToString().ToLower();
            if (userAgent.Contains("micromessenger"))
            {
                string microMessenger = userAgent.Substring(userAgent.IndexOf("micromessenger"));

                string[] array = microMessenger.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                if (array.Count() >= 2)
                    return array[1];
            }

            return "";
        }
    }
}
