var pro = pro || {};
(function () {
    pro.SystemCategory = pro.SystemCategory || {};
    pro.SystemCategory.HdPage = pro.SystemCategory.HdPage || {};
    pro.SystemCategory.HdPage = {
        init: function () {
            return {
                gridObj: new pro.GridBase("#datagrid", false),
                gridObj2: new pro.GridBase("#datagrid2", false)

            };
        },
        initPage: function () {
            var specHtml = $("#SpecHtml").val();
            var attributeHtml = $("#AttributeHtml").val();

            var initObj = this.init();
            var gridObj = initObj.gridObj;
            var gridObj2 = initObj.gridObj2;


            /////////////
            gridObj.grid({
                url: '/ProductManager/SystemCategory/GetSystemCategoryAttributeList?SystemCategoryId=' + pro.commonKit.getUrlParam("PkId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', title: '', hidden: true, width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("A_PkId", value);
                            }
                        },
                        {
                            field: 'AttributeId',
                            title: '属性名',
                            width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("A_AttributeId_" + row.PkId, value, attributeHtml);
                            }
                        },
                         {
                             field: 'Sort',
                             title: '排序',
                             width: 100,
                             formatter: function (value, row, index) {
                                 return pro.controlKit.getInputHtml("A_Sort_" + row.PkId, value);
                             }
                         }

                    ]
                ],
                pagination: false
            }
           );

            //if ($("#ExtAttributeList").val() != "") {
            //    gridObj.grid('loadData', JSON.parse($("#ExtAttributeList").val()));
            //}
          


            $("#btnAdd_ToolBar").click(function () {
                gridObj.insertRow({
                    PkId: gridObj.PkId,
                    FunctionDetailCode: ""
                });

                //console.log(JSON.stringify($("#datagrid").datagrid('getRows')));
                //console.log(gridObj.PkId + 1);

                $("#datagrid").datagrid('selectRecord', gridObj.PkId + 1);
            });

            $("#btnDel_ToolBar").click(function () {
                gridObj.delRow();

            });

            /////////////
            gridObj2.grid({
                url: '/ProductManager/SystemCategory/GetSystemCategorySpecList?SystemCategoryId=' + pro.commonKit.getUrlParam("PkId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', hidden: true, title: '规格Id', width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("S_PkId", value);
                            }
                        },
                        {
                            field: 'SpecId', title: '规格名称', width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("S_SpecId_" + row.PkId, value,specHtml);
                            }
                        },
                        {
                            field: 'Sort', title: '排序',width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("S_Sort_" + row.PkId, value);
                            }
                        }

                    ]
                ],
                pagination: false
            }
        );
            $("#btnAdd2_ToolBar").click(function () {
                gridObj2.insertRow({
                    PkId: gridObj.PkId,
                    FunctionDetailCode: ""
                });

                //console.log(JSON.stringify($("#datagrid").datagrid('getRows')));
                //console.log(gridObj.PkId + 1);

                $("#datagrid2").datagrid('selectRecord', gridObj.PkId + 1);
            });


            $("#btnDel2_ToolBar").click(function () {
                gridObj2.delRow();

            });

            //if ($("#SpecList").val() != "") {
            //    gridObj2.grid('loadData', JSON.parse($("#SpecList").val()));
            //}

            /////////////////////////
            $("#btnAdd").click(function () {
                pro.SystemCategory.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.SystemCategory.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.SystemCategory.ListPage.closeTab("");
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }
        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();

            pro.submitKit.config.columnPkidName = "A_PkId";
            pro.submitKit.config.columnNamePreStr = "A_";
            pro.submitKit.config.columns = ["AttributeId", "Sort"];
            postData.RequestEntity.SystemCategoryAttributeList = pro.submitKit.getRowJson();

            pro.submitKit.config.columnPkidName = "S_PkId";
            pro.submitKit.config.columnNamePreStr = "S_";
            pro.submitKit.config.columns = ["SpecId", "Sort"];
            postData.RequestEntity.SystemCategorySpecList = pro.submitKit.getRowJson();


            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/ProductManager/SystemCategory/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.SystemCategory.ListPage.closeTab();
                    }
                    parent.$.alertExtend.info("", afterSuccess());
                }
            ).fail(
             function (errordetails, errormessage) {
                 //  $.alertExtend.error();
             }
            );

        },
        submitExtend: {
            addRule: function () {
                $("#form1").validate({
                    rules: {

                        SystemCategoryName: { required: true }
                       // Sort: { required: true }
                       
                    },
                    messages: {
                        PkId: "必填!",
                        SystemCategoryName: "分类名称必填!",
                        Sort: "排序必填!",
                        CreatorUserCode: "创建人必填!",
                        CreationTime: "创建时间必填!",
                        LastModifierUserCode: "修改人必填!",
                        LastModificationTime: "修改时间必填!",
                        IsDeleted: "是否删除必填!",
                        DeleterUserCode: "删除人必填!",
                        DeletionTime: "删除时间必填!",
                    },
                    errorPlacement: function (error, element) {
                        pro.commonKit.errorPlacementHd(error, element);
                    },
                    debug: false
                });
            },
            logicValidate: function () {
                return true;
            }
        },

        addTab: function (subtitle, url) {

        }

    };
})();



$(function () {
    pro.SystemCategory.HdPage.initPage();
});


