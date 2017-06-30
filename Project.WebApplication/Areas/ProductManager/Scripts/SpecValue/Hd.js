var pro = pro || {};
(function () {
    pro.SpecValue = pro.SpecValue || {};
    pro.SpecValue.HdPage = pro.SpecValue.HdPage || {};
    pro.SpecValue.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.SpecValue.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.SpecValue.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.SpecValue.ListPage.closeTab("");
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
                url: "/ProductManager/SpecValue/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.SpecValue.ListPage.closeTab();
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
          SpecId: { required: true  },
          SpecValueName: { required: true  },
          Sort: { required: true  },
          ImagePath: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          SpecId:  "必填!",
          SpecValueName:  "必填!",
          Sort:  "必填!",
          ImagePath:  "必填!",
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
    pro.SpecValue.HdPage.initPage();
});


