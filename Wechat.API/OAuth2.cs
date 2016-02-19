using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API
{
    // 摘要:
    //      网页授权获取用户基本信息
    //  
    public static class OAuth2
    {
        // 摘要: 
        //     用户同意授权，获取code。
        //
        // 注释：
        //    【此时微信会返回给重定向到的url路径一个code参数，方法接受此参数即可】。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   redirect_url:
        //     授权后重定向的回调链接地址，请使用urlencode对链接进行处理。
        //
        //   scope:
        //     应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）。
        //
        //   state:
        //     重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节。
        //
        // 返回结果: code(string)
        //
        public static string GetCode(string appId, string redirect_url, string scope, string state)
        {
            return string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect", appId, redirect_url, scope, state);
        }

        // 摘要: 
        //     通过code换取网页授权access_token。
        //
        // 注释：
        //    此处的token和AccessTokenAPI处的token不一样。
        //   【则本步骤中获取到网页授权access_token的同时，也获取到了openid，snsapi_base式的网页授权流程即到此为止】。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   appSecret:
        //     公众号appSecret。
        //
        //   code:
        //     通过GetCode方法获得的code。
        //
        // 异常: 
        //   {"errcode":40029,"errmsg":"invalid code"}
        //
        // 返回结果: string
        //     {
        //          "access_token":"ACCESS_TOKEN",
        //          "expires_in":7200,
        //          "refresh_token":"REFRESH_TOKEN",
        //          "openid":"OPENID",
        //          "scope":"SCOPE",
        //          "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
        //      }
        //
        public static string GetToken(string appId, string appSecret, string code)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecret, code);

            return WechatWebClient.Get(url);
        }

        // 摘要: 
        //     检验授权凭证（access_token）是否有效。
        //
        // 参数: 
        //   openId:
        //     上面方法中获得openId。
        //
        //   token:
        //     获得的网页授权token。
        //
        // 异常: 
        //   { "errcode":40003,"errmsg":"invalid openid"}
        //
        // 返回结果: string
        //     {
        //        "errcode":0,
        //        "errmsg":"ok"
        //     }
        //
        public static string GetOAuthTokenIsValid(string openId, string token)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/auth?access_token={0}&openid={1}", token, openId);

            return WechatWebClient.Get(url);
        }

        // 摘要: 
        //     刷新access_token。
        //
        // 注释：
        //     如果需要则判断授权凭证是否有效，有效则不需刷新，无效就刷新。
        //
        // 参数: 
        //   appId:
        //     公众号appID。
        //
        //   refreshToken:
        //     通过GetToken方法获得的refresh_token。
        //
        // 异常: 
        //   {"errcode":40029,"errmsg":"invalid code"}
        //
        // 返回结果: string
        //     {
        //          "access_token":"ACCESS_TOKEN",
        //          "expires_in":7200,
        //          "refresh_token":"REFRESH_TOKEN",
        //          "openid":"OPENID",
        //          "scope":"SCOPE"
        //      }
        //
        public static string RefreshToken(string appId, string refreshToken)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", appId, refreshToken);

            return WechatWebClient.Get(url);
        }

        // 摘要: 
        //     拉取scope为 snsapi_userinfo时的用户信息。
        //
        // 参数: 
        //   openId:
        //     上面方法中获得openId。
        //
        //   token:
        //     上面方法中获得token。
        //
        //   language:
        //     返回此用户国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语
        //
        // 异常: 
        //   {"errcode":40003,"errmsg":" invalid openid "}
        //
        // 返回结果: string
        //     {
        //          "openid":" OPENID",
        //          "nickname": NICKNAME,
        //          "sex":"1",
        //          "province":"PROVINCE"
        //          "city":"CITY",
        //          "country":"COUNTRY",
        //          "headimgurl":"http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46", 
        //          "privilege":[
        //               "PRIVILEGE1"
        //               "PRIVILEGE2"
        //           ],
        //          "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
        //     }
        //
        public static string GetUserInfo(string openId, string token, string language)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}", token, openId, language);

            return WechatWebClient.Get(url);
        }
    }
}
