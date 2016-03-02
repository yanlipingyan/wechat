using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wechat.WebUI.HttpHandler
{
    /// <summary>
    /// UploadImage 的摘要说明
    /// </summary>
    public class UploadImage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //注释：利用一般处理程序进行上传的话是没有办法进行保存到数据库操作的

            var strogeFolder = string.Format("/Upload/Header/{0}/", DateTime.Now.ToString("yyyy/MM/dd"));

            var networkPath = strogeFolder;
            var physicalPath = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, strogeFolder);

            var fileName = string.Format("{0}.{1}", Guid.NewGuid().ToString("N"), "jpg");

            YLP.Tookit.Helper.FaustCplusUploadHelper.Upload(networkPath, physicalPath, fileName);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}