
var pro = pro || {};
(function () {
    pro.OrderMain = pro.OrderMain || {};
    pro.OrderMain.ListPage = pro.OrderMain.ListPage || {};
    pro.OrderMain.ListPage = {
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
                url: '/OrderManager/OrderMain/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'OrderNo', title: '订单号', width: 140 },
         { field: 'Attr_State', title: '订单状态', width: 100 },
          { field: 'Attr_ReturnState', title: '退货状态', width: 100 },
         { field: 'Totalamount', title: '订单总价', width: 100 },
         { field: 'CustomerName', title: '会员姓名', width: 100 },
         { field: 'Linkman', title: '联系人', width: 100 },
         { field: 'LinkmanTel', title: '联系人电话', width: 100 },
         { field: 'LinkmanAddressfull', title: '联系人配送地址全', width: 100 },
         { field: 'LinkmanRemark', title: '联系人送货备注', width: 100 },
         { field: 'PayTime', title: '支付时间', width: 100 },
         { field: 'PayRemark', title: '支付备注', width: 100 },
         { field: 'SendTime', title: '发货时间', width: 100 },
         { field: 'SendNo', title: '快递单号', width: 100 },
         { field: 'SendRemark', title: '发货备注', width: 100 },
         { field: 'ReturnReason', title: '退货原因', width: 100 },
         { field: 'ReturnNo', title: '退单快递单号', width: 100 },
         { field: 'ReturnState', title: '退货状态', width: 100 },
         { field: 'ReturnTime', title: '退货时间', width: 100 },
         { field: 'ReturnRemark', title: '退货备注', width: 100 },
         { field: 'ConfirmTime', title: '客户确认时间', width: 100 },
         { field: 'ConfirmRemark', title: '客户确认备注', width: 100 },
         { field: 'UserIp', title: '创建人ip', width: 100 },
         { field: 'CreatorUserCode', title: '创建人', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 }
         //{ field: 'LastModifierUserCode', title: '修改人', width: 100 },
         //{ field: 'LastModificationTime', title: '修改时间', width: 100 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );


            this.initSaleOderOp(tabObj, gridObj);

            this.initReturnOpNoSend(tabObj, gridObj);

            this.initReturnOpAfterSend(tabObj, gridObj);

           



            $("#btnView").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/OrderManager/OrderMain/Detail?OrderNo=" + gridObj.getSelectedRow().OrderNo + "&PkId=" + PkId, "查看" + PkId);
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
                        url: "/OrderManager/OrderMain/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
        //正单相关按钮的初始化
        initSaleOderOp: function (tabObj, gridObj) {
            $("#btnAdd").click(function () {
                tabObj.add("/OrderManager/OrderMain/Hd", "新增");
            });

            $("#btnPay").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                if (gridObj.getSelectedRow().State != 1) {
                    $.alertExtend.infoOp("请选择待付款订单！");
                    return;
                }

                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/Pay?OrderNo=" + OrderNo + "&PkId=" + PkId, "订单付款" + OrderNo);
            });

            $("#btnCancel").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                if (gridObj.getSelectedRow().State != 1) {
                    $.alertExtend.infoOp("请选择待付款订单！");
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/Cancel?OrderNo=" + OrderNo + "&PkId=" + PkId, "取消订单" + OrderNo);
            });

            $("#btnSend").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/Send?OrderNo=" + OrderNo + "&PkId=" + PkId, "发货" + OrderNo);
            });
        },
        //未发货相关按钮的初始化
        initReturnOpNoSend: function (tabObj, gridObj) {
            //未发货退货申请
            $("#btnReturnPayNoSend").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                if (gridObj.getSelectedRow().State != 2) {
                    $.alertExtend.infoOp("请选择已付款订单！");
                    return;
                }

                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/ReturnPayNoSend?OrderNo=" + OrderNo + "&PkId=" + PkId, "退款申请" + OrderNo);
            });


            $("#btnReturnPayNoSendConfirm").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/ReturnPayNoSendConfirm?OrderNo=" + OrderNo + "&PkId=" + PkId, "确认退款" + OrderNo);
            });
        },
        //已发货相关按钮的初始化
        initReturnOpAfterSend: function (tabObj, gridObj) {

            $("#btnReturnPrdAfterSend").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/ReturnPrdAfterSend?OrderNo=" + OrderNo + "&PkId=" + PkId, "退货申请" + OrderNo);
            });

            $("#btnReturnPrdAfterSendAudit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/ReturnPrdAfterSendAudit?OrderNo=" + OrderNo + "&PkId=" + PkId, "退货审核" + OrderNo);
            });

            $("#btnReturnPrdSend").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/ReturnPrdSend?OrderNo=" + OrderNo + "&PkId=" + PkId, "客户退货" + OrderNo);
            });


            $("#btnReturnPrdSendConfirm").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/ReturnPrdSendConfirm?OrderNo=" + OrderNo + "&PkId=" + PkId, "商家确认收货" + OrderNo);
            });


            $("#btnReturnPayAfterSend").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var OrderNo = gridObj.getSelectedRow().OrderNo;
                tabObj.add("/OrderManager/OrderMain/ReturnPayAfterSend?OrderNo=" + OrderNo + "&PkId=" + PkId, "退款" + OrderNo);
            });

        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.OrderMain.ListPage.initPage();
});


