var pro = pro || {};
(function () {
    pro.ListPage = pro.ListPage || {};
    pro.ListPage = {
        initPage: function () {
            $("#btn_OrderSearch").click(function () {
                pro.ListPage.List();
            });
        },
        List: function () {
            var CreateStart = $("#CreateStart").val();
            var CreateEnd = $("#CreateEnd").val();
            var OrderNo = $("#OrderNo").val();
          
            window.location.href = (window.location.pathname + "?CreateStart=" + CreateStart + "&CreateEnd=" + CreateEnd + "&OrderNo=" + OrderNo);
       
        }
    };
})();


$(function () {
    pro.ListPage.initPage();
});

