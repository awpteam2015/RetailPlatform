
var pro = pro || {};
(function () {
    pro.Activity = pro.Activity || {};
    pro.Activity.ListPage = pro.Activity.ListPage || {};
    pro.Activity.ListPage = {
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
                view: detailview,
                url: '/SalePromotionManager/Activity/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [
                    [
                        { field: 'PkId', title: '活动Id', width: 100 },
                        { field: 'Title', title: '促销主题', width: 300 },
                        { field: 'StartDate', title: '开始时间', width: 100 },
                        { field: 'EndDate', title: '结束时间', width: 100 },
                        { field: 'Attr_State', title: '状态', width: 100 }
                    ]
                ],
                onLoadSuccess: function (data) {
                    //去掉展开和关闭标签
                    $.each(data.rows, function (key, temp) {
                        if (temp.IsHasRule == "0") {
                            var index = $('#datagrid').datagrid('getRowIndex', temp);
                            var expander = $('#datagrid').datagrid('getExpander', index);
                            expander.removeClass('datagrid-row-expand');
                        }
                    });

                },
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40], //可以设置每页记录条数的列表,
                detailFormatter: function (index, row) {
                    return '<div style="padding:2px"><table   id="ddv-' + index + '"></table></div>';
                },
                onExpandRow: function (index, row) {
                    if (row.IsHasRule == "0") {
                        return false;
                    }

                    $('#ddv-' + index).datagrid({
                        url: '/SalePromotionManager/Rule/GetList?ActivityId=' + row.PkId,
                        fitColumns: true,
                        singleSelect: true,
                        height: 'auto',
                        width: 'auto',
                        columns: [
                            [
                                {
                                    field: 'PkId',
                                    title: '操作',
                                    width: 30,
                                    formatter: function (value, row) {
                                        return '<input onclick="pro.listKit.onSelCheck(\'' + row.PkId + '\')" type="checkbox" name="PkId" value="' + row.PkId + '" forgrid="' + index + '"/>';
                                    }
                                },
                                  { field: 'ActivityId', title: '活动Id', width: 100 },
                                      { field: 'Title', title: '规则标题', width: 300 },
                                  { field: 'RuleType', title: '规则类型A B C', width: 100 }

                            ]
                        ],
                        onResize: function () {
                            $('#datagrid').datagrid('fixDetailRowHeight', index);
                        },
                        onLoadSuccess: function () {
                            setTimeout(function () {
                                $('#datagrid').datagrid('fixDetailRowHeight', index);
                            }, 0);
                        },
                        onBeforeLoad: function () {

                        }
                    });

                }
            }
            );

            $("#btnAddActivity").click(function () {
                tabObj.add("/SalePromotionManager/Activity/Hd", "新增活动");
            });

            $("#btnEditActivity").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/SalePromotionManager/Activity/Hd?PkId=" + PkId, "编辑活动" + PkId);
            });

            $("#btnDelActivity").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/SalePromotionManager/Activity/Delete?PkId=" + gridObj.getSelectedRow().PkId
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



            $("#btnAddRule").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/SalePromotionManager/Rule/RuleTypeList?ActivityId=" + PkId, "新增规则");

            });

            $("#btnEditRule").click(function () {
                if (pro.listKit.getSelData() == "") {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = pro.listKit.getSelData().PkId;
                var RuleType = pro.listKit.getSelData().RuleType;
                tabObj.add("/SalePromotionManager/Rule/Rule" + RuleType + "Hd?PkId=" + PkId, "编辑规则" + PkId);
            });

            $("#btnDelRule").click(function () {
                if (pro.listKit.getSelData() == "") {
                    $.alertExtend.infoOp();
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/SalePromotionManager/Rule/Delete?PkId=" + pro.listKit.getSelData().PkId
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





            $("#btnSearch").click(function () {
                gridObj.search();
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
    pro.Activity.ListPage.initPage();
});


