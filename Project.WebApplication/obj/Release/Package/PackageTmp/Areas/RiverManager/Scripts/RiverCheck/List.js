
var pro = pro || {};
(function () {
    pro.RiverCheck = pro.RiverCheck || {};
    pro.RiverCheck.ListPage = pro.RiverCheck.ListPage || {};
    pro.RiverCheck.ListPage = {
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
                url: '/RiverManager/RiverCheck/GetList?RiverId=' + pro.commonKit.getUrlParam("RiverId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[

         { field: 'RiverName', title: '河道名称', width: 100 },
         { field: 'UserName', title: '河长姓名', width: 100 },
         { field: 'UserCode', title: '河长手机', width: 100 },
                  { field: 'Attr_RiverDepartmentName', title: '所属部门', width: 100 },
                           { field: 'Attr_Lever', title: '河长级别', width: 100 },
         { field: 'Remark', title: '巡河描述', width: 300 },
         { field: 'CreationTime', title: '巡河时间', width: 100 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );




            $("#btnAdd").click(function () {
                tabObj.add("/RiverManager/RiverCheck/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var entity = gridObj.getSelectedRow();
                var title = entity.UserName + entity.Att_CreationTime + "巡河";
                tabObj.add("/RiverManager/RiverCheck/Hd?PkId=" + PkId, "编辑" + title);
            });

            $("#btnView").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var entity = gridObj.getSelectedRow();
                var title = entity.UserName + entity.Att_CreationTime + "巡河";
                tabObj.add("/RiverManager/RiverCheck/ViewHd?PkId=" + PkId, "查看" + title);
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
                        url: "/RiverManager/RiverCheck/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.RiverCheck.ListPage.initPage();
});


