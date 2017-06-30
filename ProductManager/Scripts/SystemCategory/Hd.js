var pro = pro || {};
(function () {
    pro.SystemCategory = pro.SystemCategory || {};
    pro.SystemCategory.HdPage = pro.SystemCategory.HdPage || {};
    pro.SystemCategory.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.SystemCategory.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.SystemCategory.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.SystemCategory.ListPage.closeTab("");
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
                url: "/ProductManager/SystemCategory/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.SystemCategory.ListPage.closeTab();
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
          SystemCategoryName: { required: true  },
          Sort: { required: true  },
          CreatorUserCode: { required: true  },
          CreationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          LastModificationTime: { required: true  },
          IsDeleted: { required: true  },
          DeleterUserCode: { required: true  },
          DeletionTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          SystemCategoryName:  "分类名称必填!",
          Sort:  "排序必填!",
          CreatorUserCode:  "创建人必填!",
          CreationTime:  "创建时间必填!",
          LastModifierUserCode:  "修改人必填!",
          LastModificationTime:  "修改时间必填!",
          IsDeleted:  "是否删除必填!",
          DeleterUserCode:  "删除人必填!",
          DeletionTime:  "删除时间必填!",
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
    pro.SystemCategory.HdPage.initPage();
});


