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
          SystemCategoryId: { required: true  },
          ProductCategoryId: { required: true  },
          ProductCategoryRoute: { required: true  },
          BrandId: { required: true  },
          ProductCode: { required: true  },
          Unit: { required: true  },
          BriefDescription: { required: true  },
          Description: { required: true  },
          Weight: { required: true  },
          WeightUnit: { required: true  },
          MarketPrice: { required: true  },
          SellPrice: { required: true  },
          Cost: { required: true  },
          PriceUnit: { required: true  },
          StockNum: { required: true  },
          BuyMaxNum: { required: true  },
          BuyMinNum: { required: true  },
          ViewNum: { required: true  },
          CommentNum: { required: true  },
          SelledNum: { required: true  },
          Title: { required: true  },
          MetaKeywords: { required: true  },
          MetaDescription: { required: true  },
          IsShow: { required: true  },
          IsCommand: { required: true  },
          PdtDesc: { required: true  },
          SpecDesc: { required: true  },
          ParamsDesc: { required: true  },
          TagsDesc: { required: true  },
          CreatorUserCode: { required: true  },
          CreationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          LastModificationTime: { required: true  },
          IsDeleted: { required: true  },
          DeleterUserCode: { required: true  },
          DeletionTime: { required: true  },
          P1: { required: true  },
          P2: { required: true  },
          P3: { required: true  },
          P4: { required: true  },
          P5: { required: true  },
          P6: { required: true  },
          P7: { required: true  },
          P8: { required: true  },
          P9: { required: true  },
                    },
                    messages: {
          PkId:  "产品ID必填!",
          ProductName:  "产品名称必填!",
          SystemCategoryId:  "系统类型必填!",
          ProductCategoryId:  "商品分类必填!",
          ProductCategoryRoute:  "商品分类全路由必填!",
          BrandId:  "品牌ID必填!",
          ProductCode:  "商品编号必填!",
          Unit:  "计量单位必填!",
          BriefDescription:  "简介必填!",
          Description:  "详细描述必填!",
          Weight:  "重量必填!",
          WeightUnit:  "重量单位必填!",
          MarketPrice:  "市场价必填!",
          SellPrice:  "销售价必填!",
          Cost:  "成本价必填!",
          PriceUnit:  "货币单位必填!",
          StockNum:  "库存数量必填!",
          BuyMaxNum:  "最大数量限制必填!",
          BuyMinNum:  "最小数量限制必填!",
          ViewNum:  "访问次数必填!",
          CommentNum:  "评论次数必填!",
          SelledNum:  "售出数量必填!",
          Title:  "页面标题必填!",
          MetaKeywords:  "页面关键字必填!",
          MetaDescription:  "页面描述必填!",
          IsShow:  "是否上架必填!",
          IsCommand:  "是否推荐必填!",
          PdtDesc:  "Pdt_desc必填!",
          SpecDesc:  "Spec_desc必填!",
          ParamsDesc:  "Params_desc必填!",
          TagsDesc:  "Tags_desc必填!",
          CreatorUserCode:  "创建人必填!",
          CreationTime:  "创建时间必填!",
          LastModifierUserCode:  "修改人必填!",
          LastModificationTime:  "修改时间必填!",
          IsDeleted:  "是否删除必填!",
          DeleterUserCode:  "删除人必填!",
          DeletionTime:  "删除时间必填!",
          P1:  "扩展属性1必填!",
          P2:  "扩展属性2必填!",
          P3:  "扩展属性1必填!",
          P4:  "扩展属性2必填!",
          P5:  "扩展属性1必填!",
          P6:  "扩展属性2必填!",
          P7:  "扩展属性1必填!",
          P8:  "扩展属性2必填!",
          P9:  "扩展属性1必填!",
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


