var pro = pro || {};
(function () {
    pro.PayPage = pro.PayPage || {};
    pro.PayPage = {
        initPage: function () {
            $("#btn_ConfirmPay").click(function () {
                pro.PayPage.ConfirmPay();
            });
        },
        CheckPay: function () {
            var postData = { pkId: i };
            $.ajax({
                dataType: 'json',
                type: 'POST',
                contentType: 'application/json',
                url: "/Order/CheckPay",
                data: JSON.stringify(postData),
                cache: false,
                async: false,
                success: function (data) {
                    alert(JSON.stringify(data));
                    if (data.success) {
                        return true;
                    } else {
                        return false;
                    }
                }
            });
       
        },
        ConfirmPay: function() {

            if (pro.PayPage.CheckPay()) {
                $("#btn_ConfirmPay").submit();
            }

        }
    };
})();


$(function () {
    pro.PayPage.initPage();
});

