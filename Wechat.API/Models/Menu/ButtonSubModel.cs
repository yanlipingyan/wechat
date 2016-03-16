using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    /// <summary>
    /// 二级菜单
    /// </summary>
    public class ButtonSubModel : BaseButtonModel, IBaseButtonModel
    {
        /// <summary>
        /// 子按钮数组，按钮个数应为2~5个
        /// </summary>
        public List<BaseSingleButtonModel> sub_button { get; set; }


        public ButtonSubModel()
        {
            sub_button = new List<BaseSingleButtonModel>();
        }

        public ButtonSubModel(string name)
            : this()
        {
            base.name = name;
        }
    }
}
