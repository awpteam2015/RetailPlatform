var pro = pro || {};
(function () {
    pro.ConfirmPage = pro.ConfirmPage || {};
    pro.ConfirmPage = {
        initPage: function () {
            $("#btn_OrderSearch").click(function () {
                pro.ConfirmPage.Confirm();
            });
        },
        Confirm: function () {
            var postData = { pkId: i };
            $.ajax({
                dataType: 'json',
                type: 'POST',
                contentType: 'application/json',
                url: "/ShopCart/DelCart",
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
       
        }
    };
})();


$(function () {
    pro.ConfirmPage.initPage();
});

