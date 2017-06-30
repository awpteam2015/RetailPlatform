var pro = pro || {};
(function () {
    pro.Brand = pro.Brand || {};
    pro.Brand.HdPage = pro.Brand.HdPage || {};
    pro.Brand.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Brand.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Brand.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Brand.ListPage.closeTab("");
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
                url: "/ProductManager/Brand/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Brand.ListPage.closeTab();
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
          BrandName: { required: true  },
          Sort: { required: true  },
          UrlLink: { required: true  },
          Logo: { required: true  },
          Remark: { required: true  },
          GoodsNum: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          BrandName:  "必填!",
          Sort:  "必填!",
          UrlLink:  "必填!",
          Logo:  "必填!",
          Remark:  "必填!",
          GoodsNum:  "产品数量必填!",
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
    pro.Brand.HdPage.initPage();
});


