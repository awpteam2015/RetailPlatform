var pro = pro || {};
(function () {
    pro.Ticket = pro.Ticket || {};
    pro.Ticket.HdPage = pro.Ticket.HdPage || {};
    pro.Ticket.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Ticket.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Ticket.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Ticket.ListPage.closeTab("");
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
                url: "/SalePromotionManager/Ticket/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Ticket.ListPage.closeTab();
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
          TicketCode: { required: true  },
          TickettypeId: { required: true  },
          Status: { required: true  },
          AvaildateStart: { required: true  },
          AvaildateEnd: { required: true  },
          OrderNo: { required: true  },
          UseDate: { required: true  },
          CustomerId: { required: true  },
          RuleId: { required: true  },
          ActivityId: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          TicketCode:  "必填!",
          TickettypeId:  "券类型编码必填!",
          Status:  "激活 作废 已使用必填!",
          AvaildateStart:  "有效时间开始必填!",
          AvaildateEnd:  "有效时间结束必填!",
          OrderNo:  "使用订单号（2012.5.30新增）必填!",
          UseDate:  "使用日期（2012.5.30新增）必填!",
          CustomerId:  "归属会员必填!",
          RuleId:  "活动规则Id必填!",
          ActivityId:  "活动Id必填!",
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
    pro.Ticket.HdPage.initPage();
});


