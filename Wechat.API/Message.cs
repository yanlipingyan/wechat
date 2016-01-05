using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Wechat.API
{
    public static class Message
    {
        public static void MessageHelper(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string data = reader.ReadToEnd();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(data);

            string toUserName = xmlDoc.SelectSingleNode("xml/ToUserName").InnerText;//开发者微信号
            string fromUserName = xmlDoc.SelectSingleNode("xml/FromUserName").InnerText;//发送方帐号（一个OpenID）
            string msgType = xmlDoc.SelectSingleNode("xml/MsgType").InnerText;//消息类型：普通消息类型：text，image，voice，video，shortvideo，location，link；事件消息类型：event

            switch (msgType)
            {
                //普通消息
                case "text"://文本消息
                    break;
                case "image"://图片消息
                    break;
                case "voice"://语音消息
                    break;
                case "video"://视频消息
                    break;
                case "shortvideo"://小视频消息
                    break;
                case "location"://地理位置消息
                    break;
                case "link"://链接消息
                    break;

                //事件推送
                case "event":
                    string eventType = xmlDoc.SelectSingleNode("xml/Event").InnerText;////事件类型，subscribe(订阅)，unsubscribe(取消订阅)，SCAN(浏览)，LOCATION(上报地址位置)，CLICK(自定义菜单)，VIEW(点击菜单跳转链接)
                    EventMessage(eventType);
                    break;
            }
        }

        public static void EventMessage(string eventType)
        {
            switch (eventType)
            {
                case "subscribe"://关注事件
                    break;
                case "unsubscribe"://取消关注事件
                    break;
                case "SCAN"://用户已关注时的事件推送
                    break;
                case "LOCATION"://上报地理位置事件
                    break;


                case "CLICK"://点击菜单拉取消息时的事件推送
                    break;
                case "VIEW"://点击菜单跳转链接时的事件推送
                    break;
                case "scancode_push"://扫码推事件的事件推送
                    break;
                case "scancode_waitmsg"://扫码推事件且弹出“消息接收中”提示框的事件推送
                    break;
                case "pic_sysphoto"://弹出系统拍照发图的事件推送
                    break;
                case "pic_photo_or_album"://弹出拍照或者相册发图的事件推送
                    break;
                case "pic_weixin"://弹出微信相册发图器的事件推送
                    break;
                case "location_select"://弹出地理位置选择器的事件推送
                    break;


                case "card_pass_check"://卡券通过审核
                    break;
                case "card_not_pass_check"://卡券未通过审核
                    break;
                case "user_get_card"://用户领取卡券
                    break;
                case "user_del_card"://用户删除卡券
                    break;
                case "user_consume_card"://核销事件
                    break;
                case "User_pay_from_pay_cell"://微信买单事件
                    break;
            }
        }
    }
}
