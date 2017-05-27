
var pro = pro || {};
(function () {
    pro.Product = pro.Product || {};
    pro.Product.ListPage = pro.Product.ListPage || {};
    pro.Product.ListPage = {
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
                url: '/ProductManager/Product/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '111', width: 100 },
         { field: 'ProductName', title: '', width: 100 },
         { field: 'ProductCode', title: '', width: 100 },
         { field: 'Price', title: '', width: 100 },
         { field: 'ProductCategoryId', title: '', width: 100 },
         { field: 'SpecId1', title: '', width: 100 },
         { field: 'SpecId2', title: '', width: 100 },
         { field: 'SpecId3', title: '', width: 100 },
         { field: 'SpecName1', title: '', width: 100 },
         { field: 'SpecName2', title: '', width: 100 },
         { field: 'SpecName3', title: '', width: 100 },
         { field: 'Attribute1', title: '', width: 100 },
         { field: 'Attribute2', title: '', width: 100 },
         { field: 'Attribute3', title: '', width: 100 },
         { field: 'PicUrl1', title: '', width: 100 },
         { field: 'PicUrl2', title: '', width: 100 },
         { field: 'PicUrl3', title: '', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/ProductManager/Product/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/ProductManager/Product/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/ProductManager/Product/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.Product.ListPage.initPage();
});


