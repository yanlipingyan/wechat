using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wechat.API;
using Wechat.API.Models;
using YLP.Tookit.Helper;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class MaterialController : Controller
    {
        //
        // GET: /Test/Material/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Download(string media_ids)
        {
            string netPaths = "";

            foreach (var media_id in media_ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    string folder = string.Format("upload/material/{0}", DateTime.Now.ToString("yyyyMMdd"));
                    string filename = string.Format("{0}.jpg", IDHelper.Id32);
                    string physicalPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + folder;
                    string netPath = "/" + folder + "/" + filename;

                    //如果日志目录不存在就创建
                    if (!Directory.Exists(physicalPath))
                    {
                        Directory.CreateDirectory(physicalPath);
                    }

                    if (string.IsNullOrEmpty(media_id))
                        return Content("media_id为空");

                    Material.GetTemporaryMedia(ApiModel.AppID, ApiModel.AppSecret, media_id, ms);

                    //保存到文件
                    var fileName = physicalPath + "/" + filename;

                    using (FileStream fs = new FileStream(fileName, FileMode.Create))
                    {
                        ms.Position = 0;
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            fs.Write(buffer, 0, bytesRead);
                        }
                        fs.Flush();
                    }
                    netPaths += netPath + ",";
                }
            }

            return Content(new JavaScriptSerializer().Serialize(new { Flag = true, Content = netPaths }));
        }

    }
}
