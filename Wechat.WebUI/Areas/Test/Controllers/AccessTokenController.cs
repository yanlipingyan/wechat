using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;
using Wechat.API.Models;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class AccessTokenController : Controller
    {
        //
        // GET: /Test/AccessToken/

        public ActionResult Index()
        {
            return Content(AccessToken.GetToken("wx6e534c2ba592cce4", "04093ec4f3c22beebcc74dbefe123022"));
        }

    }
}
