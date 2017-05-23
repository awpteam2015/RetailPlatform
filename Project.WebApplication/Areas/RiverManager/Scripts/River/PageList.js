
function choose(riverid, rivername) {
    //alert(riverid);
    parent.pro.RiverProblemApply.HdPage.setRelationInfo(riverid, null, rivername);
   // parent.$(#)
}


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
                url: '/RiverManager/River/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
  {
      field: 'Coords', title: '选择', width: 100, formatter: function (value, row) {
          return '<input type="button" value="选择" onclick="choose(\'' + row.PkId + '\',\'' + row.RiverName + '\')">';
      }
  },
         { field: 'RiverName', title: '河道名称', width: 100 },
         { field: 'DepartmentName', title: '归属部门', width: 150 },
         { field: 'RiverRank', title: '河道等级', width: 100 },
         { field: 'RiverStart', title: '河道起点', width: 100 },
           { field: 'RiverEnd', title: '河道终点', width: 100 },
         { field: 'RiverLength', title: '长度', width: 100 },
         { field: 'RiverCrossArea', title: '流经乡（镇）', width: 150 },

           { field: 'Att_UserCode', title: '河长手机', width: 100 },
             { field: 'Att_UserName', title: '河长姓名', width: 100 },

         //{ field: 'Coords', title: '坐标', width: 100 },
         //{ field: 'IsActive', title: '是否有效', width: 100 },
         //{ field: 'CreatorUserCode', title: '创建人', width: 100 },
         //{ field: 'CreationTime', title: '创建时间', width: 100 },
         //{ field: 'LastModifierUserCode', title: '修改人', width: 100 },
         //{ field: 'LastModificationTime', title: '修改时间', width: 100 },
         { field: 'Remark', title: '备注', width: 100 }
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
                tabObj.add("/RiverManager/River/SetRiverChief?PkId=" + PkId, "设置河长" + PkId);
            });

            $('#DepartmentCode').combotree({
                required: false,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
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


