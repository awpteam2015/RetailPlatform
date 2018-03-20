var pro = pro || {};
(function () {
    pro.CardType = pro.CardType || {};
    pro.CardType.HdPage = pro.CardType.HdPage || {};
    pro.CardType.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.CardType.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.CardType.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.CardType.ListPage.closeTab("");
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
                url: "/CustomerManager/CardType/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.CardType.ListPage.closeTab();
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
          //PkId: { required: true  },
          CardtypeName: { required: true  },
          Discount: { required: true  }
                    },
                    messages: {
          PkId:  "自动增加得建立序列必填!",
          CardtypeName:  "名称必填!",
          Discount:  "折扣必填!",
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
    pro.CardType.HdPage.initPage();
});


