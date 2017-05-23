
var pro = pro || {};
(function () {
    pro.RiverAttach = pro.RiverAttach || {};
    pro.RiverAttach.ListPage = pro.RiverAttach.ListPage || {};
    pro.RiverAttach.ListPage = {
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
                url: '/RiverManager/RiverAttach/GetList?RiverId=' + pro.commonKit.getUrlParam("RiverId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'Att_RecordTime', title: '记录月份', width: 100 },
         { field: 'RiverName', title: '河道名称', width: 100 },
         { field: 'WaterQualityRank', title: '水质类别', width: 100 },
                    {
                        field: 'WaterQualityChange', title: '水质变化', width: 100, formatter: function (value,row) {
                            if (value == "2") {
                                return '<img src="/Resources/ng.png">';
                            } else {
                                return '<img src="/Resources/gx.png">';
                            }
                        }
                    }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/RiverManager/RiverAttach/Hd", "新增");
            });

  

            $("#btnExportIn").click(function () {
                tabObj.add("/RiverManager/RiverAttach/Upload", "导入");
            });


            $('#btnExport').click(function () {
                location.href = "/RiverManager/RiverAttach/ExportReport";
            });


            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverAttach/Hd?PkId=" + PkId, "编辑" + PkId);
            });



            $("#btnView").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/RiverManager/RiverAttach/ViewHd?PkId=" + PkId, "查看" + PkId);
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
                        url: "/RiverManager/RiverAttach/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.RiverAttach.ListPage.initPage();
});


