
var pro = pro || {};
(function () {
    pro.RegisterPage = pro.RegisterPage || {};
    pro.RegisterPage = {
        data: { delayTime :120},
        initPage: function () {

            $("#btn_SendMobileCode").click(function () {
                //var mobile = $.trim($("#UserName").val());
                var status =true ;//account.sendMobileCode(nameState, mobile);
                if (status) {
                    $("#btn_SendMobileCode").val("120秒后重新获取");
                    $("#btn_SendMobileCode").attr("disabled", "disabled");
                    //$("#mobilecode_valid").removeClass().addClass("field-validation-error DarkGray");
                    //$("#mobilecode_valid").html("<span class='yes_icon'></span>验证码已发送，请查收！");
                    setTimeout(pro.RegisterPage.countDown, 1000);
                } else {
                    $("#mobilecode_valid").removeClass().addClass("field-validation-error");
                    $("#mobilecode_valid").html("<span>发送失败</span>");
                }
            });

        },
        countDown: function () {
            pro.RegisterPage.data.delayTime--;
            $("#btn_SendMobileCode").val(pro.RegisterPage.data.delayTime+ "秒后重新获取");
            if (pro.RegisterPage.data.delayTime == 0) {
                pro.RegisterPage.data.delayTime = 120;
               // verifyCodeState = false;
                //$("#mobilecode_valid").empty();
                //$("#mobilecode_valid").removeClass().addClass("field-validation-error");
                $("#btn_SendMobileCode").val("获取短信验证码");
                $("#btn_SendMobileCode").removeClass().addClass("btn btn_LightGray fl btn_h_30").removeAttr("disabled");
            } else {
                setTimeout(pro.RegisterPage.countDown, 1000);
            }
        }

    };
})();



$(function () {
    pro.RegisterPage.initPage();
});