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
        /// 此Get请求验证服务器地址的有效性
        /// 
        /// 注释：
        /// GET请求携带四个参数：
        /// 参数	        描述
        /// signature	    微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
        /// timestamp	    时间戳
        /// nonce	        随机数
        /// echostr	        随机字符串
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public string Index(string signature, string timestamp, string nonce, string echostr)
        {
            if (Signature.Check(signature, timestamp, nonce, "liblog"))
                return echostr;

            return "接入失败";
        }

        /// <summary>
        /// 填写服务器配置中的Url
        /// 
        /// 注释：
        /// 1、但是在提交服务器配置后，微信服务器将发送GET请求到填写的服务器地址URL（即上面的同名Get请求方法）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index()
        {
            ReceiveMessage.MessageHelper(Request.InputStream);
            return Content("1");
        }

    }
}
