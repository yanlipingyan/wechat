using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Helper
{
    class AccessTokenHelper
    {
        static Dictionary<string, Models.AccessTokenModel> dic = new Dictionary<string, Models.AccessTokenModel>();

        /// <summary>
        /// 获取AccessTokenModel
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>AccessTokenModel</returns>
        public static Models.AccessTokenModel Get(string key)
        {
            if (!dic.ContainsKey(key))
                return new Models.AccessTokenModel();

            return dic[key];
        }

        /// <summary>
        /// 存储AccessTokenModel
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="model">AccessTokenModel</param>
        public static void Set(string key, Models.AccessTokenModel model)
        {
            dic.Remove(key);
            dic.Add(key, model);
        }
    }
}
