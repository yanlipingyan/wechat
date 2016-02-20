using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Wechat.API
{
    public static class Signature
    {
        public static bool Check(string signature, string timestamp, string nonce, string token)
        {
            return GetSign(timestamp, nonce, token) == signature;
        }

        static string GetSign(string timestamp, string nonce, string token)
        {
            //1. 将token、timestamp、nonce三个参数进行字典序排序
            string[] temp = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();

            //2. 将三个参数字符串拼接成一个字符串进行sha1加密
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes = UTF8Encoding.Default.GetBytes(string.Join("", temp));
            byte[] bytesHash = sha1.ComputeHash(bytes);

            //3. 返回加密后的字符串
            return BitConverter.ToString(bytesHash).Replace("-", "").ToLower();
            //return HexStringFromBytes(hashBytes);
        }
    }
}
