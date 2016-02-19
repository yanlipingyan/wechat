using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API
{
    public static class SendMessage
    {
        #region 发送模版消息
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="openId">openid</param>
        /// <param name="templateId">模板id</param>
        /// <param name="url">跳转地址</param>
        /// <param name="color">消息颜色</param>
        /// <param name="data">消息内容</param>
        /// <returns>响应内容</returns>
        public static string SendTempleteMessage(string appId, string appSecret, string openId, string templateId, string url, string color, string data)
        {
            string address = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", AccessToken.GetToken(appId, appSecret));

            object obj = new
            {
                touser = openId,
                template_id = templateId,
                url = url,
                topcolor = color,
                data = data
            };

            return WechatWebClient.Post(address, JsonConvert.SerializeObject(obj));
        }
        #endregion
    }
}
