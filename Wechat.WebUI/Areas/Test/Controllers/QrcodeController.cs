using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API.Models;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class QrcodeController : Controller
    {
        //
        // GET: /Test/Qrcode/

        public ActionResult Index()
        {
            ViewBag.Image1 = Wechat.API.Qrcode.ShowTemporaryQrcode(ApiModel.AppID, ApiModel.AppSecret, 2);
            ViewBag.Image2 = Wechat.API.Qrcode.ShowPermanentQrcode(ApiModel.AppID, ApiModel.AppSecret, 3);
            return View();
        }

    }
}
