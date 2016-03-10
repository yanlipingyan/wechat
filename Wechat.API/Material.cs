using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Wechat.API
{
    // 摘要: 
    //      素材管理API接口
    //
    public static class Material
    {
        #region 临时素材
        /// <summary>
        /// 新增临时素材（原上传媒体文件）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="type"></param>
        /// <param name="file"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ResultModels.TemporaryMaterialResult UploadTemporaryMedia(string appId, string appSecret, Enums.MaterialFileEnum type, string file)
        {
            var url = string.Format("http://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", AccessToken.GetToken(appId, appSecret), type.ToString());

            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            return new ResultModels.TemporaryMaterialResult();// HttpUtility.Post.PostFileGetJson<ResultModels.TemporaryMaterialResult>(url, null, fileDictionary, null, timeOut: timeOut);
            //return ApiHandlerWapper.TryCommonApi(accessToken =>
            //{
            //    var url = string.Format("http://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", accessToken, type.ToString());
            //    var fileDictionary = new Dictionary<string, string>();
            //    fileDictionary["media"] = file;
            //    return HttpUtility.Post.PostFileGetJson<UploadTemporaryMediaResult>(url, null, fileDictionary, null, timeOut: timeOut);

            //}, accessTokenOrAppId);
        }

        public static string GetTemporaryMedia(string appId, string appSecret, string media_id)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", AccessToken.GetToken(appId, appSecret), media_id);

            return WechatWebClient.Get(url);
        }

        public static void GetTemporaryMedia(string appId, string appSecret, string media_id, Stream stream)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", AccessToken.GetToken(appId, appSecret), media_id);

            WechatWebClient.Download(url, stream);
        }
        #endregion

        #region 永久素材
        #endregion
    }
}
