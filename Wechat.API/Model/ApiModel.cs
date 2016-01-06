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
        private static string appID = Get() ? Get().AppID : null;
        private static string appSecret = Get() ? Get().AppSecret : null;
        private static string token = Get() ? Get().Token : null;
        private static string encodingAESKey = Get() ? Get().EncodingAESKey : null;
        private static string mchID = Get() ? Get().MchID : null;
        private static string mchAPISecret = Get() ? Get().MchAPISecret : null;


        private static dynamic Get()
        {
            string file = string.Format(@"{0}Config\WechatSetting.config", AppDomain.CurrentDomain.BaseDirectory).Replace("\\", @"\");
            if (File.Exists(file))
            {
                return Helper.XMLSerializerHelper.DeSerialize<dynamic>(file);

                //if (result != null)
                //{
                //    appID = string.IsNullOrEmpty(result.AppID) ? "" : result.AppID;
                //    appSecret = string.IsNullOrEmpty(result.AppSecret) ? "" : result.AppSecret;
                //    token = string.IsNullOrEmpty(result.Token) ? "" : result.Token;
                //    encodingAESKey = string.IsNullOrEmpty(result.EncodingAESKey) ? "" : result.EncodingAESKey;
                //    mchID = string.IsNullOrEmpty(result.MchID) ? "" : result.MchID;
                //    mchAPISecret = string.IsNullOrEmpty(result.MchAPISecret) ? "" : result.MchAPISecret;
                //}
            }
            return null;
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
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["AppSecret"]))
                        throw new WechatException("没有对AppID进行配置");
                    return ConfigurationManager.AppSettings["AppID"].ToString();
                }
                return appID;
            }
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
        }
    }
}
