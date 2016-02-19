using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Helper
{
    class JsApiTicketHelper
    {
        static Dictionary<string, Models.JsApiTicketModel> dic = new Dictionary<string, Models.JsApiTicketModel>();

        /// <summary>
        /// 获取JsApiTicket
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>JsApiTicketModel</returns>
        public static Models.JsApiTicketModel Get(string key)
        {
            if (!dic.ContainsKey(key))
                return new Models.JsApiTicketModel();

            return dic[key];
        }

        /// <summary>
        /// 存储JsApiTicketModel
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="model">JsApiTicketModel</param>
        public static void Set(string key, Models.JsApiTicketModel model)
        {
            dic.Remove(key);
            dic.Add(key, model);
        }
    }
}
