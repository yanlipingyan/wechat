using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.Toolkit.Helper
{
    public class LogHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 加载log4net配置文件
        /// </summary>
        /// <param name="path">log4net配置文件路径</param>
        public static void LoadConfig(string path)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
        }

        /// <summary>
        /// 错误信息
        /// </summary
        public static void Error(string error)
        {
            log.Error(error);
        }

        /// <summary>
        /// 致命信息
        /// </summary>
        public static void Fatal(string fatal)
        {
            log.Fatal(fatal);
        }

        /// <summary>
        /// 一般信息
        /// </summary>
        public static void Info(string info)
        {
            log.Info(info);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        public static void Warn(string warn)
        {
            log.Warn(warn);
        }
    }
}
