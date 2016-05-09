using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Wechat.WebUI
{
    public class CrossSiteAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        //{
        //    if (actionExecutedContext.Response != null)
        //    {
        //        System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        //        System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
        //        System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET,POST,PUT,OPTIONS,DELETE,PATCH");
        //    }
        //    base.OnActionExecuted(actionExecutedContext);
        //}

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //if (actionContext.Response != null)
            //{
                System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
                System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
                System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET,POST,PUT,OPTIONS,DELETE,PATCH");
            //}
            //base.OnActionExecuting(actionContext);
        }
    }
}