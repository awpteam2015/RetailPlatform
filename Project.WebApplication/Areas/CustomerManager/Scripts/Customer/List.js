
var pro = pro || {};
(function () {
    pro.Customer = pro.Customer || {};
    pro.Customer.ListPage = pro.Customer.ListPage || {};
    pro.Customer.ListPage = {
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
                url: '/CustomerManager/Customer/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'CardNo', title: '', width: 100 },
         { field: 'Password', title: '密码', width: 100 },
         { field: 'CustomerName', title: '会员名称', width: 100 },
         { field: 'Gender', title: '性别', width: 100 },
         { field: 'Birthday', title: '生日', width: 100 },
         { field: 'Email', title: '邮件', width: 100 },
         { field: 'Familytelephone', title: '家庭电话', width: 100 },
         { field: 'Postcode', title: '邮编', width: 100 },
         { field: 'Mobilephone', title: '手机', width: 100 },
         { field: 'ProvinceId', title: '居住地址   省', width: 100 },
         { field: 'CityId', title: '居住地址   市', width: 100 },
         { field: 'CountryId', title: '居住地址   区（新增）', width: 100 },
         { field: 'Address', title: '居住地址   详细地址', width: 100 },
         { field: 'Memo', title: '备注', width: 100 },
         { field: 'Discount', title: '折扣率', width: 100 },
         { field: 'Totalamount', title: '消费总金额', width: 100 },
         { field: 'Totalpoints', title: '总积分', width: 100 },
         { field: 'Availablepoints', title: '可用积分', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
         { field: 'LastModifierUserCode', title: '修改人', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'CreatorUserCode', title: '创建人', width: 100 },
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
               tabObj.add("/CustomerManager/Customer/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/CustomerManager/Customer/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/CustomerManager/Customer/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.Customer.ListPage.initPage();
});


