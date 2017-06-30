var pro = pro || {};
(function () {
    pro.Product = pro.Product || {};
    pro.Product.HdPage = pro.Product.HdPage || {};
    pro.Product.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Product.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Product.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Product.ListPage.closeTab("");
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
                url: "/ProductManager/Product/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Product.ListPage.closeTab();
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
          Id: { required: true  },
          SystemCategoryId: { required: true  },
          ProductId: { required: true  },
          Rank1: { required: true  },
                    },
                    messages: {
          Id:  "必填!",
          SystemCategoryId:  "必填!",
          ProductId:  "必填!",
          Rank1:  "等级必填!",
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
    pro.Product.HdPage.initPage();
});


