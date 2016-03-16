using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    /// <summary>
    /// 整个按钮设置（可以直接用ButtonGroup实例返回JSON对象）
    /// </summary>
    public class ButtonGroupModel
    {
        /// <summary>
        /// 按钮数组，按钮个数应为2~3个
        /// </summary>
        public List<BaseButtonModel> button { get; set; }

        /// <summary>
        /// 个性化菜单匹配规则。matchrule共六个字段，均可为空，但不能全部为空，至少要有一个匹配信息是不为空的。
        /// </summary>
        public PersonaliseButtonModel matchrule { get; set; }


        public ButtonGroupModel()
        {
            button = new List<BaseButtonModel>();
        }
    }
}
