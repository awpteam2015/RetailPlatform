var pro = pro || {};
(function () {
    pro.Product = pro.Product || {};
    pro.Product.ProductImageHd = pro.Product.ProductImageHd || {};
    pro.Product.ProductImageHd = {
        opData: {

        },
        init: function () {
            this.initImageArea();

            this.bindData();
        },
        bindData: function () {
            if ($("#BindEntity").val()) {
                var bindEntity = JSON.parse($("#BindEntity").val());

                var i = 1;
                JSLINQ(bindEntity.ProductImageEntityList).ForEach(function (image) {
                    var pkid = i;
                    $('#div_filename' + pkid).html("<span ><img id='img_" + pkid + "' name=\"listP\" style=\"height:150px;width:150px;\" src=\"" + image.ImageUrl + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.Product.ProductImageHd.delImage(" + pkid + ")\">删除</a></span>");

                    $("#ImageUrl" + pkid).val(image.ImageUrl);
                    i++;
                });
            }
        },
        initImageArea: function () {
            for (var i = 1; i <= 5; i++) {
                $('#file_upload' + i).uploadify({
                    'formData': { "path": "ProductImgFile", "isGenerateOtherSize": "1" },
                    'swf': '/Scripts/jqueryPlugins/jquery_uploadify/uploadify.swf',
                    'uploader': '/SystemSetManager/Upload/UploadImage',
                    'buttonText': '选择图片',
                    'selfDefineId': i,
                   // 'width':50,
                    'fileTypeDesc': 'Image Files',
                    'fileTypeExts': '*.jpg;*.bmp;*.png;*.gif',
                    'onUploadSuccess': function (file, data, response) {
                        var json = $.parseJSON(data);
                        if (json.success) {
                            var pkid = this.settings.selfDefineId;
                            $('#div_filename' + pkid).html("<span ><img id='img_" + pkid + "' name=\"listP\" style=\"height:150px;width:150px;\" src=\"" + json.extension.fileFullPath + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.Product.ProductImageHd.delImage(" + pkid + ")\">删除</a></span>");//+ json.extension.orgfileName
                            $("#ImageUrl" + pkid).val(json.extension.fileFullPath);

                        } else {
                            alert(json.msg);
                        }
                    }
                });
            }
        },
        initEvent: function () {

        },
        delImage: function (i) {
            $("#img_" + i).parent().remove();
            $("#ImageUrl" + i).val("");
        },
        getImageList: function () {
            var imageList = [];
            var i = 0;
            $("input[name=ImageUrl]").each(
                function () {
                    if ($(this).val() != "") {
                        var isDefault = (i == 0 ? 1 : 0);
                        imageList.push({ ImageUrl: $(this).val(), IsDefault: isDefault });
                        i++;
                    }
                }
            );

            return imageList;
        }

    }
})();



$(function () {
    pro.Product.ProductImageHd.init();
});