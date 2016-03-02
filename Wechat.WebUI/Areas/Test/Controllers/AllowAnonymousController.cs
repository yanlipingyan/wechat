using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    [Authorize(Roles = "ylp")]
    public class AllowAnonymousController : Controller
    {
        //
        // GET: /Test/AllowAnonymous/

        /*
         * 匿名属性启用的前提是网站启用了form身份验证
         */

        [AllowAnonymous]
        public ActionResult Test()
        {
            return Content("允许异步");
        }
        public ActionResult Test1()
        {
            return Content("不允许异步");
        }
    }
}
