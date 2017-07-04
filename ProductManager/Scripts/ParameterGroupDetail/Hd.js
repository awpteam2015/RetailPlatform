﻿var pro = pro || {};
(function () {
    pro.ParameterGroupDetail = pro.ParameterGroupDetail || {};
    pro.ParameterGroupDetail.HdPage = pro.ParameterGroupDetail.HdPage || {};
    pro.ParameterGroupDetail.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.ParameterGroupDetail.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.ParameterGroupDetail.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.ParameterGroupDetail.ListPage.closeTab("");
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
                url: "/ProductManager/ParameterGroupDetail/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.ParameterGroupDetail.ListPage.closeTab();
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
          ParameterName: { required: true  },
          ParameterGroupId: { required: true  },
          SystemCategoryId: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          ParameterName:  "参数明细必填!",
          ParameterGroupId:  "必填!",
          SystemCategoryId:  "必填!",
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
    pro.ParameterGroupDetail.HdPage.initPage();
});


