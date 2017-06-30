var pro = pro || {};
(function () {
    pro.ExtAttribute = pro.ExtAttribute || {};
    pro.ExtAttribute.HdPage = pro.ExtAttribute.HdPage || {};
    pro.ExtAttribute.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.ExtAttribute.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.ExtAttribute.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.ExtAttribute.ListPage.closeTab("");
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
                url: "/ProductManager/ExtAttribute/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.ExtAttribute.ListPage.closeTab();
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
          AttributeName: { required: true  },
          OtherName: { required: true  },
          ShowType: { required: true  },
          AttributeValues: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          AttributeName:  "必填!",
          OtherName:  "必填!",
          ShowType:  "表现方式 1 select 2input 3必填!",
          AttributeValues:  "必填!",
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
    pro.ExtAttribute.HdPage.initPage();
});


