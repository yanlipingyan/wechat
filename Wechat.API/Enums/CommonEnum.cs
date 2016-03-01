﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Enums
{
    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum OAuthScopeEnum
    {
        /// <summary>
        /// 不弹出授权页面，直接跳转，只能获取用户openid
        /// </summary>
        snsapi_base,
        /// <summary>
        /// 弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息
        /// </summary>
        snsapi_userinfo
    }

    /// <summary>
    /// 上传素材文件类型
    /// </summary>
    public enum MaterialFileEnum
    {
        /// <summary>
        /// 图片: 2M，支持bmp/png/jpeg/jpg/gif格式
        /// </summary>
        image,
        /// <summary>
        /// 语音：M，播放长度不超过60s，支持AMR\MP3格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频：10MB，支持MP4格式
        /// </summary>
        video,
        /// <summary>
        /// thumb：64KB，支持JPG格式
        /// </summary>
        thumb,
        /// <summary>
        /// 图文消息
        /// </summary>
        news
    }
}