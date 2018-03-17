var pro = pro || {};
(function () {
    pro.Brand = pro.Brand || {};
    pro.Brand.HdPage = pro.Brand.HdPage || {};
    pro.Brand.HdPage = {
        initPage: function () {

            UE.getEditor('Description', {
                initialFrameHeight: 320,
                autoHeightEnabled: false,
                initialFrameWidth: 800
            });


            $('#file_upload1').uploadify({
                'formData': { "path": "ImgFile" },
                'swf': '/Scripts/jqueryPlugins/jquery_uploadify/uploadify.swf',
                'uploader': '/SystemSetManager/Upload/UploadImage',
                'buttonText': '选择图片',
                // 'width':50,
                'fileTypeDesc': 'Image Files',
                'fileTypeExts': '*.jpg;*.bmp;*.png;*.gif',
                'onUploadSuccess': function (file, data, response) {
                    var json = $.parseJSON(data);
                    if (json.success) {
                        var pkid = 1;
                        $('#div_filename' + pkid).html("<span ><img id='img_" + pkid + "' name=\"listP\" style=\"height:150px;width:150px;\" src=\"" + json.extension.fileFullPath + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.Brand.HdPage.delImage(" + pkid + ")\">删除</a></span>");//+ json.extension.orgfileName
                        $("#ImageUrl" + pkid).val(json.extension.fileFullPath);

                    } else {
                        alert(json.msg);
                    }
                }
            });


            $("#btnAdd").click(function () {
                pro.Brand.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Brand.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Brand.ListPage.closeTab("");
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }

                var pkid = 1;
                $('#div_filename' + pkid).html("<span ><img id='img_" + pkid + "' name=\"listP\" style=\"height:150px;width:150px;\" src=\"" + bindEntity['ImageUrl'] + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.Product.ProductImageHd.delImage(" + pkid + ")\">删除</a></span>");

                $("#ImageUrl" + pkid).val(bindEntity['ImageUrl']);

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
                url: "/ProductManager/Brand/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Brand.ListPage.closeTab();
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
          BrandName: { required: true  }
          //Sort: { required: true  },
          //UrlLink: { required: true  },
          //Logo: { required: true  },
          //Remark: { required: true  },
          //GoodsNum: { required: true  }
                    },
                    messages: {
         // PkId:  "必填!",
          BrandName:  "必填!"
          //Sort:  "必填!",
          //UrlLink:  "必填!",
          //Logo:  "必填!",
          //Remark:  "必填!",
          //GoodsNum:  "产品数量必填!",
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

        },
        delImage: function (i) {
            $("#img_" + i).parent().remove();
            $("#ImageUrl" + i).val("");
        }

    };
})();



$(function () {
    pro.Brand.HdPage.initPage();
});


