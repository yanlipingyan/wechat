using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wechat.API.Models;
using YLP.Tookit.Helper;


namespace Wechat.Service
{
    public static class ConfigService
    {
        public static void InitConfig()
        {
            Models.ConfigModel model = null;

            var file = string.Format("{0}/Config/WechatSetting.config", AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists(file))
                model = XMLSerializerHelper.DeSerialize<Models.ConfigModel>(file);

            if (model != null)
            {
                ApiModel.AppID = model.AppID;
                ApiModel.AppSecret = model.AppSecret;
                ApiModel.Token = model.Token;
                ApiModel.EncodingAESKey = model.EncodingAESKey;
                ApiModel.MchID = model.MchID;
                ApiModel.MchAPISecret = model.MchAPISecret;
            }
        }
    }
}
