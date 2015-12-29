using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wechat.WebUI.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        private string public_key = "71a992ad-e0f6-4402-870e-4073de3dc6dd";//PUBLIC_KEY(向点触申请)
        private string private_key = "3f971c89-ed36-4277-be00-350ad014c6db";//PRIVATE_KEY(向点触申请)

        public ActionResult TouClick()
        {
            ViewBag.PUBKEY = public_key;//向点触网站申请的公钥
            return View();
        }

        public void Verify()
        {
            var check_address = Request.Form["check_address"];
            var check_key = Request.Form["check_key"];
            if (check_address == null || check_key == null)
            {
                //错误处理
            }

            if (Wechat.Toolkit.Helper.TouClickHelper.Check(public_key, private_key, check_key, check_address))
            {
                //二次验证通过,可以放行请求
                Response.Write(true);
            }
            else
            {
                //恶意行为或是发生错误
                Response.Write(false);
            }
        }

    }
}
