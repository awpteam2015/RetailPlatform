var pro = pro || {};
(function () {
    pro.ListPage = pro.ListPage || {};
    pro.ListPage = {
        initPage: function () {
          
        },
        DelCart: function (i) {
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

        },
        UpdateCartNum: function (i) {
            var postData = { pkId: i ,num:$("#Num_"+i).val()};
            $.ajax({
                dataType: 'json',
                type: 'POST',
                contentType: 'application/json',
                url: "/ShopCart/UpdateCartNum",
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
        UpdateCartCheck: function (i) {

            var postData = { pkId: i, isCheck: $("#IsCheck_" + i).is(':checked')?1:2 };
            $.ajax({
                dataType: 'json',
                type: 'POST',
                contentType: 'application/json',
                url: "/ShopCart/UpdateCartCheck",
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

        AddOrder: function () {
            window.location.href = "/Order/Confirm";
        }


    };
})();


$(function () {
    pro.ListPage.initPage();
});

