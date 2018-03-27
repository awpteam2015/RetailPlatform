var pro = pro || {};
(function () {
    pro.Rule = pro.Rule || {};
    pro.Rule.HdPage = pro.Rule.HdPage || {};
    pro.Rule.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Rule.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Rule.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Rule.ListPage.closeTab("");
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
          PkId: { required: true  },
          ActivityId: { required: true  },
          RuleType: { required: true  },
          Title: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          ActivityId:  "必填!",
          RuleType:  "规则类型A B C必填!",
          Title:  "必填!",
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
    pro.Rule.HdPage.initPage();
});


