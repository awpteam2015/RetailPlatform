
var pro = pro || {};
(function () {
    pro.RuleDiscountMoney = pro.RuleDiscountMoney || {};
    pro.RuleDiscountMoney.ListPage = pro.RuleDiscountMoney.ListPage || {};
    pro.RuleDiscountMoney.ListPage = {
      init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false)
            };
        },
        initPage: function () {
            var initObj = this.init();
            var tabObj = initObj.tabObj;
            var gridObj = initObj.gridObj;
            gridObj.grid({
                url: '/SalePromotionManager/RuleDiscountMoney/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'RuleId', title: '活动规则Id', width: 100 },
         { field: 'ActivityId', title: '活动Id', width: 100 },
         { field: 'StartMoney', title: '订单金额范围', width: 100 },
         { field: 'EndMoney', title: '订单金额范围', width: 100 },
         { field: 'DiscountMoney', title: '折扣金额', width: 100 },
         { field: 'CardTypeIds', title: '会员可类型范围', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/SalePromotionManager/RuleDiscountMoney/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/SalePromotionManager/RuleDiscountMoney/Hd?PkId=" + PkId, "编辑" + PkId);
            });


            $("#btnSearch").click(function () {
                gridObj.search();
            });

            $("#btnDel").click(function () {
                if (!gridObj.isSelected()) {
                $.alertExtend.infoOp();
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/SalePromotionManager/RuleDiscountMoney/Delete?PkId=" + gridObj.getSelectedRow().PkId
                    }).done(
                    function (dataresult, data) {
                        $.alertExtend.info();
                        gridObj.search();
                    }
                    ).fail(
                    function (errordetails, errormessage) {
                        $.alertExtend.error();
                    }
                    );
                });
            });

            $("#btnRefresh").click(function () {
                gridObj.refresh();
            });
        },
         closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.RuleDiscountMoney.ListPage.initPage();
});


