var pro = pro || {};
(function () {
    pro.RulePromotionGoods = pro.RulePromotionGoods || {};
    pro.RulePromotionGoods.HdPage = pro.RulePromotionGoods.HdPage || {};
    pro.RulePromotionGoods.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.RulePromotionGoods.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.RulePromotionGoods.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.RulePromotionGoods.ListPage.closeTab("");
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
          if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/SalePromotionManager/RulePromotionGoods/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.RulePromotionGoods.ListPage.closeTab();
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
          ActivityId: { required: true  },
          RuleId: { required: true  },
          ProductId: { required: true  },
          ProductCode: { required: true  },
          GoodsCode: { required: true  },
          GoodsId: { required: true  },
          Price: { required: true  },
          PromotionPrice: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          ActivityId:  "活动Id必填!",
          RuleId:  "活动规则Id必填!",
          ProductId:  "产品主键必填!",
          ProductCode:  "产品编码必填!",
          GoodsCode:  "商品代码必填!",
          GoodsId:  "商品主键必填!",
          Price:  "单价必填!",
          PromotionPrice:  "促销价必填!",
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
    pro.RulePromotionGoods.HdPage.initPage();
});


