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
            return Content(Wechat.WebUI.JsSdk.GetTicket(Wechat.WebUI.Model.ApiModel.AppID, Wechat.WebUI.Model.ApiModel.AppSecret));
        }

        public string GetSign(string timestamp, string nonceStr, string url)
        {
            return Wechat.WebUI.JsSdk.GetSign(Wechat.WebUI.Model.ApiModel.AppID, Wechat.WebUI.Model.ApiModel.AppSecret, nonceStr, timestamp, url);
        }

        public ActionResult TestSign()
        {
            return Content(Wechat.WebUI.JsSdk.GetSign(Wechat.WebUI.Model.ApiModel.AppID, Wechat.WebUI.Model.ApiModel.AppSecret,Wechat.WebUI.Common.GetNonceStr(),Wechat.WebUI.Common.GetTimeStamp(), "http://www.linkin.net"));
        }
    }
}
