using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;
using Wechat.API.Models;
using Wechat.WebUI.Filters;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class OAuth2Controller : Controller
    {
        //
        // GET: /Test/OAuth2/

        [WechatRoute]
        public ActionResult Index(string code = "")
        {
            if (string.IsNullOrEmpty(code))
                return Content("code为空");

            var getToken = OAuth2.GetToken(ApiModel.AppID, ApiModel.AppSecret, code);
            var tokenIsValid = OAuth2.GetOAuthTokenIsValid(JsonConvert.DeserializeObject<dynamic>(getToken)["openid"].ToString(), JsonConvert.DeserializeObject<dynamic>(getToken)["access_token"].ToString());
            var refreshToken = OAuth2.RefreshToken(ApiModel.AppID, JsonConvert.DeserializeObject<dynamic>(getToken)["refresh_token"].ToString());
            var getUserInfo = OAuth2.GetUserInfo(JsonConvert.DeserializeObject<dynamic>(getToken)["openid"].ToString(), JsonConvert.DeserializeObject<dynamic>(getToken)["access_token"].ToString(), "zh_CN");

            return Content("GetToken：" + getToken + "；</br>" + "GetOAuthTokenIsValid:" + tokenIsValid + "；</br>" + "RefreshToken:" + refreshToken + "；</br>" + "getUserInfo:" + getUserInfo + "；</br>");
        }

    }
}
