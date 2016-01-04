using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Wechat.Data.BusinessObjects;

namespace Wechat.Data
{
    public class DataWriterContext
    {
        #region 当前连接
        /// <summary>
        /// 核心模块write-session
        ///     描述：用户数据添加、修改、删除以及即时判断
        /// </summary>
        public virtual ISession Session
        {
            get { return HttpWriteModule.CurrentSession; }
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
