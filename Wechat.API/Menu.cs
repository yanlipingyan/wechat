﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat.API.Models;

namespace Wechat.API
{
    // 摘要: 
    //      自定义菜单API接口
    //
    public static class Menu
    {
        #region 普通菜单
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="model">ButtonGroupModel</param>
        /// <returns>ResultModels.WechatResult</returns>
        public static ResultModels.WechatResult CreateMenu(string appId, string appSecret, ButtonGroupModel model)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post<ResultModels.WechatResult>(url, JsonConvert.SerializeObject(model));
        }

        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <returns>string</returns>
        public static string GetMenu(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Get(url);
        }

        /// <summary>
        /// 删除所有的菜单
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <returns>ResultModels.WechatResult</returns>
        public static ResultModels.WechatResult DeleteAllMenu(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Get<ResultModels.WechatResult>(url);
        }
        #endregion

        #region 个性化菜单
        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="model">ButtonGroupModel</param>
        /// <returns>ResultModels.GetPersonaliseMenuResult</returns>
        public static ResultModels.GetPersonaliseMenuResult CreatePersonaliseMenu(string appId, string appSecret, ButtonGroupModel model)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/addconditional?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Post<ResultModels.GetPersonaliseMenuResult>(url, JsonConvert.SerializeObject(model));
        }

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="menuId">post数据（个性菜单id）</param>
        /// <returns>ResultModels.WechatResult</returns>
        public static ResultModels.WechatResult DeletePersonaliseMenu(string appId, string appSecret, string menuId)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delconditional?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new { menuid = menuId };

            return WechatWebClient.Post<ResultModels.WechatResult>(url, JsonConvert.SerializeObject(obj));
        }


        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="userId">post数据（user_id可以是粉丝的OpenID，也可以是粉丝的微信号。）</param>
        /// <returns>string</returns>
        public static string TryMatchPersonaliseMenu(string appId, string appSecret, string userId)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/trymatch?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new { user_id = userId };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }
        #endregion
    }
}
