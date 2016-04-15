using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YLP.Tookit.Component;
using YLP.Tookit.Helper;

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

            #region 上传到七牛-可用
            byte[] temp = new byte[4];
            System.Web.HttpContext.Current.Request.InputStream.Read(temp, 0, temp.Length);

            var fh = BitConverter.ToUInt32(temp, 0);

            byte[] ms_b = new byte[fh];
            System.Web.HttpContext.Current.Request.InputStream.Read(ms_b, 0, ms_b.Length);

            string key = QiNiu.Upload(ConfigurationManager.AppSettings["PUBLIC_BUCKET"], ms_b, "jpg");

            if (string.IsNullOrEmpty(key))
                throw new Exception("上传失败");

            header = QiNiu.GetDownloadUrl(ConfigurationManager.AppSettings["DN_HOST"], ConfigurationManager.AppSettings["PUBLIC_BUCKET"], key);
            #endregion

            #region 上传到本地-可用
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
