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
          PkId: { required: true  },
          ProductName: { required: true  },
          ProductCode: { required: true  },
          Price: { required: true  },
          ProductCategoryId: { required: true  },
          SpecId1: { required: true  },
          SpecId2: { required: true  },
          SpecId3: { required: true  },
          SpecName1: { required: true  },
          SpecName2: { required: true  },
          SpecName3: { required: true  },
          Attribute1: { required: true  },
          Attribute2: { required: true  },
          Attribute3: { required: true  },
          PicUrl1: { required: true  },
          PicUrl2: { required: true  },
          PicUrl3: { required: true  },
                    },
                    messages: {
          PkId:  "111必填!",
          ProductName:  "必填!",
          ProductCode:  "必填!",
          Price:  "必填!",
          ProductCategoryId:  "必填!",
          SpecId1:  "必填!",
          SpecId2:  "必填!",
          SpecId3:  "必填!",
          SpecName1:  "必填!",
          SpecName2:  "必填!",
          SpecName3:  "必填!",
          Attribute1:  "必填!",
          Attribute2:  "必填!",
          Attribute3:  "必填!",
          PicUrl1:  "必填!",
          PicUrl2:  "必填!",
          PicUrl3:  "必填!",
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


