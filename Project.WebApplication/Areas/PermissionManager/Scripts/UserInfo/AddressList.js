function onChangeSort(pkid) {
    abp.ajax({
        contentType:abp.ajax.contentTypeForm,
        url: "/PermissionManager/UserInfo/ChangeSort?PkId=" + pkid + "&Sort=" + $("#Sort_" + pkid).val()
    }).done(
                   function (dataresult, data) {
                       $.alertExtend.info();
                   }
                   ).fail(
                   function (errordetails, errormessage) {
                       $.alertExtend.error();
                   }
                   );
}


var pro = pro || {};
(function () {
    pro.UserInfo = pro.UserInfo || {};
    pro.UserInfo.ListPage = pro.UserInfo.ListPage || {};
    pro.UserInfo.ListPage = {
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
                url: '/PermissionManager/UserInfo/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         //{ field: 'UserCode', title: '登录账号', width: 100 },
         { field: 'UserName', title: '河长姓名', width: 100 },
         { field: 'Mobile', title: '手机号', width: 100 },
         //{ field: 'Tel', title: '家庭电话', width: 100 },
         { field: 'Duty', title: '职务', width: 100 },
         { field: 'Att_RiverName', title: '管理河道', width: 100 },
            {
                field: 'Sort', title: '排序', width: 100, formatter: function (value, row, index) {
                    return '<input id="Sort_' + row.PkId + '" value="' + value + '" onchange="onChangeSort(' + row.PkId + ')">';

                }
            }
         //{ field: 'IsActive', title: '是否有效', width: 100 },
         //{ field: 'CreatorUserCode', title: '创建人', width: 100 },
         //{ field: 'CreationTime', title: '创建时间', width: 100 },
         //{ field: 'LastModifierUserCode', title: '修改人', width: 100 },
         //{ field: 'LastModificationTime', title: '修改时间', width: 100 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/PermissionManager/UserInfo/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var userName = gridObj.getSelectedRow().UserName;
                tabObj.add("/PermissionManager/UserInfo/Hd?PkId=" + PkId, "编辑" + userName);
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
                        url: "/PermissionManager/UserInfo/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.UserInfo.ListPage.initPage();
});


