
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
         { field: 'ProductCode', title: '产品编码', width: 100 },
         { field: 'ProductName', title: '产品名称', width: 100 },
         { field: 'SellPrice', title: '销售价', width: 100 },
         { field: 'StockNum', title: '库存', width: 100 },
          { field: 'Attr_IsCommand', title: '是否推荐', width: 100 },
          { field: 'Attr_IsShow', title: '是否上架', width: 100 },
         { field: 'SystemCategoryName', title: '商品类型', width: 150 },
         { field: 'ProductCategoryName', title: '商品分类', width: 150 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                var systemCategoryId = $("#SystemCategoryId2").combobox('getValue');
                if (systemCategoryId == "") {
                    $.alertExtend.infoOp("请选择商品所属商品类型进行发布！");
                    return;
                }

                tabObj.add("/ProductManager/Product/Hd?SystemCategoryId=" + systemCategoryId, "发布商品");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/ProductManager/Product/Hd?SystemCategoryId=" + gridObj.getSelectedRow().SystemCategoryId + "&PkId=" + PkId, "编辑" + PkId);
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


            pro.ProductcategoryControl.init({ controlId: "ProductCategoryId", required: false });


            $('#SystemCategoryId').combobox({
                required: false,
                editable: false,
                valueField: 'PkId',
                textField: 'SystemCategoryName',
                url: '/ProductManager/SystemCategory/GetList_Combobox',
                onLoadSuccess: function () {

                }
            });

            $('#SystemCategoryId2').combobox({
                required: false,
                editable: false,
                valueField: 'PkId',
                textField: 'SystemCategoryName',
                url: '/ProductManager/SystemCategory/GetList_Combobox',
                onLoadSuccess: function () {

                }
            });


            $("#btnDownPrd").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                abp.ajax({
                    url: "/ProductManager/Product/DownPrd?PkId=" + gridObj.getSelectedRow().PkId
                    //contentType: abp.ajax.contentTypeForm
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

            $("#btnUpPrd").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                abp.ajax({
                    url: "/ProductManager/Product/UpPrd?PkId=" + gridObj.getSelectedRow().PkId
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

            $("#btnCommandPrd").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                abp.ajax({
                    url: "/ProductManager/Product/CommandPrd?PkId=" + gridObj.getSelectedRow().PkId
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

            $("#btnCancelCommandPrd").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                abp.ajax({
                    url: "/ProductManager/Product/CancelCommandPrd?PkId=" + gridObj.getSelectedRow().PkId
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

        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.Product.ListPage.initPage();
});


