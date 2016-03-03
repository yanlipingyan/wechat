using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class UEditorController : Controller
    {
        //
        // GET: /Test/UEditor/

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Save(string content)
        {
            return Content(content);
        }

    }
}
