using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Wechat.WebUI.ApiControllers
{
    public class ApiBasicController : ApiController
    {
        #region Api返回结果
        protected class ApiResult
        {
            public bool Flag { get; set; }

            public object Content { get; set; }

            public string RedirectUrl { get; set; }
        }
        protected ApiResult Success(object message)
        {
            return new ApiResult { Flag = true, Content = message };
        }

        protected ApiResult Faild(string message)
        {
            return new ApiResult { Flag = false, Content = message };
        }

        protected ApiResult Result(bool flag)
        {
            if (flag)
                return Success("操作成功");
            return Faild("操作失败");
        }

        //public ApiResult Result<T>(bool flag) 
        //{

        //}


        /// <summary>
        /// model验证失败
        /// </summary>
        /// <returns></returns>
        protected ApiResult ModelValidError()
        {
            var errorInfos = ModelState.Where(x => x.Value.Errors.Count > 0);
            var errorInfo = errorInfos.SelectMany(x => x.Value.Errors).ToArray()[0].ErrorMessage;

            return new ApiResult { Flag = false, Content = errorInfo };
        }
        #endregion
    }
}
