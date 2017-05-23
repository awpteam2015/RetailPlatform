
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
                url: '/RiverManager/RiverAttach/GetList2?RiverId=' + pro.commonKit.getUrlParam("RiverId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
            { field: 'RiverName', title: '河道名称', width: 100 },
            { field: 'RiverChief', title: '责任河长', width: 100 },
            { field: 'RiverArea', title: '流域区域', width: 100 },
            { field: 'PointName', title: '点位名称', width: 100 },
            { field: 'Month', title: '监测月', width: 100 },
            { field: 'Day', title: '监测日', width: 100 },
            { field: 'RiverFlow', title: '流向', width: 100 },
            { field: 'Zb1', title: '高锰酸盐指数(mg/L)', width: 100 },
            { field: 'Zb2', title: '氨氮(mg/L)', width: 100 },
            { field: 'Zb3', title: '总磷(mg/L)', width: 100 },
            { field: 'WaterQualityRank', title: '水质类别', width: 100 },
            { field: 'Pointer', title: '定类指标', width: 100 }

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


