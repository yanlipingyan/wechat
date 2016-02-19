using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;
using Wechat.API.Models;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class JsSdkController : Controller
    {
        //
        // GET: /Test/JsSdk/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetJsApiTicket()
        {
            return Content(Wechat.API.JsSdk.GetTicket(ApiModel.AppID, ApiModel.AppSecret));
        }

        public string GetSign(string timestamp, string nonceStr, string url)
        {
            return Wechat.API.JsSdk.GetSign(ApiModel.AppID, ApiModel.AppSecret, nonceStr, timestamp, url);
        }

        public ActionResult TestSign()
        {
            return Content(Wechat.API.JsSdk.GetSign(ApiModel.AppID, ApiModel.AppSecret, Common.GetNonceStr(), Common.GetTimeStamp(), "http://www.liblog.cn"));
        }
    }
}
