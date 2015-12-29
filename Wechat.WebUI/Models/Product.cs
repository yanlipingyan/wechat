using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wechat.WebUI.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }
    }
}