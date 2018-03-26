var pro = pro || {};
(function () {
    pro.ProductCategory = pro.ProductCategory || {};
    pro.ProductCategory.HdPage = pro.ProductCategory.HdPage || {};
    pro.ProductCategory.HdPage = {
        initPage: function () {

            pro.ProductcategoryControl.init({ controlId: "ParentId", required: true });

            var parentId = pro.commonKit.getUrlParam("ParentId");
            if (parentId>0) {
                $("input[name=ParentId]").val(parentId);
            }

            $("#btnAdd").click(function () {
                pro.ProductCategory.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.ProductCategory.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.ProductCategory.ListPage.closeTab("");
            });

            var bindEntity = "";
            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                 bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }

            $('#SystemCategoryId').combobox({
                required: false,
                editable: false,
                valueField: 'PkId',
                textField: 'SystemCategoryName',
                url: '/ProductManager/SystemCategory/GetList_Combobox',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#SystemCategoryId").combobox('setValue', bindEntity['SystemCategoryId']);
                    }
                    
                    if (pro.commonKit.getUrlParam("SystemCategoryId") > 0) {
                        $("#SystemCategoryId").combobox('setValue', pro.commonKit.getUrlParam("SystemCategoryId"));
                    }

                }
            });

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
                url: "/ProductManager/ProductCategory/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.ProductCategory.ListPage.closeTab();
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
          ProductCategoryName: { required: true  },
          ParentId: { required: true  },
          //Rank: { required: true  },
          //Sort: { required: true  },
          SystemCategoryId: { required: true  },
          SystemCategoryName: { required: true  }
          //Route: { required: true  },
          //CreatorUserCode: { required: true  },
          //CreationTime: { required: true  },
          //LastModifierUserCode: { required: true  },
          //LastModificationTime: { required: true  },
          //IsDeleted: { required: true  },
          //DeleterUserCode: { required: true  },
          //DeletionTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          ProductCategoryName:  "必填!",
          ParentId:  "必填!",
          Rank:  "层级必填!",
          Sort:  "必填!",
          SystemCategoryId:  "必填!",
          SystemCategoryName:  "必填!",
          Route:  "路径必填!",
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
    pro.ProductCategory.HdPage.initPage();
});


