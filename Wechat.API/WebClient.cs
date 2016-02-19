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
        // 摘要: 
        //     通过Url读取文件内容。
        //
        // 参数: 
        //   url:
        //     url路径。
        //
        // 返回结果: 文件内容(string)
        //
        public static string Get(string url)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        // 摘要: 
        //     将数据上传到指定的Url上。
        //
        // 参数: 
        //   url:
        //     url路径。
        //
        //   data:
        //     上传的内容。
        //
        // 返回结果: 文件内容(string)
        //
        public static string Post(string url, string data)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            return client.UploadString(url, data);
        }
    }
}
