
var pro = pro || {};
(function () {
    pro.Spec = pro.Spec || {};
    pro.Spec.ListPage = pro.Spec.ListPage || {};
    pro.Spec.ListPage = {
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
                url: '/ProductManager/Spec/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '序号', width: 100 },
         { field: 'SpecName', title: '规格名称', width: 100 },
         { field: 'Remark', title: '备注', width: 100 },
         { field: 'SpecTypeName', title: '规格类型', width: 100 },
         { field: 'ShowTypeName', title: '展现方式', width: 100 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/ProductManager/Spec/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/ProductManager/Spec/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/ProductManager/Spec/Delete?PkId=" + gridObj.getSelectedRow().PkId
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


           

            $('#SpecType').combobox({
                required: false,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/SystemSetManager/Dictionary/GetList_Combobox?ParentKeyCode=SpecType',
                onLoadSuccess: function () {
                    //if (pro.commonKit.getUrlParam("PkId") > 0) {
                    //    $("#WorkState").combobox('setValue', bindEntity['WorkState']);
                    //}
                }
            });


            $('#ShowType').combobox({
                required: false,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/SystemSetManager/Dictionary/GetList_Combobox?ParentKeyCode=ShowType',
                onLoadSuccess: function () {
                    //if (pro.commonKit.getUrlParam("PkId") > 0) {
                    //    $("#WorkState").combobox('setValue', bindEntity['WorkState']);
                    //}
                }
            });


        },
         closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.Spec.ListPage.initPage();
});


