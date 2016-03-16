using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wechat.API;
using Wechat.API.Enums;
using Wechat.API.Models;

namespace Wechat.WebUI.Areas.Test.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Test/Menu/

        public ActionResult Index()
        {
            return Content(Menu.GetMenu(ApiModel.AppID, ApiModel.AppSecret));
        }

        public ActionResult Create()
        {
            string domain = "http://crp.linkin.net";

            ButtonGroupModel bg = new ButtonGroupModel();

            //第一个菜单
            bg.button.Add(new SingleViewButton()
            {
                name = "悬赏大厅",
                url = domain + "/wx",
            });

            //第二个菜单
            bg.button.Add(new SingleViewButton()
            {
                name = "推荐记录",
                url = domain + "/wx#/recommendedRecord"
            });

            //第三个菜单（是二级菜单）
            var thirdMenu = new ButtonSubModel()
            {
                name = "我的"
            };
            //thirdMenu.sub_button.Add(new SingleViewButton()
            //{
            //    name = "资料",
            //    url = "/wx#/my"
            //});
            //thirdMenu.sub_button.Add(new SingleViewButton()
            //{
            //    name = "人才库",
            //    url = "/wx#/talentPool"
            //});
            //thirdMenu.sub_button.Add(new SingleViewButton()
            //{
            //    name = "添加人才",
            //    url = "/wx#/addTalentImg"
            //});
            //thirdMenu.sub_button.Add(new SingleClickButton()
            //{
            //    name = "测试Click",
            //    key = "发送测试Click"
            //});
            //thirdMenu.sub_button.Add(new SingleScancodePushButton()
            //{
            //    name = "测试ScancodePush",
            //    key = "发送测试ScancodePush"
            //});
            thirdMenu.sub_button.Add(new SingleScancodeWaitmsgButton()
            {
                name = "测试ScancodeWaitmsg",
                key = "发送测试ScancodeWaitmsg"
            });
            thirdMenu.sub_button.Add(new SinglePicSysphotoButton()
            {
                name = "测试PicSysphoto",
                key = "发送测试PicSysphoto"
            });
            thirdMenu.sub_button.Add(new SinglePicPhotoOrAlbumButton()
            {
                name = "测试PicPhotoOrAlbum",
                key = "发送测试PicPhotoOrAlbum"
            });
            thirdMenu.sub_button.Add(new SinglePicWeixinButton()
            {
                name = "测试PicWeixin",
                key = "发送测试PicWeixin"
            });
            thirdMenu.sub_button.Add(new SingleLocationSelectButton()
            {
                name = "测试LocationSelect",
                key = "发送测试LocationSelect"
            });
            bg.button.Add(thirdMenu);

            return Content(JsonConvert.SerializeObject(Menu.CreateMenu(ApiModel.AppID, ApiModel.AppSecret, bg)));
        }

        public ActionResult Delete()
        {
            return Content(JsonConvert.SerializeObject(Menu.DeleteAllMenu(ApiModel.AppID, ApiModel.AppSecret)));
        }

        public ActionResult CreatePersonalise()
        {
            ButtonGroupModel bg = new ButtonGroupModel();

            //第一个菜单
            bg.button.Add(new SingleClickButton()
            {
                name = "测试Click",
                key = "发送测试Click"
            });

            //第二个菜单
            bg.button.Add(new SingleScancodePushButton()
            {
                name = "测试ScancodePush",
                key = "发送测试ScancodePush"
            });

            //第三个菜单（是二级菜单）
            var thirdMenu = new ButtonSubModel()
            {
                name = "我的"
            };
            thirdMenu.sub_button.Add(new SingleScancodeWaitmsgButton()
            {
                name = "测试ScancodeWaitmsg",
                key = "发送测试ScancodeWaitmsg"
            });
            thirdMenu.sub_button.Add(new SinglePicSysphotoButton()
            {
                name = "测试PicSysphoto",
                key = "发送测试PicSysphoto"
            });
            thirdMenu.sub_button.Add(new SinglePicPhotoOrAlbumButton()
            {
                name = "测试PicPhotoOrAlbum",
                key = "发送测试PicPhotoOrAlbum"
            });
            thirdMenu.sub_button.Add(new SinglePicWeixinButton()
            {
                name = "测试PicWeixin",
                key = "发送测试PicWeixin"
            });
            thirdMenu.sub_button.Add(new SingleLocationSelectButton()
            {
                name = "测试LocationSelect",
                key = "发送测试LocationSelect"
            });
            bg.button.Add(thirdMenu);

            bg.matchrule = new PersonaliseButtonModel() { sex = "2" };

            return Content(JsonConvert.SerializeObject(Menu.CreatePersonaliseMenu(ApiModel.AppID, ApiModel.AppSecret, bg)));
        }

        public ActionResult DeletePersonalise(string menuId)
        {
            return Content(JsonConvert.SerializeObject(Menu.DeletePersonaliseMenu(ApiModel.AppID, ApiModel.AppSecret, menuId)));
        }

        public ActionResult TryMatch(string userId) 
        {
            return Content(Menu.TryMatchPersonaliseMenu(ApiModel.AppID, ApiModel.AppSecret, userId));
        }
    }
}
