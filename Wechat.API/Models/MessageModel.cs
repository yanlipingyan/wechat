using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessageModel : BasicMessageModel
    {
        public string Content { get; set; }//文本消息内容
        public long MsgId { get; set; }//消息id，64位整型

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "text");
            builder.AppendFormat("<Content><![CDATA[{0}]]></Content>", Content);
            builder.AppendFormat("<MsgId>{0}</MsgId>", MsgId);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMessageModel : BasicMessageModel
    {
        public string PicUrl { get; set; }//图片链接
        public string MediaId { get; set; }//图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        public long MsgId { get; set; }//消息id，64位整型

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "image");
            builder.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", PicUrl);
            builder.AppendFormat("<MediaId><![CDATA[{0}]]></MediaId>", MediaId);
            builder.AppendFormat("<MsgId>{0}</MsgId>", MsgId);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessageModel : BasicMessageModel
    {
        public string MediaId { get; set; }//语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        public string Format { get; set; }//语音格式：如amr，speex等
        public long MsgId { get; set; }//消息id，64位整型

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "voice");
            builder.AppendFormat("<MediaId><![CDATA[{0}]]></MediaId>", MediaId);
            builder.AppendFormat("<Format><![CDATA[{0}]]></Format>", Format);
            builder.AppendFormat("<MsgId>{0}</MsgId>", MsgId);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMessageModel : BasicMessageModel
    {
        public string MediaId { get; set; }//视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        public string ThumbMediaId { get; set; }//视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        public long MsgId { get; set; }//消息id，64位整型

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "video");
            builder.AppendFormat("<MediaId><![CDATA[{0}]]></MediaId>", MediaId);
            builder.AppendFormat("<ThumbMediaId><![CDATA[{0}]]></ThumbMediaId>", ThumbMediaId);
            builder.AppendFormat("<MsgId>{0}</MsgId>", MsgId);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 小视频消息
    /// </summary>
    public class ShortVideoMessageModel : BasicMessageModel
    {
        public string MediaId { get; set; }//视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        public string ThumbMediaId { get; set; }//视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        public long MsgId { get; set; }//消息id，64位整型

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "shortvideo");
            builder.AppendFormat("<MediaId><![CDATA[{0}]]></MediaId>", MediaId);
            builder.AppendFormat("<ThumbMediaId><![CDATA[{0}]]></ThumbMediaId>", ThumbMediaId);
            builder.AppendFormat("<MsgId>{0}</MsgId>", MsgId);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class LocationMessageModel : BasicMessageModel
    {
        public decimal Location_X { get; set; }//地理位置维度
        public decimal Location_Y { get; set; }//地理位置经度
        public int Scale { get; set; }//地图缩放大小
        public string Label { get; set; }//地理位置信息
        public long MsgId { get; set; }//消息id，64位整型

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "location");
            builder.AppendFormat("<Location_X>{0}</Location_X>", Location_X);
            builder.AppendFormat("<Location_Y>{0}</Location_Y>", Location_Y);
            builder.AppendFormat("<Scale>{0}</Scale>", Scale);
            builder.AppendFormat("<Label><![CDATA[{0}]]></Label>", Label);
            builder.AppendFormat("<MsgId>{0}</MsgId>", MsgId);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 连接消息
    /// </summary>
    public class LinkMessageModel : BasicMessageModel
    {
        public string Title { get; set; }//消息标题
        public string Description { get; set; }//消息描述
        public string Url { get; set; }//消息链接
        public long MsgId { get; set; }//消息id，64位整型

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "link");
            builder.AppendFormat("<Title><![CDATA[{0}]]></Title>", Title);
            builder.AppendFormat("<Description><![CDATA[{0}]]></Description>", Description);
            builder.AppendFormat("<Url><![CDATA[{0}]]></Url>", Url);
            builder.AppendFormat("<MsgId>{0}</MsgId>", MsgId);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 消息基类
    /// </summary>
    public class BasicMessageModel
    {
        public string ToUserName { get; set; }//开发者微信号
        public string FromUserName { get; set; }//发送方帐号（一个OpenID）
        public long CreateTime { get; set; }//消息创建时间 （整型）
        public string MsgType { get; set; }//消息类型：text，image，voice，video，shortvideo，location，link
    }
}
