var pro = pro || {};
(function () {
    pro.OrderMain = pro.OrderMain || {};
    pro.OrderMain.HdPage = pro.OrderMain.HdPage || {};
    pro.OrderMain.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.OrderMain.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.OrderMain.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.OrderMain.ListPage.closeTab("");
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
                url: "/OrderManager/OrderMain/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.OrderMain.ListPage.closeTab();
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
          OrderNo: { required: true  },
          State: { required: true  },
          Totalamount: { required: true  },
          PresentPoints: { required: true  },
          CustomerId: { required: true  },
          CustomerName: { required: true  },
          Linkman: { required: true  },
          LinkmanTel: { required: true  },
          LinkmanMobilephone: { required: true  },
          LinkmanProvinceId: { required: true  },
          LinkmanCityId: { required: true  },
          LinkmanAreaId: { required: true  },
          LinkmanAddress: { required: true  },
          LinkmanAddressfull: { required: true  },
          LinkmanPostcode: { required: true  },
          LinkmanRemark: { required: true  },
          CustomerAddressId: { required: true  },
          PayTime: { required: true  },
          PayRemark: { required: true  },
          SendTime: { required: true  },
          SendNo: { required: true  },
          SendRemark: { required: true  },
          ReturnTime: { required: true  },
          ReturnRemark: { required: true  },
          ConfirmTime: { required: true  },
          ConfirmRemark: { required: true  },
          UserIp: { required: true  },
          CreatorUserCode: { required: true  },
          CreationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          LastModificationTime: { required: true  },
          IsDeleted: { required: true  },
          DeleterUserCode: { required: true  },
          DeletionTime: { required: true  },
                    },
                    messages: {
          OrderNo:  "订单号必填!",
          State:  "订单状态(作废:-1;未确认:0;确认:1;先退货审核:T;子订单部分为确认:2)必填!",
          Totalamount:  "订单总价,包括赠品_decimal_必填!",
          PresentPoints:  "赠送积分必填!",
          CustomerId:  "会员Id必填!",
          CustomerName:  "会员姓名必填!",
          Linkman:  "联系人（改）必填!",
          LinkmanTel:  "联系人电话必填!",
          LinkmanMobilephone:  "联系人手机必填!",
          LinkmanProvinceId:  "联系人省份必填!",
          LinkmanCityId:  "联系人城市必填!",
          LinkmanAreaId:  "联系人区域(新增)必填!",
          LinkmanAddress:  "联系人配送地址（改）必填!",
          LinkmanAddressfull:  "联系人配送地址全（改2012.11.2）必填!",
          LinkmanPostcode:  "联系人邮政编码（改）必填!",
          LinkmanRemark:  "联系人送货备注（改）必填!",
          CustomerAddressId:  "送货地址id（改2012.11.20）必填!",
          PayTime:  "支付时间必填!",
          PayRemark:  "支付备注必填!",
          SendTime:  "发货时间必填!",
          SendNo:  "快递单号必填!",
          SendRemark:  "发货备注必填!",
          ReturnTime:  "退货时间必填!",
          ReturnRemark:  "退货备注必填!",
          ConfirmTime:  "客户确认时间必填!",
          ConfirmRemark:  "客户确认备注必填!",
          UserIp:  "创建人ip必填!",
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
    pro.OrderMain.HdPage.initPage();
});


