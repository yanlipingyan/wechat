using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class ServerController : Controller
    {
        //
        // GET: /Test/Server/

        public ActionResult Index()
        {
            return Content("GetIP:" + WechatServer.GetIP("wx6e534c2ba592cce4", "04093ec4f3c22beebcc74dbefe123022").FirstOrDefault() + "；<br/>" + "GetVersion:" + WechatServer.GetVersion());
        }

    }
}
