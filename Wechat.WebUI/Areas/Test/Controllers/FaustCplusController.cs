using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YLP.Tookit.Component;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class FaustCplusController : Controller
    {
        //
        // GET: /Test/FaustCplus/

        public ActionResult Index(string img)
        {
            return View(img);
        }

        [HttpPost]
        public string Upload(string memberId = "")
        {
            string header = "";

            #region 上传到七牛
            byte[] temp = new byte[4];
            System.Web.HttpContext.Current.Request.InputStream.Read(temp, 0, temp.Length);

            var fh = BitConverter.ToUInt32(temp, 0);

            byte[] ms_b = new byte[fh];
            System.Web.HttpContext.Current.Request.InputStream.Read(ms_b, 0, ms_b.Length);

            string key = QiNiu.Upload("wechat", ms_b, "jpg");
            if (string.IsNullOrEmpty(key))
                throw new Exception("上传失败");

            header = QiNiu.GetDownloadUrl("www.liblog.cn", "wechat", key);
            #endregion

            #region 上传到本地
            //var strogeFolder = string.Format("/Upload/Header/{0}/", DateTime.Now.ToString("yyyy/MM/dd"));

            //var networkPath = strogeFolder;
            //var physicalPath = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, strogeFolder);

            //var fileName = string.Format("{0}.{1}", Guid.NewGuid().ToString("N"), "jpg");

            //header = YLP.Tookit.Component.FaustCplus.Upload(networkPath, physicalPath, fileName);
            #endregion

            if (!string.IsNullOrEmpty(header))
            {
                if (!string.IsNullOrEmpty(memberId))
                {
                    //TODO:保存到数据库
                }

                return header;
            }

            return "上传失败";

        }
    }
}
