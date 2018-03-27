var pro = pro || {};
(function () {
    pro.Activity = pro.Activity || {};
    pro.Activity.HdPage = pro.Activity.HdPage || {};
    pro.Activity.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Activity.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Activity.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Activity.ListPage.closeTab("");
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
                url: "/SalePromotionManager/Activity/" + command,
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
          Title: { required: true  },
          StartDate: { required: true  },
          EndDate: { required: true  },
          State: { required: true  },
          BriefDescription: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          Title:  "促销主题必填!",
          StartDate:  "必填!",
          EndDate:  "必填!",
          State:  "必填!",
          BriefDescription:  "简介必填!",
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
    pro.Activity.HdPage.initPage();
});


