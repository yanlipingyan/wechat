using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.ResultModels
{
    public class GetUserCardListResult : WechatResult
    {
        public List<CardList> card_list { get; set; }
    }

    public class CardList
    {
        public string code { get; set; }
        public string card_id { get; set; }
    }
}
