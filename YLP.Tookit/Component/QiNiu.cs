using Qiniu.IO;
using Qiniu.RS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YLP.Tookit.Helper;

namespace YLP.Tookit.Component
{
    /// <summary>
    /// 七牛云存储
    /// </summary>
    public class QiNiu
    {
        /// <summary>
        /// 上传文件，普通上传(PutFile)
        /// </summary>
        /// <param name="bucket_name">设置上传的空间</param>
        /// <param name="local_file_path">上传文件的路径</param>
        /// <param name="ext"></param>
        public static string Upload(string bucket_name, string local_file_path, string ext)
        {
            IOClient iOClient = new IOClient();
            PutExtra extra = new PutExtra();

            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间
            PutPolicy putPolicy = new PutPolicy(bucket_name, 3600);

            //设置上传的文件的key值
            string key = Guid.NewGuid().ToString("N") + "." + ext;

            //调用Token()方法生成上传的Token
            string upToken = putPolicy.Token();

            //调用PutFile()方法上传
            PutRet putRet = iOClient.PutFile(upToken, key, local_file_path, extra);

            //打印出相应的信息
            string result;
            if (putRet == null)
            {
                result = null;
            }
            else
            {
                if (putRet.OK)
                {
                    result = key;
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }



        /// <summary>
        /// 上传文件，普通上传(Put)
        /// </summary>
        /// <param name="bucket_name">设置上传的空间</param>
        /// <param name="data">上传文件字节数据</param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string Upload(string bucket_name, byte[] data, string ext)
        {
            IOClient iOClient = new IOClient();
            PutExtra extra = new PutExtra();

            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间
            PutPolicy putPolicy = new PutPolicy(bucket_name, 3600u);

            //设置上传的文件的key值
            string key = Guid.NewGuid().ToString("N") + "." + ext;

            //调用Token()方法生成上传的Token
            string upToken = putPolicy.Token(null);

            PutRet putRet = null;
            using (Stream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Position = 0L;
                putRet = iOClient.Put(upToken, key, stream, extra);
            }

            string result;
            if (putRet == null)
            {
                result = null;
            }
            else
            {
                if (putRet.OK)
                {
                    result = key;
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 上传文件，普通上传(Put)
        /// </summary>
        /// <param name="bucket_name">设置上传的空间</param>
        /// <param name="stream">上传文件流数据</param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string Upload(string bucket_name, Stream stream, string ext)
        {
            IOClient iOClient = new IOClient();
            PutExtra extra = new PutExtra();

            PutPolicy putPolicy = new PutPolicy(bucket_name, 3600u);

            //设置上传的文件的key值
            string key = Guid.NewGuid().ToString("N") + "." + ext;

            string upToken = putPolicy.Token(null);

            PutRet putRet = iOClient.Put(upToken, key, stream, extra);

            string result;
            if (putRet == null)
            {
                result = null;
            }
            else
            {
                if (putRet.OK)
                {
                    result = key;
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 上传文件，普通上传(Put)
        /// </summary>
        /// <param name="bucket_name">设置上传的空间</param>
        /// <param name="key"></param>
        /// <param name="data">上传文件字节数据</param>
        /// <returns></returns>
        public static bool Upload(string bucket_name, string key, byte[] data)
        {
            IOClient iOClient = new IOClient();
            PutExtra extra = new PutExtra();

            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间
            PutPolicy putPolicy = new PutPolicy(bucket_name, 3600u);

            //调用Token()方法生成上传的Token
            string upToken = putPolicy.Token(null);

            PutRet putRet = null;
            using (Stream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Position = 0L;
                putRet = iOClient.Put(upToken, key, stream, extra);
            }

            return putRet != null && putRet.OK;
        }

        /// <summary>
        /// 上传文件，普通上传(Put)
        /// </summary>
        /// <param name="bucket_name">设置上传的空间</param>
        /// <param name="key"></param>
        /// <param name="stream">上传文件流数据</param>
        /// <returns></returns>
        public static bool Upload(string bucket_name, string key, Stream stream)
        {
            IOClient iOClient = new IOClient();
            PutExtra extra = new PutExtra();

            PutPolicy putPolicy = new PutPolicy(bucket_name, 3600u);

            string upToken = putPolicy.Token(null);

            stream.Position = 0L;
            PutRet putRet = iOClient.Put(upToken, key, stream, extra);

            return putRet != null && putRet.OK;
        }




        public static string GetDownloadUrl(string domain, string bucket_name, string key)
        {
            string format = GetPolicy.MakeBaseUrl(domain, key);
            return string.Format(format, bucket_name);
        }
        public static string GetDownloadUrlWithToken(string domain, string bucket_name, string key)
        {
            string baseUrl = string.Format(GetPolicy.MakeBaseUrl(domain, key), bucket_name);
            return GetPolicy.MakeRequest(baseUrl, 3600u, null);
        }



        public static string GetPublicThumbUrl(string domain, string bucket_name, string key)
        {
            string format = GetPolicy.MakeBaseUrl(domain, key) + "?imageView2/1/w/50/h/50/q/75";
            return string.Format(format, bucket_name);
        }
        public static string GetPrivateThumbUrl(string domain, string bucket_name, string key)
        {
            string baseUrl = string.Format(GetPolicy.MakeBaseUrl(domain, key), bucket_name) + "?imageView2/1/w/50/h/50/q/75";
            return GetPolicy.MakeRequest(baseUrl, 3600u, null);
        }
    }
}
