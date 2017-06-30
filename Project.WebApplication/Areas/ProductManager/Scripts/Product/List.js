
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
         { field: 'PkId', title: '产品ID', width: 100 },
         { field: 'ProductName', title: '产品名称', width: 100 },
         { field: 'SystemCategoryId', title: '系统类型', width: 100 },
         { field: 'ProductCategoryId', title: '商品分类', width: 100 },
         { field: 'ProductCategoryRoute', title: '商品分类全路由', width: 100 },
         { field: 'BrandId', title: '品牌ID', width: 100 },
         { field: 'ProductCode', title: '商品编号', width: 100 },
         { field: 'Unit', title: '计量单位', width: 100 },
         { field: 'BriefDescription', title: '简介', width: 100 },
         { field: 'Description', title: '详细描述', width: 100 },
         { field: 'Weight', title: '重量', width: 100 },
         { field: 'WeightUnit', title: '重量单位', width: 100 },
         { field: 'MarketPrice', title: '市场价', width: 100 },
         { field: 'SellPrice', title: '销售价', width: 100 },
         { field: 'Cost', title: '成本价', width: 100 },
         { field: 'PriceUnit', title: '货币单位', width: 100 },
         { field: 'StockNum', title: '库存数量', width: 100 },
         { field: 'BuyMaxNum', title: '最大数量限制', width: 100 },
         { field: 'BuyMinNum', title: '最小数量限制', width: 100 },
         { field: 'ViewNum', title: '访问次数', width: 100 },
         { field: 'CommentNum', title: '评论次数', width: 100 },
         { field: 'SelledNum', title: '售出数量', width: 100 },
         { field: 'Title', title: '页面标题', width: 100 },
         { field: 'MetaKeywords', title: '页面关键字', width: 100 },
         { field: 'MetaDescription', title: '页面描述', width: 100 },
         { field: 'IsShow', title: '是否上架', width: 100 },
         { field: 'IsCommand', title: '是否推荐', width: 100 },
         { field: 'PdtDesc', title: 'Pdt_desc', width: 100 },
         { field: 'SpecDesc', title: 'Spec_desc', width: 100 },
         { field: 'ParamsDesc', title: 'Params_desc', width: 100 },
         { field: 'TagsDesc', title: 'Tags_desc', width: 100 },
         { field: 'CreatorUserCode', title: '创建人', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'LastModifierUserCode', title: '修改人', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
         { field: 'IsDeleted', title: '是否删除', width: 100 },
         { field: 'DeleterUserCode', title: '删除人', width: 100 },
         { field: 'DeletionTime', title: '删除时间', width: 100 },
         { field: 'P1', title: '扩展属性1', width: 100 },
         { field: 'P2', title: '扩展属性2', width: 100 },
         { field: 'P3', title: '扩展属性1', width: 100 },
         { field: 'P4', title: '扩展属性2', width: 100 },
         { field: 'P5', title: '扩展属性1', width: 100 },
         { field: 'P6', title: '扩展属性2', width: 100 },
         { field: 'P7', title: '扩展属性1', width: 100 },
         { field: 'P8', title: '扩展属性2', width: 100 },
         { field: 'P9', title: '扩展属性1', width: 100 },
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


