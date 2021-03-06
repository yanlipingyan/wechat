﻿using System;
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
        /// <summary>
        /// 用户同意授权，获取code
        /// 注释：【此时微信会返回给重定向到的url路径一个code参数，方法接受此参数即可】。
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="redirect_url">授权后重定向的回调链接地址，请使用urlencode对链接进行处理。</param>
        /// <param name="scope">应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）。</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节</param>
        /// <returns>string</returns>
        public static string GetCode(string appId, string redirect_url, Enums.OAuthScopeEnum scope, string state)
        {
            /* 这一步发送之后，客户会得到授权页面，无论同意或拒绝，都会返回redirectUrl页面。
             * 如果用户同意授权，页面将跳转至 redirect_uri/?code=CODE&state=STATE。这里的code用于换取access_token（和通用接口的access_token不通用）
             * 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE
             */

            return string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect", appId, System.Web.HttpUtility.UrlEncode(redirect_url), scope, state);
        }

        /// <summary>
        /// 通过code换取网页授权access_token
        /// 注释：此处的token和AccessTokenAPI处的token不一样。
        /// 【则本步骤中获取到网页授权access_token的同时，也获取到了openid，snsapi_base式的网页授权流程即到此为止】。
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="code">通过GetCode方法获得的code</param>
        /// <returns>ResultModels.Oauth2TokenResult</returns>
        public static ResultModels.Oauth2TokenResult GetToken(string appId, string appSecret, string code)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecret, code);

            return WechatWebClient.Get<ResultModels.Oauth2TokenResult>(url);
        }

        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="openId">上面方法中获得openId</param>
        /// <param name="token">获得的网页授权token</param>
        /// <returns>ResultModels.WechatResult</returns>
        public static ResultModels.WechatResult GetOAuthTokenIsValid(string openId, string token)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/auth?access_token={0}&openid={1}", token, openId);

            return WechatWebClient.Get<ResultModels.WechatResult>(url);
        }

        /// <summary>
        /// 刷新access_token
        /// 注释：如果需要则判断授权凭证是否有效，有效则不需刷新，无效就刷新
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="refreshToken">通过GetToken方法获得的refresh_token</param>
        /// <returns>ResultModels.Oauth2TokenResult</returns>
        public static ResultModels.Oauth2TokenResult RefreshToken(string appId, string refreshToken)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", appId, refreshToken);

            return WechatWebClient.Get<ResultModels.Oauth2TokenResult>(url);
        }

        /// <summary>
        /// 拉取scope为 snsapi_userinfo时的用户信息
        /// </summary>
        /// <param name="openId">上面方法中获得openId</param>
        /// <param name="token">上面方法中获得token</param>
        /// <param name="language">返回此用户国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns>ResultModels.OAuth2UserInfoResult</returns>
        public static ResultModels.OAuth2UserInfoResult GetUserInfo(string openId, string token, string language)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}", token, openId, language);

            return WechatWebClient.Get<ResultModels.OAuth2UserInfoResult>(url);
        }
    }
}
