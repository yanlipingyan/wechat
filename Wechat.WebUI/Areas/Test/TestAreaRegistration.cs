﻿using System.Web.Mvc;

namespace Wechat.WebUI.Areas.Test
{
    public class TestAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Test";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Test_default",
                "Test/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Wechat.WebUI.Areas.Test.Controllers" }
            );
        }
    }
}
