using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NHibernate;

namespace Wechat.Data
{
    public class HttpReadModule : IHttpModule
    {
        public const string KEY = "NhibernateHttpReadSession";

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            //context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        private void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpContext context = app.Context;

            ISession session = (ISession)context.Items[KEY];

            if (session != null)
            {
                session.Flush();
                session.Close();
            }
            context.Items[KEY] = null;
        }

        /// <summary>
        /// TDD 测试用
        /// </summary>
        private static ISession _currentSession;

        public static ISession CurrentSession
        {
            get
            {
                HttpContext currentContext = HttpContext.Current;

                #region TDD 测试用
                if (currentContext == null)
                {
                    if (_currentSession != null)
                    {
                        return _currentSession;
                    }
                    else
                    {
                        _currentSession = OpenNewSession();
                        return _currentSession;
                    }
                } 
                #endregion
                else
                {
                    ISession session = (ISession)currentContext.Items[KEY];
                    if (session == null)
                    {
                        session = OpenNewSession();
                        currentContext.Items[KEY] = session;
                    }
                    return session;
                }

            }
        }

        private static ISessionFactory sessionFactory = null;

        private static ISessionFactory GetFactory()
        {
            if (sessionFactory == null)
            {
                HttpContext currentContext = HttpContext.Current;

                NHibernate.Cfg.Configuration config = new NHibernate.Cfg.Configuration();

                if (config == null)
                {
                    throw new InvalidOperationException("Nhibernate configuration is null");
                }

                var path = string.IsNullOrEmpty(HttpRuntime.AppDomainAppId) ? AppDomain.CurrentDomain.BaseDirectory : HttpRuntime.AppDomainAppPath;

                config.Configure(string.Format("{0}/Config/Nhibernate/HttpReadNHibernate.cfgl.xml", path));

                sessionFactory = config.BuildSessionFactory();

                if (sessionFactory == null)
                {
                    throw new InvalidOperationException("Call to Configuration.BuildSessionFactory() returned null.");
                }

            }
            return sessionFactory;
        }

        public static ISession OpenNewSession()
        {
            ISessionFactory sessionFactory = GetFactory();
            ISession session = sessionFactory.OpenSession();

            if (session == null)
            {
                throw new InvalidOperationException("Call to SessionFactory.OpenSession() returned null.");
            }

            return session;
        }

        #endregion
    }
}
