
var pro = pro || {};
(function () {
    pro.RiverProblemApply = pro.RiverProblemApply || {};
    pro.RiverProblemApply.ListPage = pro.RiverProblemApply.ListPage || {};
    pro.RiverProblemApply.ListPage = {
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
                url: '/RiverManager/RiverProblemApply/' + $("#urlCommand").length>0? $("#urlCommand").val():"List",
                fitColumns: false,
                nowrap: true,
                rownumbers: true, //行号
                singleSelect: true,
                frozenColumns: [[
          { field: 'Title', title: '标题', width: 100 },
          { field: 'Des', title: '问题描述', width: 200 },
          { field: 'Attr_StateStr', title: '问题状态', width: 100 }
                ]],
                columns: [[
         { field: 'Title', title: '标题', width: 100 },
         { field: 'Des', title: '问题描述', width: 200 },
         { field: 'Attr_ProblemTypeStr', title: '问题类型', width: 100 },
         { field: 'DepartmentName', title: '所属部门', width: 200 },
         { field: 'RiverName', title: '河流名称', width: 100 },
         { field: 'UserCode', title: '河长编码', width: 100 },
         { field: 'UserName', title: '河长名称', width: 100 },
         { field: 'DepartmentRemark', title: '部门转发备注', width: 100 },
         { field: 'TopDepartmentRemark', title: '顶级部门批注', width: 100 },
         { field: 'FinishRemark', title: '河长结束问题备注', width: 100 },
         { field: 'ReturnRemark', title: '河长回退问题备注', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/RiverManager/RiverProblemApply/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/Hd?PkId=" + PkId, "编辑" + PkId);
            });

            $("#btnZf").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/ZfHd?PkId=" + PkId, "转发" + PkId);
            });

            $("#btnPs").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/PsHd?PkId=" + PkId, "批示" + PkId);
            });

            $("#btnReturn").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/ReturnHd?PkId=" + PkId, "回退" + PkId);
            });

            $("#btnFinish").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/FinishHd?PkId=" + PkId, "完结" + PkId);
            });


            $("#btnViewDetail").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/ViewHd?PkId=" + PkId, "查看" + PkId);
            });


            $("#btnSearch").click(function () {
                gridObj.search();
            });

            $("#btnDel").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/DelHd?PkId=" + PkId, "删除" + PkId);
            });

            $("#btnRefresh").click(function () {
                gridObj.refresh();
            });

            $('#DepartmentCode').combotree({
                required: false,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            });
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.RiverProblemApply.ListPage.initPage();
});


