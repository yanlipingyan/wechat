using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class YLPAutorizeController : Controller
    {
        //
        // GET: /Test/YLPAutorize/

        [Authorize(Roles = "ylp")]
        public ActionResult Test()
        {
            return Content("这是Test");
        }

        [Authorize(Roles = "ce")]
        public ActionResult Test1()
        {
            return Content("这是Test1");
        }
    }
}
