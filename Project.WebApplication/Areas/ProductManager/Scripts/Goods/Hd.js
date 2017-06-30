var pro = pro || {};
(function () {
    pro.Goods = pro.Goods || {};
    pro.Goods.HdPage = pro.Goods.HdPage || {};
    pro.Goods.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Goods.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Goods.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Goods.ListPage.closeTab("");
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

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/ProductManager/Goods/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Goods.ListPage.closeTab();
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
          ProductId: { required: true  },
          GoodsCode: { required: true  },
          GoodsStock: { required: true  },
          GoodsPrice: { required: true  },
          GoodsCost: { required: true  },
          GoodsWeight: { required: true  },
          GoodsWeightUnit: { required: true  },
          Unit: { required: true  },
          Title: { required: true  },
          IsDefault: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          ProductId:  "必填!",
          GoodsCode:  "货号必填!",
          GoodsStock:  "库存必填!",
          GoodsPrice:  "销售价必填!",
          GoodsCost:  "成本价必填!",
          GoodsWeight:  "重量必填!",
          GoodsWeightUnit:  "重量单位必填!",
          Unit:  "单位必填!",
          Title:  "商品描述必填!",
          IsDefault:  "是否是默认商品必填!",
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
    pro.Goods.HdPage.initPage();
});


