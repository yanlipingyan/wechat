using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YLP.Tookit;
using YLP.Tookit.Helper;

namespace Wechat.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            YLPAuthorize.SetCookie(new YLPMember() { Id = IDHelper.Id32, Role = "ylp", Account = "yanliping", Name = "闫丽平", Enduring = false });
            return Content("设置登录cookie");
        }

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
