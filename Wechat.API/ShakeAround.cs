using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API
{
    // 摘要:
    //      微信摇一摇周边接口
    // 
    public static class ShakeAround
    {
        /// <summary>
        /// 申请开通摇一摇周边功能接口
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <returns>model(摇一摇周边申请model)</returns>
        public static string Apply(string appId, string appSecret, Models.ShakeAroundApplyModel model)
        {
            string url = string.Format("https://api.weixin.qq.com/shakearound/account/register?access_token={0}", AccessToken.GetToken(appId, appSecret));

            var obj = new
            {
                name = model.Name,
                phone_number = model.PhoneNumber,
                email = model.Email,
                industry_id = model.IndustryId,
                qualification_cert_urls = model.QualificationCertUrls,
                apply_reason = model.ApplyReason
            };

            return WechatWebClient.Post(url, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 查询摇一摇周边申请的审核状态
        /// </summary>
        /// <param name="appId">公众号appID</param>
        /// <param name="appSecret">公众号appSecret</param>
        /// <returns></returns>
        public static string GetAuditStatus(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/shakearound/account/auditstatus?access_token={0}", AccessToken.GetToken(appId, appSecret));

            return WechatWebClient.Get(url);
        }
    }
}
