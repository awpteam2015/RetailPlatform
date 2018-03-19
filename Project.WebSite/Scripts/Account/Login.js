
var pro = pro || {};
(function () {
    pro.LoginPage = pro.LoginPage || {};
    pro.LoginPage = {
        initPage: function () {

            $("#btn_Login").click(
                function () {
                    var postData = { AccountName: $("#AccountName").val(), Password: $("#Password").val() };

                    abp.ajax({
                        url: "/Account/Login",
                        data: JSON.stringify(postData)
                    }).done(
                    function (data) {

                        alert(JSON.stringify(data));
                    }
                    ).fail(
                    function (errordetails) {
                        alert(JSON.stringify(errordetails));
                    }
                    );


                }
            );
        }

    };
})();



$(function () {
    pro.LoginPage.initPage();
});