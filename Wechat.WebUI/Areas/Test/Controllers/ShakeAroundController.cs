using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;
using Wechat.API.Enums;
using Wechat.API.Models;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class ShakeAroundController : Controller
    {
        //
        // GET: /Test/ShakeAround/

        public ActionResult Index()
        {
            var result = ShakeAround.Apply(ApiModel.AppID, ApiModel.AppSecret, new ShakeAroundApplyModel() { Name = "闫丽平", PhoneNumber = "13937110383", Email = "759001549@qq.com", IndustryId = WechatIndustryEnum.其他组织, QualificationCertUrls = new string[] { }, ApplyReason = "" });
            return Content(result);
        }

        public ActionResult GetAuditStatus()
        {
            var result = ShakeAround.GetAuditStatus(ApiModel.AppID, ApiModel.AppSecret);

            return Content(result);
        }

    }
}
