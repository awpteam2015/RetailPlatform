var pro = pro || {};
(function () {
    pro.Goods = pro.Goods || {};
    pro.Goods.HdPage = pro.Goods.HdPage || {};
    pro.Goods.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Goods.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Goods.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Goods.ListPage.closeTab("");
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
                url: "/ProductManager/Goods/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Goods.ListPage.closeTab();
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
          GoodsCode: { required: true  },
          GoodsName: { required: true  },
          ProductCode: { required: true  },
          SpecValue1: { required: true  },
          SpecValue2: { required: true  },
          SpecValue3: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          GoodsCode:  "商品编码必填!",
          GoodsName:  "商品名称必填!",
          ProductCode:  "产品编码必填!",
          SpecValue1:  "功率必填!",
          SpecValue2:  "颜色必填!",
          SpecValue3:  "其他必填!",
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
    pro.Goods.HdPage.initPage();
});


