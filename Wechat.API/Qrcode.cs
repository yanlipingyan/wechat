using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API
{
    // 摘要: 
    //      微信二维码接口调用凭据
    //
    public static class Qrcode
    {
        /// <summary>
        /// 创建临时二维码图片URL
        /// 注释：这里返回的直接是一张图片，可以直接展示和下载
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="scene_id">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）。</param>
        /// <returns>二维码图片URL</returns>
        public static string ShowTemporaryQrcode(string appId, string appSecret, int scene_id)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                expire_seconds = "3600",
                action_name = "QR_SCENE",
                action_info = new
                {
                    scene = new
                    {
                        scene_id = scene_id
                    }
                }
            };

            var result = WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));

            var ticket = JsonConvert.DeserializeObject<dynamic>(result)["ticket"].ToString();

            return string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", Uri.EscapeDataString(ticket));
        }

        /// <summary>
        /// 创建永久二维码图片URL。
        /// 注释：这里返回的直接是一张图片，可以直接展示和下载
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <param name="scene_id">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）。</param>
        /// <returns>二维码图片URL</returns>
        public static string ShowPermanentQrcode(string appId, string appSecret, int scene_id)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", AccessToken.GetToken(appId, appSecret));
            var obj = new
            {
                action_name = "QR_LIMIT_SCENE",
                action_info = new
                {
                    scene = new
                    {
                        scene_id = scene_id
                    }
                }
            };

            var result = WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));

            var ticket = JsonConvert.DeserializeObject<dynamic>(result)["ticket"].ToString();

            return string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", Uri.EscapeDataString(ticket));
        }
    }
}
