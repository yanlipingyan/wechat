using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class AjaxSumbitController : Controller
    {
        //
        // GET: /Test/AjaxSumbit/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(string content)
        {
            return Json(new { Flag = true, Content = "添加成功" }, JsonRequestBehavior.AllowGet);
        }
    }
}
