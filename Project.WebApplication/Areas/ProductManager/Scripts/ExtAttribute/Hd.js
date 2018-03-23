var pro = pro || {};
(function () {
    pro.ExtAttribute = pro.ExtAttribute || {};
    pro.ExtAttribute.HdPage = pro.ExtAttribute.HdPage || {};
    pro.ExtAttribute.HdPage = {
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
                url: '/ProductManager/ExtAttribute/GetAttributeValueList?AttributeId=' + pro.commonKit.getUrlParam("PkId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        { field: 'PkId', title: '', width: 100 },
         { field: 'OrderNo', title: '订单号', width: 100 },
         { field: 'ProductCategoryId', title: '商品分类', width: 100 },
         { field: 'ProductId', title: '产品主键', width: 100 },
         { field: 'GoodsCode', title: '商品代码', width: 100 },
         { field: 'GoodsId', title: '商品主键', width: 100 },
         { field: 'Price', title: '商品原价_decimal_', width: 100 },
         { field: 'PriceSubDiscount', title: '购买价_decimal_', width: 100 },
         { field: 'TotalAmount', title: '单项小计_decimal_', width: 100 },
         { field: 'ProductWeight', title: '商品重量', width: 100 },
         { field: 'SpecName', title: '规格汇总', width: 100 }

                    ]
                ],
                pagination: false
            }
            );

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


            $('#ShowType').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/SystemSetManager/Dictionary/GetList_Combobox?ParentKeyCode=AttributeShowType',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#ShowType").combobox('setValue', bindEntity['ShowType']);
                    }
                }
            });



            $("#btnAdd").click(function () {
                pro.ExtAttribute.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.ExtAttribute.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.ExtAttribute.ListPage.closeTab("");
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
            postData.RequestEntity.ShowTypeName = $("#ShowType").combobox('getText');


            pro.submitKit.config.columnPkidName = "PkId";
            pro.submitKit.config.columns = ["AttributeValueName", "Sort"];
            postData.RequestEntity.AttributeValueList = pro.submitKit.getRowJson();


            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/ProductManager/ExtAttribute/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.ExtAttribute.ListPage.closeTab();
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
          PkId: { required: true  },
          AttributeName: { required: true  },
          OtherName: { required: true  },
          ShowType: { required: true  },
          AttributeValues: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          AttributeName:  "必填!",
          OtherName:  "必填!",
          ShowType:  "表现方式 1 select 2input 3必填!",
          AttributeValues:  "必填!",
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
    pro.ExtAttribute.HdPage.initPage();
});


