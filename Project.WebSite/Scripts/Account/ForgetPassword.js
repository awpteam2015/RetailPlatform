
var pro = pro || {};
(function () {
    pro.ForgetPasswordPage = pro.ForgetPasswordPage || {};
    pro.ForgetPasswordPage = {
        data: { delayTime: 120 },
        initPage: function () {

            /////步骤一//////////////////////////////////////////////////
            $("#btn_ChangeVerifyCode").click(function () {
                $("#checkcode_img").attr("src", "/SystemSet/VerifyCode?date=" + Math.random());
                $("#AuthCode").val("");
                $("#AuthCode").focus();


            });
            $("#checkcode_img").click(function () {
                this.src = "/SystemSet/VerifyCode?date=" + Math.random();
                $("#AuthCode").val("");
                $("#AuthCode").focus();
            });

            $("#btn_Step1").click(function () {

                var postData = { accountName: $("#AccountName").val(), authCode: $("#AuthCode").val() };
                var result = false;
                $.ajax({
                    dataType: 'json',
                    type: 'POST',
                    contentType: 'application/json',
                    url: "/Account/ForgetPasswordStep1",
                    data: JSON.stringify(postData),
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data.success) {
                            window.location.href = "/Account/ForgetPassword2?key=" + data.result;
                        } else {
                            alert(data.error.message);
                        }
                    }
                });


            });

            /////////步骤二///////////////////////////////////////////////////////////////////////////////

            $("#btn_SendMobileCode").click(function () {
                var mobile = $.trim($("#AccountName").html());
                var status = pro.ForgetPasswordPage.sendMobileCode(mobile);
                if (status) {
                    $("#btn_SendMobileCode").val("120秒后重新获取");
                    $("#btn_SendMobileCode").attr("disabled", "disabled");
                    //$("#mobilecode_valid").removeClass().addClass("field-validation-error DarkGray");
                    //$("#mobilecode_valid").html("<span class='yes_icon'></span>验证码已发送，请查收！");
                    setTimeout(pro.ForgetPasswordPage.countDown, 1000);
                } else {
                    $("#mobilecode_valid").removeClass().addClass("field-validation-error");
                    $("#mobilecode_valid").html("<span>发送失败</span>");
                }
            });

            $("#btn_Step2").click(function () {

                //pro.commonKit.getUrlParam("key") 会有等号特殊字符产生 getUrlParam不能满足  所以必须赋值到页面

                var postData = { key: $("#key").val(), authCode: $("#MobileCode").val() };
                var result = false;
                $.ajax({
                    dataType: 'json',
                    type: 'POST',
                    contentType: 'application/json',
                    url: "/Account/ForgetPasswordStep2",
                    data: JSON.stringify(postData),
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data.success) {
                            window.location.href = "/Account/ForgetPassword3?key=" + data.result;
                        } else {
                            alert(data.error.message);
                        }
                    }
                });

            });

            //////////步骤3//////////////////////////////////////////


            $("#btn_Step3").click(function () {

                var postData = { key: $("#key").val(), newPassword: $("#NewPassword").val() };
                var result = false;
                $.ajax({
                    dataType: 'json',
                    type: 'POST',
                    contentType: 'application/json',
                    url: "/Account/ForgetPasswordStep3",
                    data: JSON.stringify(postData),
                    cache: false,
                    async: false,
                    success: function (data) {
                        alert(JSON.stringify(data));
                        if (data.success) {
                            window.location.href = "/Account/ForgetPassword4";
                        } else {
                            alert(data.error.message);
                        }
                    }
                });

            });


        },
        sendMobileCode: function (i) {

            var postData = { mobile: i, authType: "forgetpassword" };
            var result = false;
            $.ajax({
                dataType: 'json',
                type: 'POST',
                contentType: 'application/json',
                url: "/SystemSet/SendMobileCode",
                data: JSON.stringify(postData),
                cache: false,
                async: false,
                success: function (data) {
                    alert(JSON.stringify(data));
                    if (data.success) {
                        result = true;
                    } else {
                        result = false;
                    }
                }
            });

            return result;
        },
        countDown: function () {
            pro.ForgetPasswordPage.data.delayTime--;
            $("#btn_SendMobileCode").val(pro.ForgetPasswordPage.data.delayTime + "秒后重新获取");
            if (pro.ForgetPasswordPage.data.delayTime == 0) {
                pro.ForgetPasswordPage.data.delayTime = 120;
                // verifyCodeState = false;
                //$("#mobilecode_valid").empty();
                //$("#mobilecode_valid").removeClass().addClass("field-validation-error");
                $("#btn_SendMobileCode").val("获取短信验证码");
                $("#btn_SendMobileCode").removeClass().addClass("btn btn_LightGray fl btn_h_30").removeAttr("disabled");
            } else {
                setTimeout(pro.ForgetPasswordPage.countDown, 1000);
            }
        }

    };
})();



$(function () {
    pro.ForgetPasswordPage.initPage();
});