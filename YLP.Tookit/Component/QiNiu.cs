using Qiniu.IO;
using Qiniu.RS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLP.Tookit.Helper;

namespace YLP.Tookit.Component
{
    public class QiNiu
    {
        /// <summary>
        /// 上传文件，普通上传
        /// </summary>
        /// <param name="bucket_name">设置上传的空间</param>
        /// <param name="filePath">上传文件的路径</param>
        public void Upload(string bucket_name, string filePath)
        {
            IOClient target = new IOClient();
            PutExtra extra = new PutExtra();
            //设置上传的文件的key值
            String key = "yourdefinekey";

            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间
            PutPolicy put = new PutPolicy(bucket_name, 3600);

            //调用Token()方法生成上传的Token
            string upToken = put.Token();
            Log4NetHelper.Error(string.Format("upToken:{0}", upToken));

            //调用PutFile()方法上传
            PutRet ret = target.PutFile(upToken, key, filePath, extra);

            //打印出相应的信息
            Log4NetHelper.Error(ret.Response.ToString());
            Log4NetHelper.Error(ret.key);
            //Console.WriteLine(ret.Response.ToString());
            //Console.WriteLine(ret.key);
            //Console.ReadLine();
        }
    }
}
