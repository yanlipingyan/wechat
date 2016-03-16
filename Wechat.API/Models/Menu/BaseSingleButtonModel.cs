using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public abstract class BaseSingleButtonModel : BaseButtonModel, IBaseButtonModel
    {
        /// <summary>
        /// 按钮类型（click或view）
        /// </summary>
        public string type { get; set; }

        public BaseSingleButtonModel(string theType)
        {
            type = theType;
        }
    }
}
