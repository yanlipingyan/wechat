using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Wechat.API
{
    // 摘要:
    //      上传获取页面内容
    //  
    public static class WechatWebClient
    {
        /// <summary>
        /// 通过Url读取文件内容
        /// </summary>
        /// <param name="url">url路径</param>
        /// <returns>文件内容(string)</returns>
        public static string Get(string url)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        /// <summary>
        /// 通过Url读取文件内容。
        /// </summary>
        /// <typeparam name="T">返回某类型的model</typeparam>
        /// <param name="url">url路径</param>
        /// <returns>T</returns>
        public static T Get<T>(string url)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader reader = new StreamReader(stream);

            T t = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());

            return t;
        }

        /// <summary>
        /// 将数据上传到指定的Url上
        /// </summary>
        /// <param name="url">url路径</param>
        /// <param name="data">上传的内容</param>
        /// <returns>文件内容(string)</returns>
        public static string Post(string url, string data)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            return client.UploadString(url, data);
        }
    }
}
