using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Wechat.API.Model
{
    [Serializable]
    public static class ApiModel
    {
        private static string appID;
        private static string appSecret;
        private static string token;
        private static string encodingAESKey;
        private static string mchID;
        private static string mchAPISecret;


        public ApiModel()
        {
            var result = Helper.XMLSerializerHelper.DeSerialize<dynamic>("~/Config/WechatSetting.config");

            if (!string.IsNullOrEmpty(result))
            {
                appID = string.IsNullOrEmpty(result["AppID"]) ? "" : result["AppID"];
                appSecret = string.IsNullOrEmpty(result["AppSecret"]) ? "" : result["AppSecret"];
                token = string.IsNullOrEmpty(result["Token"]) ? "" : result["Token"];
                encodingAESKey = string.IsNullOrEmpty(result["EncodingAESKey"]) ? "" : result["EncodingAESKey"];
                mchID = string.IsNullOrEmpty(result["MchID"]) ? "" : result["MchID"];
                mchAPISecret = string.IsNullOrEmpty(result["MchAPISecret"]) ? "" : result["MchAPISecret"];
            }
        }

        /// <summary>
        /// 开发者ID
        /// </summary>
        [XmlElement]
        public static string AppID
        {
            get
            {
                if (string.IsNullOrEmpty(appID))
                    return ConfigurationManager.AppSettings["AppID"].ToString();
                return appID;
            }
            set { appID = value; }
        }

        /// <summary>
        /// 开发者应用密钥
        /// </summary>
        [XmlElement]
        public static string AppSecret
        {
            get
            {
                if (string.IsNullOrEmpty(appSecret))
                    return ConfigurationManager.AppSettings["AppSecret"].ToString();
                return appSecret;
            }
            set { appSecret = value; }
        }

        /// <summary>
        /// 服务器令牌
        /// </summary>
        [XmlElement]
        public static string Token
        {
            get
            {
                if (string.IsNullOrEmpty(token))
                    return ConfigurationManager.AppSettings["Token"].ToString();
                return token;
            }
            set { token = value; }
        }

        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        [XmlElement]
        public static string EncodingAESKey
        {
            get
            {
                if (string.IsNullOrEmpty(encodingAESKey))
                    return ConfigurationManager.AppSettings["EncodingAESKey"].ToString();
                return encodingAESKey;
            }
            set { encodingAESKey = value; }
        }

        /// <summary>
        /// 微信支付商户号
        /// </summary>
        [XmlElement]
        public static string MchID
        {
            get
            {
                if (string.IsNullOrEmpty(mchID))
                    return ConfigurationManager.AppSettings["MchID"].ToString();
                return mchID;
            }
            set { mchID = value; }
        }


        /// <summary>
        /// 微信支付商户API密钥
        /// </summary>
        [XmlElement]
        public static string MchAPISecret
        {
            get
            {
                if (string.IsNullOrEmpty(mchAPISecret))
                    return ConfigurationManager.AppSettings["MchAPISecret"].ToString();
                return mchAPISecret;
            }
            set { mchAPISecret = value; }
        }
    }
}
