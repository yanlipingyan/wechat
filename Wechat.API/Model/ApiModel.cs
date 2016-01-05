using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace Wechat.API.Model
{
    public static class ApiModel
    {
        private static string appID;
        private static string appSecret;
        private static string token;
        private static string encodingAESKey;
        private static string mchID;
        private static string mchAPISecret;


        static ApiModel()
        {
            string file = string.Format(@"{0}Config\WechatSetting.config", AppDomain.CurrentDomain.BaseDirectory).Replace("\\", @"\");
            if (File.Exists(file))
            {
                var result = Helper.XMLSerializerHelper.DeSerialize<ApiConfigModel>(file);

                if (result != null)
                {
                    appID = string.IsNullOrEmpty(result.AppID) ? "" : result.AppID;
                    appSecret = string.IsNullOrEmpty(result.AppSecret) ? "" : result.AppSecret;
                    token = string.IsNullOrEmpty(result.Token) ? "" : result.Token;
                    encodingAESKey = string.IsNullOrEmpty(result.EncodingAESKey) ? "" : result.EncodingAESKey;
                    mchID = string.IsNullOrEmpty(result.MchID) ? "" : result.MchID;
                    mchAPISecret = string.IsNullOrEmpty(result.MchAPISecret) ? "" : result.MchAPISecret;
                }
            }
        }

        /// <summary>
        /// 开发者ID
        /// </summary>
        public static string AppID
        {
            get
            {
                if (string.IsNullOrEmpty(appID))
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["AppID"]))
                        throw new WechatException("没有对AppID进行配置");
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
                        throw new WechatException("没有对AppSecret进行配置");
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
                        throw new WechatException("没有对Token进行配置");
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
                        throw new WechatException("没有对EncodingAESKey进行配置");
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
                        throw new WechatException("没有对MchID进行配置");
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
                        throw new WechatException("没有对MchAPISecret进行配置");
                    return ConfigurationManager.AppSettings["MchAPISecret"].ToString();
                }
                return mchAPISecret;
            }
            set { mchAPISecret = value; }
        }
    }
}
