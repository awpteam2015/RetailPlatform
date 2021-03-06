﻿
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
                url: '/RiverManager/RiverProblemApply/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                frozenColumns: [[
        { field: 'Title', title: '标题', width: 100 },
        { field: 'Des', title: '问题描述', width: 200 },
        { field: 'Attr_StateStr', title: '问题状态', width: 100 },
        { field: 'Attr_IsExposure', title: '是否曝光', width: 100 },
        { field: 'Attr_ExposureLever', title: '曝光等级', width: 100 }
                ]],
                columns: [[
        { field: 'Attr_ProblemTypeStr', title: '问题类型', width: 100 },
         { field: 'DepartmentName', title: '所属部门', width: 200 },
         { field: 'RiverName', title: '河流名称', width: 100 },
         { field: 'UserCode', title: '河长编码', width: 100 },
         { field: 'UserName', title: '河长名称', width: 100 },
          { field: 'UrgentRemark', title: '督办备注', width: 100 },
         { field: 'DepartmentRemark', title: '部门转发备注', width: 100 },
         { field: 'TopDepartmentRemark', title: '顶级部门批注', width: 100 },
         { field: 'FinishRemark', title: '河长结束问题备注', width: 100 },
         { field: 'ReturnRemark', title: '河长回退问题备注', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );



            $("#btnBg").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/BgHd?PkId=" + PkId, "曝光" + PkId);
            });


            $("#btnSearch").click(function () {
                gridObj.search();
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


