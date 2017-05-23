
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
            var url = "GetList";
            var command = "";
            if (($("#Command").length > 0)) {
                command = $("#Command").val();
                switch (command) {
                    case "zf":
                        url = "GetZfList";
                        break;
                    case "finish":
                        url = "GetFinishList";
                        break;
                    case "bg":
                        url = "GetBgList";
                        break;
                    case "zfcs":
                        url = "GetZfCsList";
                        break;
                    case "dbcs":
                        url = "GetDbCsList";
                        break;
                    case "dbfinish":
                        url = "GetDbFinishList";
                        break;
                    default:
                        break;
                }
            }
            var initObj = this.init();
            var tabObj = initObj.tabObj;
            var gridObj = initObj.gridObj;
            gridObj.grid({
                url: '/RiverManager/RiverProblemApply/' + url,
                fitColumns: false,
                nowrap: true,
                rownumbers: true, //行号
                singleSelect: true,
                frozenColumns: [[
            { field: 'Attr_ProblemTypeStr', title: '问题类型', width: 100 },
            { field: 'Attr_StateStr', title: '问题状态', width: 100 },
                { field: 'Attr_DbState', title: '督办状态', width: 100 },
            { field: 'RiverName', title: '河道名称', width: 100 },
            { field: 'Attr_IsExposure', title: '是否曝光', width: 100, hidden: true },
            { field: 'Attr_ExposureLever', title: '曝光等级', width: 100, hidden: true },
            { field: 'Attr_IsUrgent', title: '是否督办', width: 70, hidden: true },
            { field: 'Attr_IsSendMessage', title: '是否发送短信', width: 70, hidden: true }

                ]],
                columns: [[
             { field: 'Attr_IsDeal', title: '是否处理', width: 100 },
               { field: 'Attr_IsMark', title: '标识', width: 100 },
            { field: 'ApplyManName', title: '上报人', width: 100 },
            { field: 'ApplyManTel', title: '联系方式', width: 100 },
            { field: 'Title', title: '地址描述', width: 100 },
            { field: 'Des', title: '问题描述', width: 200 },
            { field: 'DepartmentName', title: '所属部门', width: 200 },
            //{ field: 'UserCode', title: '河长手机', width: 100 },
            { field: 'UserName', title: '责任河长', width: 100 },
            { field: 'UrgentRemark', title: '督办意见', width: 100 },
            { field: 'DepartmentRemark', title: '部门处理意见', width: 100 },
            { field: 'TopDepartmentRemark', title: '治水办处理意见', width: 100 },
            { field: 'FinishRemark', title: '处理结果', width: 100 },
            { field: 'ReturnRemark', title: '回退说明', width: 100 },
            { field: 'CreationTime', title: '上报时间', width: 100 }
                ]],
                rowStyler: function (index, row) {
                    if (row.DifferDay>7) {
                        return 'background-color:red;';
                    } else if(row.DifferDay>5) {
                        return 'background-color:yellow;';
                    }
                },
                onBeforeLoad: function () {
                    switch (command) {
                        case "db":
                            gridObj.grid("showColumn", "Attr_IsUrgent");
                            gridObj.grid("showColumn", "Attr_IsSendMessage");
                            break;
                        case "bg":
                            gridObj.grid("showColumn", "Attr_IsExposure");
                            gridObj.grid("showColumn", "Attr_ExposureLever");
                            break;
                        case "dbfinish":
                            gridObj.grid("showColumn", "Attr_DbState");
                            break;
                        default:
                    }
                },
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
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                tabObj.add("/RiverManager/RiverProblemApply/BgHd?PkId=" + PkId, "曝光" + title);
            });



            $("#btnDb").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                tabObj.add("/RiverManager/RiverProblemApply/DbHd?PkId=" + PkId, "督办" + title);
            });

            $("#btnAdd").click(function () {
                tabObj.add("/RiverManager/RiverProblemApply/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                tabObj.add("/RiverManager/RiverProblemApply/Hd?PkId=" + PkId, "编辑" + title);
            });

            $("#btnZf").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var row = gridObj.getSelectedRow();
                if (row.State != "1" && row.State != "5") {
                    $.alertExtend.infoOp();
                    return;
                }
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                tabObj.add("/RiverManager/RiverProblemApply/ZfHd?PkId=" + PkId, "转发" + title);
            });

            $("#btnPs").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                tabObj.add("/RiverManager/RiverProblemApply/PsHd?PkId=" + PkId, "批示" + title);
            });

            $("#btnReturn").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                var row = gridObj.getSelectedRow();
                if (row.State != "2") {
                    $.alertExtend.infoOp();
                    return;
                }

                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/ReturnHd?PkId=" + PkId, "回退" + title);
            });

            $("#btnFinish").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                var row = gridObj.getSelectedRow();
                if (row.State != "2") {
                    $.alertExtend.infoOp();
                    return;
                }
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/FinishHd?PkId=" + PkId, "完结" + title);
            });


            $("#btnDbReturn").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                var row = gridObj.getSelectedRow();
                //if (row.State != "2") {
                //    $.alertExtend.infoOp();
                //    return;
                //}

                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/DbReturnHd?PkId=" + PkId, "督办回退" + title);
            });

            $("#btnDbFinish").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }

                var row = gridObj.getSelectedRow();
                //if (row.State != "2") {
                //    $.alertExtend.infoOp();
                //    return;
                //}
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/DbFinishHd?PkId=" + PkId, "督办完结" + title);
            });




            $("#btnViewDetail").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/ViewHd?PkId=" + PkId, "查看" + title);
            });


            $("#btnSearch").click(function () {
                gridObj.search();
            });

            $("#btnDel").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var title = gridObj.getSelectedRow().ApplyManName + "" + gridObj.getSelectedRow().Attr_ProblemTypeStr;
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverProblemApply/DelHd?PkId=" + PkId, "删除" + title);
            });

            $("#btnDeal").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                abp.ajax({
                    url: "/RiverManager/RiverProblemApply/Deal?PkId=" + gridObj.getSelectedRow().PkId
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

            $("#btnMark").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                abp.ajax({
                    url: "/RiverManager/RiverProblemApply/Mark?PkId=" + gridObj.getSelectedRow().PkId
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


