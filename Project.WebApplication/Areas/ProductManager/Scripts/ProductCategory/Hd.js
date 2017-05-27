var pro = pro || {};
(function () {
    pro.ProductCategory = pro.ProductCategory || {};
    pro.ProductCategory.HdPage = pro.ProductCategory.HdPage || {};
    pro.ProductCategory.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.ProductCategory.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.ProductCategory.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.ProductCategory.ListPage.closeTab("");
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
                url: "/ProductManager/ProductCategory/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.ProductCategory.ListPage.closeTab();
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
          ProductCategoryName: { required: true  },
          ParentProductCategoryId: { required: true  },
          CategoryRoute: { required: true  },
          Rank: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          ProductCategoryName:  "必填!",
          ParentProductCategoryId:  "必填!",
          CategoryRoute:  "分类路径必填!",
          Rank:  "分类层级必填!",
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
    pro.ProductCategory.HdPage.initPage();
});


