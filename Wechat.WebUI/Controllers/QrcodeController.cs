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
            ViewBag.Inage1 = Wechat.API.Qrcode.ShowTemporaryQrcode(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, 2);
            ViewBag.Inage2 = Wechat.API.Qrcode.ShowPermanentQrcode(Wechat.API.Model.ApiModel.AppID, Wechat.API.Model.ApiModel.AppSecret, 3);
            return View();
        }
    }
}
