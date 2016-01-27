using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.WebUI
{
    // 摘要: 
    //      获取普通access_token
    //
    public static class AccessToken
    {
        // 摘要: 
        //     获取Token。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        // 返回结果: token(string)
        //
        public static string GetToken(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret);

            var tokenModel = Helper.AccessTokenHelper.Get(appId);

            //如果token不存在或已过期则重新生成Token
            if (tokenModel == null || string.IsNullOrEmpty(tokenModel.Token) || Common.IsExprie(tokenModel.DateTime))
            {
                string result = WebHttpClient.Get(url);

                tokenModel.Token = JsonConvert.DeserializeObject<dynamic>(result)["access_token"];
                tokenModel.DateTime = DateTime.Now;

                Helper.AccessTokenHelper.Set(appId, tokenModel);
            }

            return tokenModel.Token;
        }
    }
}
