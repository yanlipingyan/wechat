using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Model
{
    public class AccessTokenModel
    {
        /// <summary>
        /// token
        /// </summary>
        private string token;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        /// <summary>
        /// datetime
        /// </summary>
        private DateTime dateTime;

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
    }
}
