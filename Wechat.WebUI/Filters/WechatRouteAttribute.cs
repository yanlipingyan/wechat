using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;
using Wechat.API.Models;

namespace Wechat.WebUI.Filters
{
    public class WechatRouteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string path = HttpContext.Current.Request.Url.ToString();

            if (Common.IsFromWechatBrowser() && !path.Contains("open.weixin.qq.com") && (HttpContext.Current.Request.QueryString["code"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["code"].ToString())))
            {
                filterContext.Result = new RedirectResult(OAuth2.GetCode(ApiModel.AppID, path, Wechat.API.Enums.OAuthScopeEnum.snsapi_userinfo, "STATE"));
            }
        }
    }
}