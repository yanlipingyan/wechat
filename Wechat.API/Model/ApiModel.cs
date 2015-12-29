using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Wechat.API.Model
{
    public static class ApiModel
    {
        /// <summary>
        /// 开发者ID
        /// </summary>
        private static string appID;

        public static string AppID
        {
            get { return ConfigurationManager.AppSettings["AppID"].ToString(); }
            set { appID = value; }
        }

        /// <summary>
        /// 开发者应用密钥
        /// </summary>
        private static string appSecret;

        public static string AppSecret
        {
            get { return ConfigurationManager.AppSettings["AppSecret"].ToString(); }
            set { appSecret = value; }
        }

        /// <summary>
        /// 服务器令牌
        /// </summary>
        private static string token;

        public static string Token
        {
            get { return ConfigurationManager.AppSettings["Token"].ToString(); }
            set { token = value; }
        }

        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        private static string encodingAESKey;

        public static string EncodingAESKey
        {
            get { return ConfigurationManager.AppSettings["EncodingAESKey"].ToString(); }
            set { encodingAESKey = value; }
        }

        /// <summary>
        /// 微信支付商户号
        /// </summary>
        private static string mchID;

        public static string MchID
        {
            get { return ConfigurationManager.AppSettings["MchID"].ToString(); }
            set { mchID = value; }
        }


        /// <summary>
        /// 微信支付商户API密钥
        /// </summary>
        private static string mchAPISecret;

        public static string MchAPISecret
        {
            get { return ConfigurationManager.AppSettings["MchAPISecret"].ToString(); }
            set { mchAPISecret = value; }
        }
    }
}
