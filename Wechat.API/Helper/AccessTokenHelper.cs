using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.WebUI.Helper
{
    class AccessTokenHelper
    {
        static Dictionary<string, Model.AccessTokenModel> dic = new Dictionary<string, Model.AccessTokenModel>();

        /// <summary>
        /// 获取AccessTokenModel
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>AccessTokenModel</returns>
        public static Model.AccessTokenModel Get(string key)
        {
            if (!dic.ContainsKey(key))
                return new Model.AccessTokenModel();

            return dic[key];
        }

        /// <summary>
        /// 存储AccessTokenModel
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="model">AccessTokenModel</param>
        public static void Set(string key, Model.AccessTokenModel model)
        {
            dic.Remove(key);
            dic.Add(key, model);
        }
    }
}
