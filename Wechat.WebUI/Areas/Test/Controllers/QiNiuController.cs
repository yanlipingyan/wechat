using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YLP.Tookit.Component;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class QiNiuController : Controller
    {
        //
        // GET: /Test/QiNiu/

        public void Index()
        {
            new QiNiu().Upload("teamopf-test", System.AppDomain.CurrentDomain.BaseDirectory + "/Content/images/logo.png");
        }

    }
}
