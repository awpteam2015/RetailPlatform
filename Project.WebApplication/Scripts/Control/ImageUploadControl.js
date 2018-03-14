
var pro = pro || {};
(function () {
    pro.ImageUploadControl = pro.ImageUploadControl || {};
    pro.ImageUploadControl = {
        init: function (i) {
          
            $('#file_upload_' + i).uploadify({
                'formData': { "path": "ImgFile" },
                'swf': '/Scripts/jqueryPlugins/jquery_uploadify/uploadify.swf',
                'uploader': '/SystemSetManager/Upload/UploadImage',
                'buttonText': '选择图片',
                'selfDefineId': i,
                'fileTypeDesc': 'Image Files',
                'fileTypeExts': '*.jpg;*.bmp;*.png;*.gif',
                'onUploadSuccess': function (file, data, response) {
                    var json = $.parseJSON(data);
                    if (json.success) {
                        var pkid = this.settings.selfDefineId;
                        $('#div_filename_' + pkid).html("<span ><img id='img_" + pkid + "' name=\"listP\" style=\"height:30px;width:30px;\" src=\"" + json.extension.fileFullPath + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.ImageUploadControl.delImage(" + pkid + ")\">删除</a></span>");//+ json.extension.orgfileName
                        $("#ImageUrl_" + pkid).val(json.extension.fileFullPath);

                    } else {
                        alert(json.msg);
                    }
                }
            });

        },
        //init: function () {

        //},
        delImage: function(i) {
            $("#img_" + i).parent().remove();
            $("#ImageUrl_" + i).val("");
        }

    }
}
)();





