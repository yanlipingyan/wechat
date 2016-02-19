using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;
using Wechat.API.Models;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Test/Menu/

        public ActionResult Index()
        {
            return Content(Menu.QueryAllMenu(ApiModel.AppID, ApiModel.AppSecret));
        }

    }
}
