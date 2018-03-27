
var pro = pro || {};
(function () {
    pro.RuleSendTicket = pro.RuleSendTicket || {};
    pro.RuleSendTicket.ListPage = pro.RuleSendTicket.ListPage || {};
    pro.RuleSendTicket.ListPage = {
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
                url: '/SalePromotionManager/RuleSendTicket/GetList',
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
         { field: 'TicketNum', title: '券张数', width: 100 },
         { field: 'CardTypeIds', title: '会员可类型范围', width: 100 },
         { field: 'TicketAvaildateEnd', title: '有效时间结束', width: 100 },
         { field: 'TicketAvaildateStart', title: '有效时间开始', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/SalePromotionManager/RuleSendTicket/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/SalePromotionManager/RuleSendTicket/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/SalePromotionManager/RuleSendTicket/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.RuleSendTicket.ListPage.initPage();
});


