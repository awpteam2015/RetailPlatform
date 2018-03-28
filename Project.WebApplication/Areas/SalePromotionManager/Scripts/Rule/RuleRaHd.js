var pro = pro || {};
(function () {
    pro.Rule = pro.Rule || {};
    pro.Rule.RuleRaHdPage = pro.Rule.RuleRaHdPage || {};

    pro.Rule.RuleRaHdPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false)
            };
        },
        gridMethod:{
            changeGoodsCode: function (i) {

                var goodsCode = $("input[name=GoodsCode_" + i + "]").val();

                abp.ajax({
                    url: "/ProductManager/Goods/GetGoodsInfo?GoodsCode=" + goodsCode
                }).done(
                    function(dataresult, data) {

                        $("[name=ProductName_span_" + i + "]").html(dataresult.ProductInfo.ProductName);
                        $("[name=Price_span_" + i + "]").html(dataresult.ProductInfo.SellPrice);
                        $("[name=SpecDetail_span_" + i + "]").html(dataresult.SpecDetail);
                        $("[name=ProductId_" + i + "]").val(dataresult.ProductId);

                    }
                ).fail(
                    function (errordetails, errormessage) {

                        $("input[name=GoodsCode_" + i + "]").val("");
                    }
                );


            }
        },
        initPage: function () {
            var initObj = this.init();
            var tabObj = initObj.tabObj;
            var gridObj = initObj.gridObj;
            gridObj.grid({
                url: '/SalePromotionManager/Rule/GetPromotionGoodsList?RuleId=' + pro.commonKit.getUrlParam("PkId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', title: '', hidden: true, width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("PkId", value);
                            }
                        },
                        {
                            field: 'GoodsCode',
                            title: '商品编码',
                            width: 100,
                            formatter: function (value, row, index) {
                                return '<input onchange="pro.Rule.RuleRaHdPage.gridMethod.changeGoodsCode('+row.PkId+')" name="GoodsCode_' + row.PkId + '" value="' + value + '"  type="text" style="width:200px" />';
                            }
                        },
                        {
                            field: 'ProductName',
                            title: '商品名称',
                            width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSpanHtml("ProductName_" + row.PkId, value);
                            }
                        },
                         {
                             field: 'ProductId',
                             title: '产品系统编号',
                             hidden:true,
                             width: 200,
                             formatter: function (value, row, index) {
                                 return pro.controlKit.getInputHtml("ProductId_" + row.PkId, value);
                             }
                         },
                        {
                            field: 'Price', title: '单价', width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSpanHtml("Price_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'PromotionPrice', title: '促销价', width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("PromotionPrice_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'SpecDetail',
                            title: '规格',
                            width: 300,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSpanHtml("SpecDetail_" + row.PkId, value, 300);
                            }
                        }
                    ]
                ],
                pagination: false
            }
            );

            $("#btnAdd_ToolBar").click(function () {
                gridObj.insertRow({
                    PkId: gridObj.PkId,
                    GoodsCode: ""
                });

                //console.log(JSON.stringify($("#datagrid").datagrid('getRows')));
                //console.log(gridObj.PkId + 1);

                $("#datagrid").datagrid('selectRecord', gridObj.PkId + 1);
            });


            $("#btnDel_ToolBar").click(function () {
                gridObj.delRow();
            });



            $("#btnAdd").click(function () {
                pro.Rule.RuleRaHdPage.submit("RuleRaAdd");
            });

            $("#btnEdit").click(function () {
                pro.Rule.RuleRaHdPage.submit("RuleRaEdit");
            });

            $("#btnClose").click(function () {
                parent.pro.Activity.ListPage.closeTab();
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }

                $("input[name=StartMoney]").val(bindEntity.RuleSendTicketEntity.StartMoney);
                $("input[name=EndMoney]").val(bindEntity.RuleSendTicketEntity.EndMoney);
                $("input[name=TicketNum]").val(bindEntity.RuleSendTicketEntity.TicketNum);
                $("input[name=TicketAvaildateStart]").val(bindEntity.RuleSendTicketEntity.TicketAvaildateStart);
                $("input[name=TicketAvaildateEnd]").val(bindEntity.RuleSendTicketEntity.TicketAvaildateEnd);

                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }
        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();

            pro.submitKit.config.columnPkidName = "PkId";
            pro.submitKit.config.columns = ["GoodsCode", "PromotionPrice", "ProductId"];
            postData.RequestEntity.RulePromotionGoodsEntityList = pro.submitKit.getRowJson();


            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            } else {
                postData.RequestEntity.ActivityId = pro.commonKit.getUrlParam("ActivityId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/SalePromotionManager/Rule/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Activity.ListPage.closeTab();
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
                        //PkId: { required: true  },
                        //ActivityId: { required: true  },
                        //RuleType: { required: true  },
                        Title: { required: true },
                        StartMoney: { required: true },
                        EndMoney: { required: true },
                        TicketNum: { required: true },
                        CardTypeIds: { required: true },
                        TicketAvaildateEnd: { required: true },
                        TicketAvaildateStart: { required: true }

                    },
                    messages: {
                        Title: "必填!",
                        StartMoney: "订单金额范围必填!",
                        EndMoney: "订单金额范围必填!",
                        TicketNum: "券张数必填!",
                        CardTypeIds: "会员可类型范围必填!",
                        TicketAvaildateEnd: "有效时间结束必填!",
                        TicketAvaildateStart: "有效时间开始必填!"
                    },
                    errorPlacement: function (error, element) {
                        pro.commonKit.errorPlacementRuleRaHd(error, element);
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
    pro.Rule.RuleRaHdPage.initPage();
});


