var pro = pro || {};
(function () {
    pro.OrderInvoice = pro.OrderInvoice || {};
    pro.OrderInvoice.HdPage = pro.OrderInvoice.HdPage || {};
    pro.OrderInvoice.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.OrderInvoice.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.OrderInvoice.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.OrderInvoice.ListPage.closeTab("");
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
                url: "/OrderManager/OrderInvoice/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.OrderInvoice.ListPage.closeTab();
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
          OrderNo: { required: true  },
          InvoiceTitle: { required: true  },
          InvoiceContent: { required: true  },
          InvoiceCompany: { required: true  },
          InvoiceNo: { required: true  },
          Money: { required: true  },
          Remark: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          OrderNo:  "订单号必填!",
          InvoiceTitle:  "发票抬头必填!",
          InvoiceContent:  "发票内容必填!",
          InvoiceCompany:  "开票公司必填!",
          InvoiceNo:  "发票编码必填!",
          Money:  "开票金额必填!",
          Remark:  "备注必填!",
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
    pro.OrderInvoice.HdPage.initPage();
});


