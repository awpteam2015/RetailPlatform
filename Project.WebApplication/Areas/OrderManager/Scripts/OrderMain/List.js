
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
         { field: 'OrderNo', title: '订单号', width: 100 },
         { field: 'State', title: '订单状态(作废:-1;未确认:0;确认:1;先退货审核:T;子订单部分为确认:2)', width: 100 },
         { field: 'Totalamount', title: '订单总价,包括赠品_decimal_', width: 100 },
         { field: 'PresentPoints', title: '赠送积分', width: 100 },
         { field: 'CustomerId', title: '会员Id', width: 100 },
         { field: 'CustomerName', title: '会员姓名', width: 100 },
         { field: 'Linkman', title: '联系人（改）', width: 100 },
         { field: 'LinkmanTel', title: '联系人电话', width: 100 },
         { field: 'LinkmanMobilephone', title: '联系人手机', width: 100 },
         { field: 'LinkmanProvinceId', title: '联系人省份', width: 100 },
         { field: 'LinkmanCityId', title: '联系人城市', width: 100 },
         { field: 'LinkmanAreaId', title: '联系人区域(新增)', width: 100 },
         { field: 'LinkmanAddress', title: '联系人配送地址（改）', width: 100 },
         { field: 'LinkmanAddressfull', title: '联系人配送地址全（改2012.11.2）', width: 100 },
         { field: 'LinkmanPostcode', title: '联系人邮政编码（改）', width: 100 },
         { field: 'LinkmanRemark', title: '联系人送货备注（改）', width: 100 },
         { field: 'CustomerAddressId', title: '送货地址id（改2012.11.20）', width: 100 },
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
         { field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'LastModifierUserCode', title: '修改人', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
         { field: 'IsDeleted', title: '是否删除', width: 100 },
         { field: 'DeleterUserCode', title: '删除人', width: 100 },
         { field: 'DeletionTime', title: '删除时间', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/OrderManager/OrderMain/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/OrderManager/OrderMain/Hd?PkId=" + PkId, "编辑" + PkId);
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
         closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.OrderMain.ListPage.initPage();
});


