﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API
{
    public class WechatException : Exception
    {
        public WechatException(string msg)
            : base(msg)
        {

        }
    }
}
