using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* ==================================================================
 *  ​注：
 * 在微信用户和公众号产生交互的过程中，用户的某些操作会使得微信服务器通过事件推送的形式通知到开发者在开发者中心处设置的服务器地址（即开发者中心页面的服务器配置处的Url路径事件），从而开发者可以获取到该信息。
 * 其中，某些事件推送在发生后，是允许开发者回复用户的，某些则不允许，详细说明请见本页末尾的微信推送消息与事件说明。
* ==================================================================*/
namespace Wechat.API.Model
{
    /// <summary>
    /// 关注取消关注事件：Event = subscribe or unsubscribe
    /// </summary>
    /// 可以回复消息：方便开发者给用户下发欢迎消息或者做帐号的解绑。
    public class SubOrUnsubEventModel : EventModel
    {
        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "event");
            builder.AppendFormat("<Event><![CDATA[{0}]]></Event>", Event);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 扫描带参数二维码事件：Event = subscribe or SCAN
    /// </summary>
    /// 用户扫描带场景值二维码时，可能推送以下两种事件：
    /// 如果用户还未关注公众号，则用户可以关注公众号，关注后微信会将带场景值关注事件推送给开发者。
    /// 如果用户已经关注公众号，则微信会将带场景值扫描事件推送给开发者。
    public class ScanQrcodeWithParamEventModel : EventModel
    {
        public string EventKey { get; set; }//事件KEY值，Event="subscribe"时，KEY为qrscene_为前缀，后面为二维码的参数值；Event="SCAN"时是一个32位无符号整数，即创建二维码时的二维码scene_id
        public string Ticket { get; set; }//二维码的ticket，可用来换取二维码图片

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "event");
            builder.AppendFormat("<Event><![CDATA[{0}]]></Event>", Event);
            builder.AppendFormat("<EventKey><![CDATA[{0}]]></EventKey>", EventKey);
            builder.AppendFormat("<Ticket><![CDATA[{0}]]></Ticket>", Ticket);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 上报地理位置事件：Event = LOCATION
    /// </summary>
    /// 用户同意上报地理位置后，每次进入公众号会话时，都会在进入时上报地理位置，或在进入会话后每5秒上报一次地理位置，公众号可以在公众平台网站中修改以上设置。上报地理位置时，微信会将上报地理位置事件推送到开发者填写的URL。
    public class ReportLocationEventModel : EventModel
    {
        public decimal Latitude { get; set; }//地理位置纬度
        public decimal Longitude { get; set; }//地理位置经度
        public decimal Precision { get; set; }//地理位置精度

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "event");
            builder.AppendFormat("<Event><![CDATA[{0}]]></Event>", Event);
            builder.AppendFormat("<Latitude>{0}</Latitude>", Latitude);
            builder.AppendFormat("<Longitude>{0}</Longitude>", Longitude);
            builder.AppendFormat("<Precision>{0}</Precision>", Precision);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 自定义菜单事件：Event = CLICK
    /// </summary>
    /// 用户点击自定义菜单后，微信会把点击事件推送给开发者，请注意，点击菜单弹出子菜单，不会产生上报。
    public class CustomMenuEventModel : EventModel
    {
        public string EventKey { get; set; }//事件KEY值，与自定义菜单接口中KEY值对应

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "event");
            builder.AppendFormat("<Event><![CDATA[{0}]]></Event>", Event);
            builder.AppendFormat("<EventKey><![CDATA[{0}]]></EventKey>", EventKey);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 点击菜单跳转连接事件：Event = VIEW
    /// </summary>
    public class ClickMenuJumpLinkEventModel : EventModel
    {
        public string EventKey { get; set; }//事件KEY值，设置的跳转URL

        public string ToXml()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            builder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            builder.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            builder.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", "event");
            builder.AppendFormat("<Event><![CDATA[{0}]]></Event>", Event);
            builder.AppendFormat("<EventKey><![CDATA[{0}]]></EventKey>", EventKey);
            builder.Append("</xml>");
            return builder.ToString();
        }
    }

    /// <summary>
    /// 基类
    /// </summary>
    public class EventModel
    {
        public string ToUserName { get; set; }//开发者微信号
        public string FromUserName { get; set; }//发送方帐号（一个OpenID）
        public long CreateTime { get; set; }//消息创建时间 （整型）
        public string MsgType { get; set; }//消息类型：event
        public string Event { get; set; }//事件类型，subscribe(订阅)、unsubscribe(取消订阅),SCAN(浏览),LOCATION(上报地址位置),CLICK(自定义菜单),VIEW(点击菜单跳转链接)
    }
}
