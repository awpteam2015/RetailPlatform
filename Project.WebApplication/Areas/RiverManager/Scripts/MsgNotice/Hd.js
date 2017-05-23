var pro = pro || {};
(function () {
    pro.MsgNotice = pro.MsgNotice || {};
    pro.MsgNotice.HdPage = pro.MsgNotice.HdPage || {};
    pro.MsgNotice.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.MsgNotice.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.MsgNotice.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.MsgNotice.ListPage.closeTab("");
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
                url: "/RiverManager/MsgNotice/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.MsgNotice.ListPage.closeTab();
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
          Title: { required: true  },
          Des: { required: true  },
          CreationTime: { required: true  },
          CreatorUserCode: { required: true  },
          LastModificationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          IsDelete: { required: true  },
          IsSend: { required: true  },
          SendTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          Title:  "必填!",
          Des:  "必填!",
          CreationTime:  "创建时间必填!",
          CreatorUserCode:  "创建人必填!",
          LastModificationTime:  "修改时间必填!",
          LastModifierUserCode:  "修改人必填!",
          IsDelete:  "必填!",
          IsSend:  "是否已发送 0代表未发送必填!",
          SendTime:  "发送日期必填!",
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
    pro.MsgNotice.HdPage.initPage();
});


