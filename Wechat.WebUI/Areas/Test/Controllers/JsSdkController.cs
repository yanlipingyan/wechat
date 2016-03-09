using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;
using Wechat.API.Models;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class JsSdkController : Controller
    {
        //
        // GET: /Test/JsSdk/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取JsSdk签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonceStr"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetSign(string timestamp, string nonceStr, string url)
        {
            return JsSdk.GetSign(ApiModel.AppID, ApiModel.AppSecret, nonceStr, timestamp, url);
        }
    }
}
