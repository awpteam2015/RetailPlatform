var pro = pro || {};
(function () {
    pro.OrderMain = pro.OrderMain || {};
    pro.OrderMain.ReturnPrdAfterSendAuditPage = pro.OrderMain.ReturnPrdAfterSendAuditPage || {};
    pro.OrderMain.ReturnPrdAfterSendAuditPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false)
            };
        },
        initPage: function () {

            $("#btnReturnPrdAfterSendAudit").click(function () {
                pro.OrderMain.ReturnPrdAfterSendAuditPage.submit("ReturnPrdAfterSendAudit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.OrderMain.ListPage.closeTab("");
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                    //$("[name=" + filedname + "]").attr("disabled", "disabled");
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
                url: "/OrderManager/OrderMain/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.OrderMain.ListPage.closeTab();
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
         
          ReturnPrdAfterSendAuditNo: { required: true  }
      
                    },
                    messages: {
         
          ReturnPrdAfterSendAuditNo:  "快递单号必填!"
         
                    },
                    errorPlacement: function (error, element) {
                        pro.commonKit.errorPlacementReturnPrdAfterSendAudit(error, element);
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
    pro.OrderMain.ReturnPrdAfterSendAuditPage.initPage();
});


