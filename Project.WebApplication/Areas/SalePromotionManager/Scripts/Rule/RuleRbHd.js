var pro = pro || {};
(function () {
    pro.Rule = pro.Rule || {};
    pro.Rule.RuleRbHdPage = pro.Rule.RuleRbHdPage || {};
    pro.Rule.RuleRbHdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Rule.RuleRbHdPage.submit("RuleRbAdd");
            });

            $("#btnEdit").click(function () {
                pro.Rule.RuleRbHdPage.submit("RuleRbEdit");
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
            postData.RequestEntity.RuleSendTicketEntity = pro.submitKit.getHeadJson();

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
                        pro.commonKit.errorPlacementRuleRbHd(error, element);
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
    pro.Rule.RuleRbHdPage.initPage();
});


