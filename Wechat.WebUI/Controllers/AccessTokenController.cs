using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Controllers
{
    public class AccessTokenController : Controller
    {
        //
        // GET: /AccessToken/

        public ActionResult Index()
        {
            return Content(new Wechat.API.AccessToken.GetToken());
        }

    }
}
