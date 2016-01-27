using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YLP.Tookit.Helper
{
    public class TouClickHelper
    {
        /// <summary>
        /// 返回   true   则表示验证成功
        ///        false  表示验证失败或是没有验证或是其他错误发生
        /// </summary>
        /// <param name="public_key">公钥(需向点触申请)</param>
        /// <param name="private_key">私钥(需向点触申请)</param>
        /// <param name="check_key">二次验证口令(来自客户端post)</param>
        /// <param name="check_address">二次验证地址(来自客户端post)</param>
        /// <param name="client_ip">进行此次二次验证的用户IP(建议传输,网站会更加安全)</param>
        /// <param name="user_name">进行此次二次验证的用户名(统计数据使用)</param>
        /// <param name="user_id">进行此次二次验证的用户ID(统计数据使用)</param>
        /// <returns></returns>
        public static bool Check(string public_key, string private_key, string check_key, string check_address, string client_ip = "", string user_name = "0", string user_id = "0")
        {
            if (public_key == null || private_key == null || check_key == null || check_address == null)
                return false;

            try
            {
                Guid.Parse(public_key);
                Guid.Parse(private_key);
            }
            catch
            {
                //记录日志
                return false;
            }

            string url = null;

            if (!filter_host_path(check_address, out url) || url == null)
            {
                return false;
            }

            url = string.Format("{0}?b={1}&z={2}&i={3}&p={4}&un={5}&ud={6}", url, public_key, private_key, check_key, client_ip, user_name, user_id);

            try
            {
                var resString = HttpSend(url);
                if (resString != null && resString.IndexOf("[yes]") > -1)
                {
                    return true;
                }
                else
                {
                    //记录日志
                    return false;
                }
            }
            catch
            {
                //记录日志
                return false;
            }
        }

        /// <summary>
        /// 从check_address 中解析出 二次验证地址
        /// </summary>
        /// <param name="check_address">来自客户端post</param>
        /// <param name="url">输出参数</param>
        /// <returns></returns>
        private static bool filter_host_path(string check_address, out string url)
        {
            Func<string, bool> regStr = (str) => Regex.IsMatch(str, "^[a-z0-9]+$");

            string[] check_address_arr = check_address.Split(',');
            if (check_address_arr == null || check_address_arr.Length != 2)
            {
                goto FALSE;
            }

            string[] check_host_arr = check_address_arr[0].Split('.');
            if (check_host_arr == null || check_host_arr.Length != 3)
            {
                goto FALSE;
            }

            string[] check_path_arr = check_address_arr[1].Split('.');
            if (check_path_arr == null || check_path_arr.Length != 2)
            {
                goto FALSE;
            }

            if (regStr(check_host_arr[0]) && regStr(check_path_arr[0]))
            {
                url = string.Format("http://{0}.touclick.com/{1}.touclick", check_host_arr[0], check_path_arr[0]);
                return true;
            }
        FALSE:
            url = null;
            return false;
        }
        private static string HttpSend(string postUrl)
        {
            if (string.IsNullOrEmpty(postUrl))
            {
                throw new ArgumentNullException("HttpSend ArgumentNullException :  postUrl IsNullOrEmpty");
            }
            var request = (HttpWebRequest)WebRequest.Create(postUrl);

            request.Method = "GET";

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                var res_byt = ReadBuffer(responseStream, 9);
                if (res_byt == null)
                {
                    throw new ArgumentNullException("ReadBuffer ArgumentNullException : res_byt IsNullOrEmpty");
                }
                return System.Text.Encoding.UTF8.GetString(res_byt);
            }
        }
        private static byte[] ReadBuffer(Stream stream, int BufferLen)
        {
            if (BufferLen < 1)
            {
                BufferLen = 0x8000;
            }
            // 初始化一个缓存区
            byte[] buffer = new byte[BufferLen];
            int read = 0;
            int block;
            while ((block = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += block;
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte == -1)
                    {
                        return buffer;
                    }
                    byte[] newBuf = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuf, buffer.Length);
                    newBuf[read] = (byte)nextByte;

                    buffer = newBuf;
                    read++;
                }
            }
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            stream.Close();
            return ret;
        }
    }
}
