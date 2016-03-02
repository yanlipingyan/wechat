using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;//这是WebApi过滤器的命名空间
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Wechat.WebUI.ApiFilters
{
    public class Test : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Redirect);
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}