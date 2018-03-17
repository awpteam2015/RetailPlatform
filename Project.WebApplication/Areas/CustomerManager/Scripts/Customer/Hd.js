var pro = pro || {};
(function () {
    pro.Customer = pro.Customer || {};
    pro.Customer.HdPage = pro.Customer.HdPage || {};
    pro.Customer.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Customer.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Customer.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Customer.ListPage.closeTab("");
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
                url: "/CustomerManager/Customer/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Customer.ListPage.closeTab();
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
          CardNo: { required: true  },
          Password: { required: true  },
          CustomerName: { required: true  },
          Gender: { required: true  },
          Birthday: { required: true  },
          Email: { required: true  },
          Familytelephone: { required: true  },
          Postcode: { required: true  },
          Mobilephone: { required: true  },
          ProvinceId: { required: true  },
          CityId: { required: true  },
          CountryId: { required: true  },
          Address: { required: true  },
          Memo: { required: true  },
          Discount: { required: true  },
          Totalamount: { required: true  },
          Totalpoints: { required: true  },
          Availablepoints: { required: true  },
          LastModificationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          CreationTime: { required: true  },
          CreatorUserCode: { required: true  },
          IsDeleted: { required: true  },
          DeleterUserCode: { required: true  },
          DeletionTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          CardNo:  "必填!",
          Password:  "密码必填!",
          CustomerName:  "会员名称必填!",
          Gender:  "性别必填!",
          Birthday:  "生日必填!",
          Email:  "邮件必填!",
          Familytelephone:  "家庭电话必填!",
          Postcode:  "邮编必填!",
          Mobilephone:  "手机必填!",
          ProvinceId:  "居住地址   省必填!",
          CityId:  "居住地址   市必填!",
          CountryId:  "居住地址   区（新增）必填!",
          Address:  "居住地址   详细地址必填!",
          Memo:  "备注必填!",
          Discount:  "折扣率必填!",
          Totalamount:  "消费总金额必填!",
          Totalpoints:  "总积分必填!",
          Availablepoints:  "可用积分必填!",
          LastModificationTime:  "修改时间必填!",
          LastModifierUserCode:  "修改人必填!",
          CreationTime:  "创建时间必填!",
          CreatorUserCode:  "创建人必填!",
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
    pro.Customer.HdPage.initPage();
});


