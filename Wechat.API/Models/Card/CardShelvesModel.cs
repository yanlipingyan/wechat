using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class CardShelvesModel
    {
        /// <summary>
        /// 必填，页面的banner图片链接，须调用，建议尺寸为640*300
        /// </summary>
        public string Banner { get; set; }
        /// <summary>
        /// 必填，页面的title
        /// </summary>
        public string PageTitle { get; set; }
        /// <summary>
        /// 必填，页面是否可以分享,填入true/false
        /// </summary>
        public bool CanShare { get; set; }
        /// <summary>
        /// 必填，投放页面的场景值  	 	 	 	 	 	
        /// </summary>
        public Enums.CardSceneEnum Scene { get; set; }
        /// <summary>
        /// 必填，卡券列表，每个item有两个字段
        /// </summary>
        public CardList[] CardList { get; set; }
    }

    public class CardList
    {
        /// <summary>
        /// 必填，所要在页面投放的cardid
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 必填，缩略图url
        /// </summary>
        public string ThumbUrl { get; set; }
    }
}
