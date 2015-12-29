using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Controllers
{
    public class JsSdkController : Controller
    {
        //
        // GET: /JsSdk/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetJsApiTicket()
        {
            return Content(Wechat.API.JsSdk.GetTicket(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret));
        }

        public string GetSign(string timestamp, string nonceStr, string url)
        {
            return Wechat.API.JsSdk.GetSign(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, nonceStr, timestamp, url);
        }

        public ActionResult TestSign()
        {
            return Content(Wechat.API.JsSdk.GetSign(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret,Wechat.API.Common.GetNonceStr(),Wechat.API.Common.GetTimeStamp(), "http://www.linkin.net"));
        }
    }
}
