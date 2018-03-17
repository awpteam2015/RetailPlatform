
var pro = pro || {};
(function () {
    pro.ShopCart = pro.ShopCart || {};
    pro.ShopCart.ListPage = pro.ShopCart.ListPage || {};
    pro.ShopCart.ListPage = {
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
                url: '/OrderManager/ShopCart/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'OrderNo', title: '订单号', width: 100 },
         { field: 'ProductCategoryId', title: '商品分类', width: 100 },
         { field: 'ProductId', title: '产品主键', width: 100 },
         { field: 'GoodsCode', title: '商品代码', width: 100 },
         { field: 'GoodsId', title: '商品主键', width: 100 },
         { field: 'Price', title: '商品原价_decimal_', width: 100 },
         { field: 'PriceSubDiscount', title: '购买价_decimal_', width: 100 },
         { field: 'TotalAmount', title: '单项小计_decimal_', width: 100 },
         { field: 'ProductWeight', title: '商品重量', width: 100 },
         { field: 'SpecName', title: '规格汇总', width: 100 },
         { field: 'CustomerId', title: '', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/OrderManager/ShopCart/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/OrderManager/ShopCart/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/OrderManager/ShopCart/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.ShopCart.ListPage.initPage();
});


