using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;

namespace Wechat.WebUI.Controllers
{
    public class WechatController : Controller
    {
        //
        // GET: /Wechat/

        /// <summary>
        /// 微信公众号后台服务器地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string id)
        {
            ReceiveMessage.MessageHelper(Request.InputStream);
            return Content("1");
        }

    }
}
