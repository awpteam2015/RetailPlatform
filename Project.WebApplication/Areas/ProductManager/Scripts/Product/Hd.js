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
          IsHasSpec1: { required: true  },
          IsHasSpec2: { required: true  },
          IsHasSpec3: { required: true  },
          Attribute1: { required: true  },
          Attribute2: { required: true  },
          Attribute3: { required: true  },
          PicUrl1: { required: true  },
          PicUrl2: { required: true  },
          PicUrl3: { required: true  },
          CreatorUserCode: { required: true  },
          CreationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          LastModificationTime: { required: true  },
          Remark: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          ProductName:  "产品名称必填!",
          ProductCode:  "产品编码必填!",
          Price:  "单价必填!",
          ProductCategoryId:  "商品分类必填!",
          IsHasSpec1:  "功率必填!",
          IsHasSpec2:  "颜色必填!",
          IsHasSpec3:  "其他必填!",
          Attribute1:  "属性1必填!",
          Attribute2:  "属性2必填!",
          Attribute3:  "属性3必填!",
          PicUrl1:  "图片地址1必填!",
          PicUrl2:  "图片地址2必填!",
          PicUrl3:  "图片地址3必填!",
          CreatorUserCode:  "创建人必填!",
          CreationTime:  "创建时间必填!",
          LastModifierUserCode:  "修改人必填!",
          LastModificationTime:  "修改时间必填!",
          Remark:  "备注必填!",
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


