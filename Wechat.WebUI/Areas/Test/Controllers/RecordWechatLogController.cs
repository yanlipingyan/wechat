using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YLP.Tookit.Helper;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class RecordWechatLogController : Controller
    {
        //
        // GET: /Test/RecordWechatLog/

        public ActionResult Index()
        {
            Wechat.API.WechatLog.Error("RecordWechatLog", "测试记录微信Error日志");
            Wechat.API.WechatLog.Info("RecordWechatLog", "测试记录微信Info日志");
            Wechat.API.WechatLog.Debug("RecordWechatLog", "测试记录微信Debug日志");

            Log4NetHelper.Error("Log4Net错误信息");

            return Content("请到相应的日志文件中查看");
        }

    }
}
