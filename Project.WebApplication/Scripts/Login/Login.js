var pro = pro || {};
(function () {
    pro.LoginPage = pro.LoginPage || {};
    pro.LoginPage = {
        initPage: function () {
            $("#btn_Login").click(function () {
            
                pro.LoginPage.Login();
            });
        },
        Login: function () {
            var postData = pro.submitKit.getHeadJson();

            $.extend(abp.ajax, {
                showError: function(error) {
                    if (error.details) {
                        return $.alertExtend.error(error.details + error.message);
                        // abp.message.error(error.details, error.message);
                    } else {
                        $("#tip").html(error.message);
                        $(".input-tips").css("display","");

                        return true;
                        //return $.alertExtend.error("操作失败111111111！" + error.message);
                        // return abp.message.error(error.message);
                    }
                }
            });

            abp.ajax({
                url: "/Login/UserLogin",
                data: JSON.stringify(postData)
            }).done(
                function (data, data2) {
                    window.location.href = "/Account/Index";
                }
            );


        }
    };
})();


$(function () {
    pro.LoginPage.initPage();
});

