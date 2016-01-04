using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Wechat.Data.BusinessObjects;

namespace Wechat.Data
{
    public class DataReaderContext
    {
        #region 当前连接
        /// <summary>
        /// 核心模块read-session
        ///     描述：用于查询列表
        /// </summary>
        public virtual ISession Session
        {
            get { return HttpReadModule.CurrentSession; }
        }
        #endregion

        /// <summary>
        /// Card表
        /// </summary>
        public IQueryable<Card> Cards
        {
            get { return Session.Query<Card>(); }
        }

    }
}
