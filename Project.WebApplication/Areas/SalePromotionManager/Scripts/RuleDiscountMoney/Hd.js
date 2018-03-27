var pro = pro || {};
(function () {
    pro.RuleDiscountMoney = pro.RuleDiscountMoney || {};
    pro.RuleDiscountMoney.HdPage = pro.RuleDiscountMoney.HdPage || {};
    pro.RuleDiscountMoney.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.RuleDiscountMoney.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.RuleDiscountMoney.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.RuleDiscountMoney.ListPage.closeTab("");
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
                url: "/SalePromotionManager/RuleDiscountMoney/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.RuleDiscountMoney.ListPage.closeTab();
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
          RuleId: { required: true  },
          ActivityId: { required: true  },
          StartMoney: { required: true  },
          EndMoney: { required: true  },
          DiscountMoney: { required: true  },
          CardTypeIds: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          RuleId:  "活动规则Id必填!",
          ActivityId:  "活动Id必填!",
          StartMoney:  "订单金额范围必填!",
          EndMoney:  "订单金额范围必填!",
          DiscountMoney:  "折扣金额必填!",
          CardTypeIds:  "会员可类型范围必填!",
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
    pro.RuleDiscountMoney.HdPage.initPage();
});


