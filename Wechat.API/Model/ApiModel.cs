using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace Wechat.WebUI.Model
{
    public static class ApiModel
    {
        private static string appID;
        private static string appSecret;
        private static string token;
        private static string encodingAESKey;
        private static string mchID;
        private static string mchAPISecret;

        /// <summary>
        /// 开发者ID
        /// </summary>
        public static string AppID
        {
            get
            {
                if (string.IsNullOrEmpty(appID))
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["AppSecret"]))
                        return null;
                    return ConfigurationManager.AppSettings["AppID"].ToString();
                }
                return appID;
            }

            set { appID = value; }
        }

        /// <summary>
        /// 开发者应用密钥
        /// </summary>
        public static string AppSecret
        {
            get
            {
                if (string.IsNullOrEmpty(appSecret))
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["AppSecret"]))
                        return null;
                    return ConfigurationManager.AppSettings["AppSecret"].ToString();
                }
                return appSecret;
            }

            set { appSecret = value; }
        }

        /// <summary>
        /// 服务器令牌
        /// </summary>
        public static string Token
        {
            get
            {
                if (string.IsNullOrEmpty(token))
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Token"]))
                        return null;
                    return ConfigurationManager.AppSettings["Token"].ToString();
                }
                return token;
            }

            set { token = value; }
        }

        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public static string EncodingAESKey
        {
            get
            {
                if (string.IsNullOrEmpty(encodingAESKey))
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["EncodingAESKey"]))
                        return null;
                    return ConfigurationManager.AppSettings["EncodingAESKey"].ToString();
                }
                return encodingAESKey;
            }

            set { encodingAESKey = value; }
        }

        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public static string MchID
        {
            get
            {
                if (string.IsNullOrEmpty(mchID))
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["MchID"]))
                        return null;
                    return ConfigurationManager.AppSettings["MchID"].ToString();
                }
                return mchID;
            }

            set { mchID = value; }
        }


        /// <summary>
        /// 微信支付商户API密钥
        /// </summary>
        public static string MchAPISecret
        {
            get
            {
                if (string.IsNullOrEmpty(mchAPISecret))
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["MchAPISecret"]))
                        return null;
                    return ConfigurationManager.AppSettings["MchAPISecret"].ToString();
                }
                return mchAPISecret;
            }

            set { mchAPISecret = value; }
        }
    }
}
