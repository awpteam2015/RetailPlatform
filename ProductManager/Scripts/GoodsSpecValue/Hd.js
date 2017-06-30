var pro = pro || {};
(function () {
    pro.GoodsSpecValue = pro.GoodsSpecValue || {};
    pro.GoodsSpecValue.HdPage = pro.GoodsSpecValue.HdPage || {};
    pro.GoodsSpecValue.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.GoodsSpecValue.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.GoodsSpecValue.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.GoodsSpecValue.ListPage.closeTab("");
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
                url: "/ProductManager/GoodsSpecValue/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.GoodsSpecValue.ListPage.closeTab();
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
          GoodsId: { required: true  },
          ProductId: { required: true  },
          SpecId: { required: true  },
          SpecName: { required: true  },
          SpecValueId: { required: true  },
          SpecValueName: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          GoodsId:  "必填!",
          ProductId:  "必填!",
          SpecId:  "必填!",
          SpecName:  "必填!",
          SpecValueId:  "必填!",
          SpecValueName:  "必填!",
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
    pro.GoodsSpecValue.HdPage.initPage();
});


