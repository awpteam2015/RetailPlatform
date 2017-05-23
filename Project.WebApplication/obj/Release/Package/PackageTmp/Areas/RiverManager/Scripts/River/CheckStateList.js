
var pro = pro || {};
(function () {
    pro.River = pro.River || {};
    pro.River.ListPage = pro.River.ListPage || {};
    pro.River.ListPage = {
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
                //url: '/RiverManager/River/GetCheckStateList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[

         { field: 'UserName', title: '河长姓名', width: 100 },
         { field: 'Jb', title: '河长级别', width: 100 },
         { field: 'Times', title: '巡河情况', width: 100 },
         { field: 'State', title: '状态', width: 100 }
 
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/RiverManager/River/Hd", "新增");
            });

            $("#btnView").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var entity = gridObj.getSelectedRow();
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/River/ViewHd?PkId=" + PkId, "查看" + entity.RiverName);
            });


            $("#btnViewQd").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var entity = gridObj.getSelectedRow();
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverCheck/ViewList?RiverId=" + PkId, "查看" + entity.RiverName + "河长签到");
            });

            $("#btnViewHz").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var entity = gridObj.getSelectedRow();
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/PermissionManager/UserInfo/ViewList?UserCodes=" + gridObj.getSelectedRow().Att_UserCode, "查看" + entity.Att_UserName + "河长信息");
            });

            $('#RiverRank').combobox({
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=RiverRank'
            });


            $("#btnViewSz").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var entity = gridObj.getSelectedRow();
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverAttach/ViewList?RiverId=" + PkId, "查看" + entity.RiverName + "水质信息");
            });





            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var entity = gridObj.getSelectedRow();
                tabObj.add("/RiverManager/River/Hd?PkId=" + PkId, "编辑" + entity.RiverName);
            });

            $("#btnSetRiverChief").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var entity = gridObj.getSelectedRow();
                tabObj.add("/RiverManager/River/SetRiverChief?PkId=" + PkId, "设置河长" + entity.RiverName+"河长");
            });

            $('#DepartmentCode').combotree({
                required: false,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            });


            $("#btnSearch").click(function () {
                if ($("#RiverName").val() == "" || $("#BeginDate").val() == "" || $("#EndDate").val() == "") {
                    
                    $.alertExtend.infoOp("河道名称，开始时间，结束时间必填！");
                    return;
                }


                gridObj.search("/RiverManager/River/GetCheckStateList");
            });

            $('#btnExport').click(function () {
                if ($("#RiverName").val() == "" || $("#BeginDate").val() == "" || $("#EndDate").val() == "") {

                    $.alertExtend.infoOp("河道名称，开始时间，结束时间必填！");
                    return;
                }

                var urlParam = pro.commonKit.parseParam(gridObj.searchForm());
                location.href = "/RiverManager/River/ExportReport1?" + urlParam;
            });



            $("#btnDel").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/RiverManager/River/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.River.ListPage.initPage();
});


