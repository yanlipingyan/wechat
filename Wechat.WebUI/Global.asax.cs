using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Wechat.Service;
using YLP.Tookit.Helper;

namespace Wechat.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Log4NetHelper.LoadConfig(Server.MapPath("~/Config/log4net.config"));

            ConfigService.InitConfig();
        }

        /// <summary>
        /// 拦截用户权限（实现角色功能）
        /// </summary>
        protected void Application_AuthenticateRequest()
        {
            if (Context.User != null)
            {
                System.Security.Principal.IIdentity id = Context.User.Identity;

                if (id != null && id.IsAuthenticated)
                    Context.User = new System.Security.Principal.GenericPrincipal(id, new string[] { YLPAuthorize.Role });
            }
        }
    }
}