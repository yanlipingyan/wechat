using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class ProjectPathController : Controller
    {
        //
        // GET: /Test/ProjectPath/

        public ActionResult Index()
        {
            //使用 HTTP 上下文中的 Server 对象来获取Web站点的根目录
            string MapPath = Server.MapPath("~");

            //使用应用程序域对象获取当前线程的应用程序域的基准目录
            string BaseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

            //使用应用程序域对象获取当前线程的应用程序域的配置信息中的应用程序目录
            string ApplicationBase = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //获取当前进程的主模块的文件名（全路径。由该文件路径可以得到程序集所在的目录）
            string FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            //获取应用程序环境的当前目录
            string CurrentDirectory = System.Environment.CurrentDirectory;

            //使用静态类 Directory 下的 GetCurrentDirectory 方法获取当前程序的路径
            string GetCurrentDirectory = System.IO.Directory.GetCurrentDirectory();

            //获取调用该方法的方法所在的程序集，并获取该程序集文件路径（由该文件路径可以得到程序集所在的目录）
            string GetCallingAssembly = "";//System.Reflection.Assembly.GetCallingAssembly().Location;

            //获取包含该应用程序入口点的程序集（可执行文件），并获取该程序集文件的路径（由该文件路径可以得到程序集所在的目录）
            string GetEntryAssembly = "";//System.Reflection.Assembly.GetEntryAssembly().Location;

            //获取执行该方法的程序集，并获取该程序集的文件路径（由该文件路径可以得到程序集所在的目录）
            string GetExecutingAssembly = "";//System.Reflection.Assembly.GetExecutingAssembly().Location;

            string content = "Server.MapPath(\"~\")</br>" + MapPath + "</br></br>"
                + "System.AppDomain.CurrentDomain.BaseDirectory</br>" + BaseDirectory + "</br></br>"
                + "System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase</br>" + ApplicationBase + "</br></br>"
                + "System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName</br>" + FileName + "</br></br>"
                + "System.Environment.CurrentDirectory</br>" + CurrentDirectory + "</br></br>"
                + "System.IO.Directory.GetCurrentDirectory()</br>" + GetCurrentDirectory + "</br></br>"
                + "System.Reflection.Assembly.GetCallingAssembly().Location</br>" + GetCallingAssembly + "</br></br>"
                + "System.Reflection.Assembly.GetEntryAssembly().Location</br>" + GetEntryAssembly + "</br></br>"
                + "System.Reflection.Assembly.GetExecutingAssembly().Location</br>" + GetExecutingAssembly;

            return Content(content);
        }

    }
}
