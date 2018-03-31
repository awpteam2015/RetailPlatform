var pro = pro || {};
(function () {
    pro.ListPage = pro.ListPage || {};
    pro.ListPage = {
        initPage: function () {
            //$("#btn_OrderSearch").click(function () {
            //    pro.ListPage.List();
            //});
        },
        List: function () {
            var StartDate = $("#StartDate").val();
            var EndDate = $("#EndDate").val();
            var OrderNo = $("#OrderNo").val();
          
            window.location.href = ( window.location.pathname + "?StartDate=" + StartDate + "&EndDate=" + EndDate + "&OrderNo=" + OrderNo);
       
        }
    };
})();


$(function () {
    pro.ListPage.initPage();
});

