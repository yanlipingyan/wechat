using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Wechat.WebUI.ApiControllers
{
    public class TestController : ApiBasicController
    {
        [HttpGet]
        public object Get()
        {
            return Success("This id Get Method");
        }

        [HttpPost]
        public object Post([FromBody]object content)
        {
            return Success(content);
        }

        [HttpPut]
        public object Put(string id, [FromBody]object content)
        {
            return Success(content);
        }

        [HttpDelete]
        public object Delete(string id)
        {
            return Success("This is Post Method");
        }
    }
}
