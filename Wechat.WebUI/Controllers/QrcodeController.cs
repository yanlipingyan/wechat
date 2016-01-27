using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Controllers
{
    public class QrcodeController : Controller
    {
        //
        // GET: /Qrcode/

        public ActionResult Index()
        {
            ViewBag.Inage1 = Wechat.WebUI.Qrcode.ShowTemporaryQrcode(Wechat.WebUI.Model.ApiModel.AppID, Wechat.WebUI.Model.ApiModel.AppSecret, 2);
            ViewBag.Inage2 = Wechat.WebUI.Qrcode.ShowPermanentQrcode(Wechat.WebUI.Model.ApiModel.AppID, Wechat.WebUI.Model.ApiModel.AppSecret, 3);
            return View();
        }
    }
}
