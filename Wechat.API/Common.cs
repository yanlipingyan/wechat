using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Wechat.API
{
    public static class Common
    {
        /// <summary>
        /// jsapiTicket是否有效。
        /// </summary>
        /// <param name="date">上次卡券jsapiTicket生成的时间</param>
        /// <param name="second">要添加的秒数</param>
        /// <returns>True：卡券jsapiTicket已过期；False：卡券jsapiTicket未过期</returns>
        public static bool IsExprie(DateTime date, int second = 7000)
        {
            return DateTime.Now > date.AddSeconds(second);
        }

        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns>时间戳（string）</returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 生成32位随机字符串
        /// </summary>
        /// <returns>随机数（string）</returns>
        public static string GetNonceStr()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// SortedDictionary格式转化成url参数键值对格式
        /// </summary>
        /// <param name="orderParams">发送或者接收到的数据集合M按照ASCII码字典序集合</param>
        /// <param name="sign">转换后是否包含sign字段，true:包含；false：不包含</param>
        /// <returns>url格式串, 该串不包含sign字段值</returns>
        public static string SortedDictionaryToUrl(SortedDictionary<string, object> orderParams, bool isIncludeSign = false)
        {
            string stringA = "";

            if (isIncludeSign)//包含
            {
                foreach (KeyValuePair<string, object> pair in orderParams)
                {

                    if (pair.Value.ToString() != "")
                    {
                        stringA += pair.Key + "=" + pair.Value + "&";
                    }
                }
            }
            else//不包含
            {
                foreach (KeyValuePair<string, object> pair in orderParams)
                {
                    if (pair.Key != "sign" && pair.Value.ToString() != "")
                    {
                        stringA += pair.Key + "=" + pair.Value + "&";
                    }
                }
            }

            return stringA.Trim('&');
        }

        /// <summary>
        /// 将SortedDictionary转成xml
        /// </summary>
        /// <param name="orderParams">发送或者接收到的数据集合M按照ASCII码字典序集合</param>
        /// <returns>xml(string)</returns>
        public static string SortedDictionaryToXml(SortedDictionary<string, object> orderParams)
        {
            if (orderParams.Count() == 0)
                return "";

            string xml = "<xml>";

            foreach (KeyValuePair<string, object> pair in orderParams)
            {
                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
            }

            xml += "</xml>";

            return xml;
        }

        /// <summary>
        /// 将xml转为Dictionary
        /// </summary>
        /// <param name="xml">待转换的xml字符串</param>
        /// <returns>经转换得到的Dictionary</returns>
        public static SortedDictionary<string, object> XmlToSortedDictionary(string xml)
        {
            SortedDictionary<string, object> orderParams = new SortedDictionary<string, object>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;

            foreach (var node in xmlNodeList)
            {
                XmlElement xmlElement = (XmlElement)node;
                orderParams[xmlElement.Name] = xmlElement.InnerText;
            }

            return orderParams;
        }
    }
}
