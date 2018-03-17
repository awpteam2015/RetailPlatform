
var pro = pro || {};
(function () {
    pro.CustomerAddress = pro.CustomerAddress || {};
    pro.CustomerAddress.ListPage = pro.CustomerAddress.ListPage || {};
    pro.CustomerAddress.ListPage = {
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
                url: '/CustomerManager/CustomerAddress/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '自动增加得建立序列', width: 100 },
         { field: 'CustomerId', title: '', width: 100 },
         { field: 'Province', title: '送货地址  省', width: 100 },
         { field: 'CityId', title: '送货地址   市', width: 100 },
         { field: 'CountryId', title: '送货地址   区（新增）', width: 100 },
         { field: 'Address', title: '送货地址   详细地址', width: 100 },
         { field: 'IsDefault', title: '是否是默认地址', width: 100 },
         { field: 'Remarks', title: '备注', width: 100 },
         { field: 'ReceiverName', title: '收货人姓名', width: 100 },
         { field: 'FamilyTelephone', title: '电话', width: 100 },
         { field: 'PostCode', title: '邮编', width: 100 },
         { field: 'Mobilephone', title: '手机', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/CustomerManager/CustomerAddress/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/CustomerManager/CustomerAddress/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/CustomerManager/CustomerAddress/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.CustomerAddress.ListPage.initPage();
});


