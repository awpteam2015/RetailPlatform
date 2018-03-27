var pro = pro || {};
(function () {
    pro.Rule = pro.Rule || {};
    pro.Rule.RuleRcHdPage = pro.Rule.RuleRcHdPage || {};
    pro.Rule.RuleRcHdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Rule.RuleRcHdPage.submit("RuleRcAdd");
            });

            $("#btnEdit").click(function () {
                pro.Rule.RuleRcHdPage.submit("RuleRcEdit");
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

                $("input[name=StartMoney]").val(bindEntity.RuleDiscountMoneyEntity.StartMoney);
                $("input[name=EndMoney]").val(bindEntity.RuleDiscountMoneyEntity.EndMoney);
                $("input[name=DiscountMoney]").val(bindEntity.RuleDiscountMoneyEntity.DiscountMoney);

                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }
        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            postData.RequestEntity.RuleDiscountMoneyEntity = pro.submitKit.getHeadJson();

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
                        Title: { required: true },
                        StartMoney: { required: true },
                        EndMoney: { required: true },
                        DiscountMoney: { required: true }
                    },
                    messages: {
          Title: "必填!",
          StartMoney: "订单金额范围必填!",
          EndMoney: "订单金额范围必填!",
          DiscountMoney: "折扣金额必填!"
                    },
                    errorPlacement: function (error, element) {
                        pro.commonKit.errorPlacementRuleRcHd(error, element);
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
    pro.Rule.RuleRcHdPage.initPage();
});


