
var pro = pro || {};
(function () {
    pro.ImageUploadDefaultControl = pro.ImageUploadDefaultControl || {};
    pro.ImageUploadDefaultControl = {
        init: function () {

            $('#file_upload').uploadify({
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
                        $('#div_filename').html("<span ><img id='img' name=\"listP\" style=\"height:150px;width:150px;\" src=\"" + json.extension.fileFullPath + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.ImageUploadDefaultControl.delImage()\">删除</a></span>");//+ json.extension.orgfileName
                        $("#ImageUrl").val(json.extension.fileFullPath);
                    } else {
                        alert(json.msg);
                    }
                }
            });
        },
        delImage: function () {
            $("#img").parent().remove();
            $("#ImageUrl").val("");
        },
        bindData: function (imageUrl) {
            $('#div_filename').html("<span ><img id='img' name=\"listP\" style=\"height:150px;width:150px;\" src=\"" + imageUrl + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.Product.ImageUploadDefaultControl.delImage()\">删除</a></span>");
            $("#ImageUrl").val(imageUrl);
        }

    }
}
)();





