var pro = pro || {};
(function () {
    pro.CustomerAddress = pro.CustomerAddress || {};
    pro.CustomerAddress.HdPage = pro.CustomerAddress.HdPage || {};
    pro.CustomerAddress.HdPage = {
        initPage: function () {
            pro.AreaSelectControl.init({ required: true });


            $("#btnAdd").click(function () {
                pro.CustomerAddress.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.CustomerAddress.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.CustomerAddress.ListPage.closeTab("");
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
            postData.RequestEntity.CustomerId = pro.commonKit.getUrlParam("CustomerId");

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
          if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/CustomerManager/CustomerAddress/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                       parent.$("#btnRefresh").trigger("click");
                        parent.pro.CustomerAddress.ListPage.closeTab();
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
          //CustomerId: { required: true  },
          //Province: { required: true  },
          //CityId: { required: true  },
          //CountryId: { required: true  },
          //Address: { required: true  },
          //IsDefault: { required: true  },
          //Remarks: { required: true  },
          ReceiverName: { required: true  },
          //FamilyTelephone: { required: true  },
          //PostCode: { required: true  },
          Mobilephone: { required: true  }
                    },
                    messages: {
          PkId:  "自动增加得建立序列必填!",
          CustomerId:  "必填!",
          Province:  "送货地址  省必填!",
          CityId:  "送货地址   市必填!",
          CountryId:  "送货地址   区（新增）必填!",
          Address:  "送货地址   详细地址必填!",
          IsDefault:  "是否是默认地址必填!",
          Remarks:  "备注必填!",
          ReceiverName:  "收货人姓名必填!",
          FamilyTelephone:  "电话必填!",
          PostCode:  "邮编必填!",
          Mobilephone:  "手机必填!",
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
    pro.CustomerAddress.HdPage.initPage();
});


