using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat.API.Enums;

namespace Wechat.API.Models
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleClickButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为click时必须。
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        public SingleClickButton()
            : base(MenuButtonTypeEnum.click.ToString())
        {
        }
    }

    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleLocationSelectButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为location_select时必须。
        /// 用户点击按钮后，微信客户端将调起地理位置选择工具，完成选择操作后，将选择的地理位置发送给开发者的服务器，同时收起位置选择工具，随后可能会收到开发者下发的消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SingleLocationSelectButton()
            : base(MenuButtonTypeEnum.location_select.ToString())
        {
        }
    }

    /// <summary>
    /// 单个按键
    /// </summary>
    public class SinglePicPhotoOrAlbumButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为pic_photo_or_album时必须。
        /// 用户点击按钮后，微信客户端将弹出选择器供用户选择“拍照”或者“从手机相册选择”。用户选择后即走其他两种流程。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SinglePicPhotoOrAlbumButton()
            : base(MenuButtonTypeEnum.pic_photo_or_album.ToString())
        {
        }
    }

    /// <summary>
    /// 单个按键
    /// </summary>
    public class SinglePicSysphotoButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为pic_sysphoto时必须。
        /// 用户点击按钮后，微信客户端将调起系统相机，完成拍照操作后，会将拍摄的相片发送给开发者，并推送事件给开发者，同时收起系统相机，随后可能会收到开发者下发的消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SinglePicSysphotoButton()
            : base(MenuButtonTypeEnum.pic_sysphoto.ToString())
        {
        }
    }

    /// <summary>
    /// 单个按键
    /// </summary>
    public class SinglePicWeixinButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为pic_weixin时必须。
        /// 用户点击按钮后，微信客户端将调起微信相册，完成选择操作后，将选择的相片发送给开发者的服务器，并推送事件给开发者，同时收起相册，随后可能会收到开发者下发的消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SinglePicWeixinButton()
            : base(MenuButtonTypeEnum.pic_weixin.ToString())
        {
        }
    }

    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleScancodePushButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为scancode_push时必须。
        /// 用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后显示扫描结果（如果是URL，将进入URL），且会将扫码的结果传给开发者，开发者可以下发消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SingleScancodePushButton()
            : base(MenuButtonTypeEnum.scancode_push.ToString())
        {
        }
    }

    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleScancodeWaitmsgButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为scancode_waitmsg时必须。
        /// 用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后，将扫码的结果传给开发者，同时收起扫一扫工具，然后弹出“消息接收中”提示框，随后可能会收到开发者下发的消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SingleScancodeWaitmsgButton()
            : base(MenuButtonTypeEnum.scancode_waitmsg.ToString())
        {
        }
    }

    /// <summary>
    /// Url按钮
    /// </summary>
    public class SingleViewButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为view时必须
        /// 网页链接，用户点击按钮可打开链接，不超过256字节
        /// </summary>
        public string url { get; set; }

        public SingleViewButton()
            : base(MenuButtonTypeEnum.view.ToString())
        {
        }
    }

    /// <summary>
    /// 多媒体按钮
    /// </summary>
    public class SingleMediaIdButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为media_id时必须
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        public string media_id { get; set; }

        public SingleMediaIdButton()
            : base(MenuButtonTypeEnum.media_id.ToString())
        {
        }
    }

    /// <summary>
    /// 多媒体按钮
    /// </summary>
    public class SingleViewLimitedButton : BaseSingleButtonModel
    {
        /// <summary>
        /// 类型为view_limited时必须
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        public string media_id { get; set; }

        public SingleViewLimitedButton()
            : base(MenuButtonTypeEnum.view_limited.ToString())
        {
        }
    }
}
