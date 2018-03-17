var pro = pro || {};
(function () {
    pro.PageContent = pro.PageContent || {};
    pro.PageContent.HdPage = pro.PageContent.HdPage || {};
    pro.PageContent.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.PageContent.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.PageContent.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.PageContent.ListPage.closeTab("");
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
                url: "/ContentManager/PageContent/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.PageContent.ListPage.closeTab();
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
          Title1: { required: true  },
          Title2: { required: true  },
          Title3: { required: true  },
          Description1: { required: true  },
          Description2: { required: true  },
          Description3: { required: true  },
          ImageUrl1: { required: true  },
          ImageUrl2: { required: true  },
          ImageUrl3: { required: true  },
          DeletionTime: { required: true  },
          DeleterUserCode: { required: true  },
          IsDeleted: { required: true  },
          LastModificationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          CreationTime: { required: true  },
          CreatorUserCode: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          Title1:  "必填!",
          Title2:  "必填!",
          Title3:  "必填!",
          Description1:  "必填!",
          Description2:  "必填!",
          Description3:  "必填!",
          ImageUrl1:  "必填!",
          ImageUrl2:  "必填!",
          ImageUrl3:  "必填!",
          DeletionTime:  "删除时间必填!",
          DeleterUserCode:  "删除人必填!",
          IsDeleted:  "是否删除必填!",
          LastModificationTime:  "修改时间必填!",
          LastModifierUserCode:  "修改人必填!",
          CreationTime:  "创建时间必填!",
          CreatorUserCode:  "创建人必填!",
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
    pro.PageContent.HdPage.initPage();
});


