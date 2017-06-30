
var pro = pro || {};
(function () {
    pro.Goods = pro.Goods || {};
    pro.Goods.ListPage = pro.Goods.ListPage || {};
    pro.Goods.ListPage = {
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
                url: '/ProductManager/Goods/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'ProductId', title: '', width: 100 },
         { field: 'GoodsCode', title: '货号', width: 100 },
         { field: 'GoodsStock', title: '库存', width: 100 },
         { field: 'GoodsPrice', title: '销售价', width: 100 },
         { field: 'GoodsCost', title: '成本价', width: 100 },
         { field: 'GoodsWeight', title: '重量', width: 100 },
         { field: 'GoodsWeightUnit', title: '重量单位', width: 100 },
         { field: 'Unit', title: '单位', width: 100 },
         { field: 'Title', title: '商品描述', width: 100 },
         { field: 'IsDefault', title: '是否是默认商品', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/ProductManager/Goods/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/ProductManager/Goods/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/ProductManager/Goods/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.Goods.ListPage.initPage();
});


