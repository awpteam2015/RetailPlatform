
var pro = pro || {};
(function () {
    pro.RulePromotionGoods = pro.RulePromotionGoods || {};
    pro.RulePromotionGoods.ListPage = pro.RulePromotionGoods.ListPage || {};
    pro.RulePromotionGoods.ListPage = {
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
                url: '/SalePromotionManager/RulePromotionGoods/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'ActivityId', title: '活动Id', width: 100 },
         { field: 'RuleId', title: '活动规则Id', width: 100 },
         { field: 'ProductId', title: '产品主键', width: 100 },
         { field: 'ProductCode', title: '产品编码', width: 100 },
         { field: 'GoodsCode', title: '商品代码', width: 100 },
         { field: 'GoodsId', title: '商品主键', width: 100 },
         { field: 'Price', title: '单价', width: 100 },
         { field: 'PromotionPrice', title: '促销价', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/SalePromotionManager/RulePromotionGoods/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/SalePromotionManager/RulePromotionGoods/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/SalePromotionManager/RulePromotionGoods/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.RulePromotionGoods.ListPage.initPage();
});


