/*!
 * jQuery AjaxSumbit v1.0.0
 *
 * http://www.liblog.cn/
 *
 * Copyright (c) 2015 LiPing Yan
 * Released under the MIT license
 *
 */
$.extend($.fn, {
    ajaxSumbit: function (successFn) {
        var self = this,
        $form = $(this),
        $submit = $form.find("[type=submit]");

        if (typeof config == "undefined") {
            config = {
                disabled: false
            }
        }

        if (!config.disabled) {
            $submit.prop("disabled", false);
        }

        console.log(this);
        console.log($form);
        console.log($form.action);

        //jquery.validate
        $form.validate({

            //验证通过后 的js代码写在这里
            submitHandler: function (form) {

                console.log(form);
                console.log(form.action);

                //禁用按钮
                $submit.prop("disabled", true);

                //ajax提交
                $.ajax({
                    url: form.action,
                    data: $form.serialize(),
                    dataType: 'json',
                    type: 'post'
                }).done(function (res) {

                    if (res.Flag) {

                        //延时解除禁用按钮
                        setTimeout(function () {
                            $submit.prop("disabled", false).removeClass("disabled");
                        }, 2000);

                        //执行回调函数
                        if (typeof successFn == "function") {
                            successFn.call(self, res, $submit);
                            //return false
                        } else {
                            window.location.reload();
                        }

                    } else {

                        var error = typeof res.Content == "undefined" ? res : (res.Content[0][1] || res.Content || "抱歉！出错了，稍后重试");
                        alert(error);
                        //layer.msg(error, { icon: 5 });     //返回错误提示
                        $submit.prop("disabled", false);

                    }

                })
            }
        })

    }
});