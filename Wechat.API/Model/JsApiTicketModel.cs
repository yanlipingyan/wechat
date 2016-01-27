using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.WebUI.Model
{
    public class JsApiTicketModel
    {
        /// <summary>
        /// ticket
        /// </summary>
        private string ticket;

        public string Ticket
        {
            get { return ticket; }
            set { ticket = value; }
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
