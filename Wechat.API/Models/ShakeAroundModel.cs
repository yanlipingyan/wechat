using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat.API.Models
{
    public class ShakeAroundApplyModel
    {
        /// <summary>
        /// 必填，联系人姓名，不超过20汉字或40个英文字母
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 必填，联系人电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 必填，联系人邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 必填，平台定义的行业代号，具体请查看链接行业代号
        /// </summary>
        public Enums.WechatIndustryEnum IndustryId { get; set; }

        /// <summary>
        /// 必填，相关资质文件的图片url，图片需先上传至微信侧服务器，用“素材管理-上传图片素材”接口上传图片，返回的图片URL再配置在此处；当不需要资质文件时，数组内可以不填写url
        /// </summary>
        public string[] QualificationCertUrls { get; set; }

        /// <summary>
        /// 不必填，申请理由，不超过250汉字或500个英文字母
        /// </summary>
        public string ApplyReason { get; set; }
    }
}
