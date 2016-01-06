using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Wechat.API.Model
{
    [Serializable]
    public class ApiConfigModel
    {
        [XmlElement]
        public string AppID { get; set; }

        [XmlElement]
        public string AppSecret { get; set; }

        [XmlElement]
        public string Token { get; set; }

        [XmlElement]
        public string EncodingAESKey { get; set; }

        [XmlElement]
        public string MchID { get; set; }

        [XmlElement]
        public string MchAPISecret { get; set; }

        public ApiConfigModel()
        {
            this.AppID =ApiModel.AppID;
            this.AppSecret = ApiModel.AppSecret;
            this.Token = ApiModel.Token;
            this.EncodingAESKey = ApiModel.EncodingAESKey;
            this.MchID = ApiModel.MchID;
            this.MchAPISecret = ApiModel.MchAPISecret;
        }
    }
}
