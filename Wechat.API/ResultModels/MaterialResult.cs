using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    /// <summary>
    /// 上传临时媒体文件返回结果
    /// </summary>
    public class TemporaryMaterialResult : WechatResult
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图）
        /// </summary>
        public Enums.MaterialFileEnum type { get; set; }
        /// <summary>
        /// 媒体文件上传后，获取时的唯一标识
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 上传缩略图返回的meidia_id参数.
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        public long created_at { get; set; }
    }

    /// <summary>
    /// 上传永久媒体文件返回结果
    /// </summary>
    public class PermanentMaterialResult : WechatResult
    {
        /// <summary>
        /// 媒体文件上传后，获取时的唯一标识
        /// </summary>
        public string media_id { get; set; }
    }
}
