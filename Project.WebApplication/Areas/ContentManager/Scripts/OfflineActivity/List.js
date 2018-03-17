
var pro = pro || {};
(function () {
    pro.OfflineActivity = pro.OfflineActivity || {};
    pro.OfflineActivity.ListPage = pro.OfflineActivity.ListPage || {};
    pro.OfflineActivity.ListPage = {
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
                url: '/ContentManager/OfflineActivity/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'Tttle', title: '活动标题', width: 100 },
         { field: 'OfflineActivityAddress', title: '活动地址', width: 100 },
         { field: 'StartDate', title: '开始时间', width: 100 },
         { field: 'EndDate', title: '结束时间', width: 100 },
         { field: 'ImageUrl', title: '图片', width: 100 },
         { field: 'BriefDescription', title: '简介', width: 100 },
         { field: 'State', title: '活动状态', width: 100 },
         { field: 'DeletionTime', title: '删除时间', width: 100 },
         { field: 'DeleterUserCode', title: '删除人', width: 100 },
         { field: 'IsDeleted', title: '是否删除', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
         { field: 'LastModifierUserCode', title: '修改人', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'CreatorUserCode', title: '创建人', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/ContentManager/OfflineActivity/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/ContentManager/OfflineActivity/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/ContentManager/OfflineActivity/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.OfflineActivity.ListPage.initPage();
});


