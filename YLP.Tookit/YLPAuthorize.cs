using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace System
{
    public class YLPMember
    {
        /// <summary>
        /// 会员Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 会员角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录状态
        /// </summary>
        public bool Enduring { get; set; }
    }

    public class YLPAuthorize
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        public const string SPLIT = "@wechat#";

        /// <summary>
        /// 会员Id
        /// </summary>
        public static string Id
        {
            get { return Serialize().Id; }
            set { }
        }

        /// <summary>
        /// 会员类型
        /// </summary>
        public static string Role
        {
            get { return Serialize().Role; }
            set { }
        }

        /// <summary>
        /// 登录帐号
        /// </summary>
        public static string Account
        {
            get { return Serialize().Account; }
            set { }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string Name
        {
            get { return Serialize().Name; }
            set { }
        }

        /// <summary>
        /// 登录状态
        /// </summary>
        public static bool Enduring
        {
            get { return Serialize().Enduring; }
            set { }
        }

        /// <summary>
        /// 将会员信息保存到cookie
        /// </summary>
        /// <param name="model"></param>
        public static void SetCookie(YLPMember model)
        {
            /*
             * Forms验证在内部的机制为把用户数据加密后保存在一个基于cookie的票据FormsAuthenticationTicket中，因为是经过特殊加密的，所以应该来说是比较安全的。
             * 而.net除了用这个票据存放自己的信息外，还留了一个地给用户自由支配，这就是现在要说的UserData。
             * 
             */

            var expires = DateTime.Now.AddMinutes(30);

            if (model.Enduring)
                expires = DateTime.Now.AddDays(7);

            //创建一个新的票据，将客户ip记入ticket的userdata
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,//int version, 
                model.Id,//string name, 
                DateTime.Now,//DateTime issueDate, 
                expires,//DateTime expiration, 
                false,//bool isPersistent, 
                string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", SPLIT, model.Id, model.Role, model.Account, model.Name, model.Enduring)//string userData
                );

            //将票据加密
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //将加密后的票据存入cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.Domain = FormsAuthentication.CookieDomain;
            if (model.Enduring)
                cookie.Expires = expires;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 清除cookie
        /// </summary>
        public static void Logoff()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        private static YLPMember Serialize()
        {
            FormsIdentity identity = HttpContext.Current.User.Identity as FormsIdentity;
            if (identity == null)
                return new YLPMember();
            if (identity.Ticket == null)
                return new YLPMember();
            if (string.IsNullOrEmpty(identity.Ticket.UserData))
                return new YLPMember();

            try
            {
                var array = identity.Ticket.UserData.Split(new string[] { SPLIT }, StringSplitOptions.None);
                if (array.Length != 5)
                {
                    return new YLPMember();
                }

                return new YLPMember
                {
                    Id = array[0],
                    Role = array[1],
                    Account = array[2],
                    Name = array[3],
                    Enduring = Convert.ToBoolean(array[4]),
                };
            }
            catch (Exception)
            {
                return new YLPMember();
            }
        }
    }
}
