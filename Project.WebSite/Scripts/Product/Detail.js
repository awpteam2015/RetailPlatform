var pro = pro || {};
(function () {
    pro.DetailPage = pro.DetailPage || {};
    pro.DetailPage = {
        initPage: function () {
            $("#btn_AddCart").click(function () {
                pro.DetailPage.AddCart();
            });
        },
        AddCart: function () {
           
            var postData = { goodsId: $("#GoodsId").val(), num: $("#Num").val() };
            $.ajax({
                dataType: 'json',
                type: 'POST',
                contentType: 'application/json',
                url: "/ShopCat/AddCart",
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
    pro.DetailPage.initPage();
});

