using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.WebUI.Helper
{
    class JsApiTicketHelper
    {
        static Dictionary<string, Model.JsApiTicketModel> dic = new Dictionary<string, Model.JsApiTicketModel>();

        /// <summary>
        /// 获取JsApiTicket
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>JsApiTicketModel</returns>
        public static Model.JsApiTicketModel Get(string key)
        {
            if (!dic.ContainsKey(key))
                return new Model.JsApiTicketModel();

            return dic[key];
        }

        /// <summary>
        /// 存储JsApiTicketModel
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="model">JsApiTicketModel</param>
        public static void Set(string key, Model.JsApiTicketModel model)
        {
            dic.Remove(key);
            dic.Add(key, model);
        }
    }
}
