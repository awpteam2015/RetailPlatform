var pro = pro || {};
(function () {
    pro.OfflineActivity = pro.OfflineActivity || {};
    pro.OfflineActivity.HdPage = pro.OfflineActivity.HdPage || {};
    pro.OfflineActivity.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.OfflineActivity.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.OfflineActivity.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.OfflineActivity.ListPage.closeTab("");
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
                url: "/ContentManager/OfflineActivity/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.OfflineActivity.ListPage.closeTab();
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
          Tttle: { required: true  },
          OfflineActivityAddress: { required: true  },
          StartDate: { required: true  },
          EndDate: { required: true  },
          ImageUrl: { required: true  },
          BriefDescription: { required: true  },
          State: { required: true  },
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
          Tttle:  "活动标题必填!",
          OfflineActivityAddress:  "活动地址必填!",
          StartDate:  "开始时间必填!",
          EndDate:  "结束时间必填!",
          ImageUrl:  "图片必填!",
          BriefDescription:  "简介必填!",
          State:  "活动状态必填!",
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
    pro.OfflineActivity.HdPage.initPage();
});


