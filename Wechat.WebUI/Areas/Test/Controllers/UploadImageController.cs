using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class UploadImageController : Controller
    {
        //
        // GET: /Test/UploadImage/

        public ActionResult Index(string img)
        {
            return View(img);
        }

        [HttpPost]
        public ActionResult Upload(string memberId = "")
        {
            #region 上传到本地
            var strogeFolder = string.Format("/Upload/Header/{0}/", DateTime.Now.ToString("yyyy/MM/dd"));

            var networkPath = strogeFolder;
            var physicalPath = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, strogeFolder);

            var fileName = string.Format("{0}.{1}", Guid.NewGuid().ToString("N"), "jpg");

            string header = YLP.Tookit.Helper.FaustCplusUploadHelper.Upload(networkPath, physicalPath, fileName);

            if (!string.IsNullOrEmpty(header))
            {
                if (!string.IsNullOrEmpty(memberId))
                {
                    //TODO:保存到数据库
                }

                return Content("上传成功");
            }

            return Content("上传失败");
            #endregion
        }
    }
}
