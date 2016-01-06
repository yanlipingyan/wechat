using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Controllers
{
    public class ConfigController : Controller
    {
        //
        // GET: /Config/

        public ActionResult Index()
        {
            return View(new Wechat.API.Model.ApiConfigModel());
        }

        public ActionResult Submit()
        {
            return Content("ddd");
        }
    }
}
