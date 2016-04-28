﻿
//扩展验证规则
jQuery.validator.addMethod("hreecmail", function (value, element) {
    var mail = /^[A-Za-z0-9._%-]+@hreec.com$/;
    return this.optional(element) || (mail.test(value));
}, "必须为企业邮箱");

//邮箱
$.validator.addMethod("mail", function (value, element) {
    var mail = /^[A-Za-z0-9._%-]+@([A-Za-z0-9-]+\.)+[A-Za-z]{2,4}$/;
    return this.optional(element) || (mail.test(value));
}, "邮箱格式不对");

//电话验证规则
$.validator.addMethod("phone", function (value, element) {
    var phone = /^0\d{2,3}-\d{7,8}$/;
    return this.optional(element) || (phone.test(value));
}, "电话格式如：0371-68787027");

//区号验证规则
$.validator.addMethod("ac", function (value, element) {
    var ac = /^0\d{2,3}$/;
    return this.optional(element) || (ac.test(value));
}, "区号如：010或0371");

//无区号电话验证规则
$.validator.addMethod("noactel", function (value, element) {
    var noactel = /^\d{7,8}$/;
    return this.optional(element) || (noactel.test(value));
}, "电话格式如：68787027");

//手机验证规则
$.validator.addMethod("mobile", function (value, element) {
    var mobile = /^1[3|4|5|7|8]\d{9}$/;
    return this.optional(element) || (mobile.test(value));
}, "手机格式不对");

//邮箱或手机验证规则
$.validator.addMethod("mm", function (value, element) {
    var mm = /^[A-Za-z0-9._%-]+@([A-Za-z0-9-]+\.)+[A-Za-z]{2,4}$|^1[3|4|5|7|8]\d{9}$/;
    return this.optional(element) || (mm.test(value));
}, "格式不对");

//电话或手机验证规则
$.validator.addMethod("tm", function (value, element) {
    var tm = /(^1[3|4|5|7|8]\d{9}$)|(^\d{3,4}-\d{7,8}$)|(^\d{7,8}$)|(^\d{3,4}-\d{7,8}-\d{1,4}$)|(^\d{7,8}-\d{1,4}$)|(^\d{3,4}-?\d{3,4}-?\d{2,4}$)/;
    return this.optional(element) || (tm.test(value));
}, "格式不对");

//年龄
$.validator.addMethod("age", function (value, element) {
    var age = /^(?:[1-9][0-9]?|1[01][0-9]|120)$/;
    return this.optional(element) || (age.test(value));
}, "不能超过120岁");
///// 20-60   /^([2-5]\d)|60$/

//传真
$.validator.addMethod("fax", function (value, element) {
    var fax = /^(\d{3,4})?[-]?\d{7,8}$/;
    return this.optional(element) || (fax.test(value));
}, "传真格式如：0371-68787027");

//验证当前值和目标val的值相等 相等返回为 false
$.validator.addMethod("equalTo2", function (value, element) {
    var returnVal = true;
    var id = $(element).attr("data-rule-equalto2");
    var targetVal = $(id).val();
    if (value === targetVal) {
        returnVal = false;
    }
    return returnVal;
}, "不能和原始密码相同");

//大于指定数
$.validator.addMethod("gt", function (value, element) {
    var returnVal = false;
    var gt = $(element).data("gt");
    if (value > gt && value != "") {
        returnVal = true;
    }
    return returnVal;
}, "不能小于0 或空");

//汉字
$.validator.addMethod("chinese", function (value, element) {
    var chinese = /^[\u4E00-\u9FFF]+$/;
    return this.optional(element) || (chinese.test(value));
}, "格式不对");

//字母或数字
$.validator.addMethod("dw", function (value, element) {
    var dw = /^[A-Za-z0-9]+$/;
    return this.optional(element) || (dw.test(value));
}, "必须为数字/字母,建议：字母和数组组合！");

//字母或数字
$.validator.addMethod("password", function (value, element) {
    var dw = /^[A-Za-z0-9]+$/;
    return this.optional(element) || (dw.test(value));
}, "必须为数字/字母");

//指定数字的整数倍
$.validator.addMethod("times", function (value, element) {
    var returnVal = true;
    var base = $(element).attr('data-rule-times');
    if (value % base != 0) {
        returnVal = false;
    }
    return returnVal;
}, "必须是发布赏金的整数倍");

//身份证
$.validator.addMethod("idCard", function (value, element) {
    var isIDCard1 = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;//(15位)
    var isIDCard2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;//(18位)

    return this.optional(element) || (isIDCard1.test(value)) || (isIDCard2.test(value));
}, "格式不对");
