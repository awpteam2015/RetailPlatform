var pro = pro || {};
(function () {
    pro.RuleSendTicket = pro.RuleSendTicket || {};
    pro.RuleSendTicket.HdPage = pro.RuleSendTicket.HdPage || {};
    pro.RuleSendTicket.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.RuleSendTicket.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.RuleSendTicket.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.RuleSendTicket.ListPage.closeTab("");
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
                url: "/SalePromotionManager/RuleSendTicket/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.RuleSendTicket.ListPage.closeTab();
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
          TicketNum: { required: true  },
          CardTypeIds: { required: true  },
          TicketAvaildateEnd: { required: true  },
          TicketAvaildateStart: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          RuleId:  "活动规则Id必填!",
          ActivityId:  "活动Id必填!",
          StartMoney:  "订单金额范围必填!",
          EndMoney:  "订单金额范围必填!",
          TicketNum:  "券张数必填!",
          CardTypeIds:  "会员可类型范围必填!",
          TicketAvaildateEnd:  "有效时间结束必填!",
          TicketAvaildateStart:  "有效时间开始必填!",
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
    pro.RuleSendTicket.HdPage.initPage();
});


