var pro = pro || {};
(function () {
    pro.RiverAttach = pro.RiverAttach || {};
    pro.RiverAttach.HdPage = pro.RiverAttach.HdPage || {};
    pro.RiverAttach.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.RiverAttach.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.RiverAttach.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.RiverAttach.ListPage.closeTab("");
             });


             $('#RiverId').combobox({
                 required: true,
                 editable: false,
                 valueField: 'PkId',
                 textField: 'RiverName',
                 url: '/RiverManager/River/GetListNoPage',
                 onLoadSuccess: function () {
                     if (pro.commonKit.getUrlParam("PkId") > 0) {
                         $("#RiverId").combobox('setValue', bindEntity['RiverId']);
                     }
                 }
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
            postData.RequestEntity.RiverName = $('#RiverId').combobox('getText');
            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/RiverManager/RiverAttach/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.RiverAttach.ListPage.closeTab();
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
      
          RiverId: { required: true  },
  
          RecordTime: { required: true  },
          Remark: { required: true  }
                    },
                    messages: {
          PkId:  "必填!",
          RiverId:  "必填!",
          RiverName:  "必填!",
          RecordTime:  "记录月份必填!",
          Remark:  "水纹水质必填!"
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
    pro.RiverAttach.HdPage.initPage();
});


