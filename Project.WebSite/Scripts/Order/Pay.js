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
            var orderNo = $("#OrderNo").val();
            var payCode = $("input[name='Paycode']:checked").val();


            var postData = { orderNo: orderNo, payCode: payCode };
            var flag = true;
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
                        flag= true;
                    } else {
                        flag= false;
                    }
                }
            });

            return flag;
        },
        ConfirmPay: function() {
            var result = pro.PayPage.CheckPay(); alert(result);
            if (result) {
                
                $("#btn_ConfirmPay").submit();
            }

        }
    };
})();


$(function () {
    pro.PayPage.initPage();
});

