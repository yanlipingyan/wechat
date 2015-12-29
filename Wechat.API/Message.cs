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
                case "text":
                    break;
                case "image":
                    break;
                case "voice":
                    break;
                case "video":
                    break;
                case "shortvideo":
                    break;
                case "location":
                    break;
                case "link":
                    break;
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
                case "subscribe":
                    break;
                case "unsubscribe":
                    break;
                case "SCAN":
                    break;
                case "LOCATION":
                    break;
                case "CLICK":
                    break;
                case "VIEW":
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
