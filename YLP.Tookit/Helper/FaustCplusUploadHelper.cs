using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace YLP.Tookit.Helper
{
    /// <summary>
    /// FaustCplus插件上传图片
    /// </summary>
    public class FaustCplusUploadHelper
    {
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="networkPath">网络路径</param>
        /// <param name="physicalPath">物理路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns>网络文件路径</returns>
        public static string Upload(string networkPath, string physicalPath, string fileName)
        {
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            try
            {
                byte[] temp = new byte[4];
                HttpContext.Current.Request.InputStream.Read(temp, 0, temp.Length);

                int fl = BitConverter.ToInt32(temp, 0);

                byte[] fb = new byte[fl];
                HttpContext.Current.Request.InputStream.Read(fb, 0, fb.Length);

                var image = ConvertBytesToImage(fb);
                image.Save(string.Format("{0}{1}", physicalPath, fileName));

                return string.Format("{0}{1}", networkPath, fileName);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将一个byte转换成一个Bitmap对象
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <returns>Image对象</returns>
        private static Image ConvertBytesToImage(Byte[] buffer)
        {
            if (buffer.Length <= 0)
                return null;

            System.IO.MemoryStream ms = null;
            try
            {
                ms = new System.IO.MemoryStream(buffer);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
            catch
            {
                if (ms != null)
                    ms.Close();

                return null;
            }
        }
    }
}
