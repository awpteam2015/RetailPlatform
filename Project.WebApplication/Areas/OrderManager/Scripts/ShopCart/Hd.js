var pro = pro || {};
(function () {
    pro.ShopCart = pro.ShopCart || {};
    pro.ShopCart.HdPage = pro.ShopCart.HdPage || {};
    pro.ShopCart.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.ShopCart.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.ShopCart.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.ShopCart.ListPage.closeTab("");
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
                url: "/OrderManager/ShopCart/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.ShopCart.ListPage.closeTab();
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
          OrderNo: { required: true  },
          ProductCategoryId: { required: true  },
          ProductId: { required: true  },
          GoodsCode: { required: true  },
          GoodsId: { required: true  },
          Price: { required: true  },
          PriceSubDiscount: { required: true  },
          TotalAmount: { required: true  },
          ProductWeight: { required: true  },
          SpecName: { required: true  },
          CustomerId: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          OrderNo:  "订单号必填!",
          ProductCategoryId:  "商品分类必填!",
          ProductId:  "产品主键必填!",
          GoodsCode:  "商品代码必填!",
          GoodsId:  "商品主键必填!",
          Price:  "商品原价_decimal_必填!",
          PriceSubDiscount:  "购买价_decimal_必填!",
          TotalAmount:  "单项小计_decimal_必填!",
          ProductWeight:  "商品重量必填!",
          SpecName:  "规格汇总必填!",
          CustomerId:  "必填!",
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
    pro.ShopCart.HdPage.initPage();
});


