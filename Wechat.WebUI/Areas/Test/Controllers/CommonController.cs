using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Test/Common/

        public ActionResult Index()
        {
            return Content("是否来自微信：" + Common.IsFromWechatBrowser().ToString() + "；" + "生成时间戳：" + Common.GetTimeStamp() + "；");
        }

    }
}
