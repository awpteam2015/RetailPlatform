var pro = pro || {};
(function () {
    pro.DetailPage = pro.DetailPage || {};
    pro.DetailPage = {
        initPage: function () {
            $("#btn_OrderSearch").click(function () {
                pro.DetailPage.Detail();
            });
        },
        Detail: function () {
            var CreateStart = $("#CreateStart").val();
            var CreateEnd = $("#CreateEnd").val();
            var OrderNo = $("#OrderNo").val();

            window.location.href = (window.location.pathname + "?CreateStart=" + CreateStart + "&CreateEnd=" + CreateEnd + "&OrderNo=" + OrderNo);

        }
    };
})();


$(function () {
    pro.DetailPage.initPage();
});

