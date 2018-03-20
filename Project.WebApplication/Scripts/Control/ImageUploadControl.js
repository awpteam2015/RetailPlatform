
var pro = pro || {};
(function () {
    pro.ImageUploadControl = pro.ImageUploadControl || {};
    pro.ImageUploadControl = {
        init: function (i, combineChar) {
            i = i == undefined ? "" : i;
            combineChar = combineChar == undefined ? "" : undefined;

            pro.ImageUploadControl.initHtml(i, combineChar);

            $('#file_upload' + combineChar + i).uploadify({
                'formData': { "path": "ImgFile" },
                'swf': '/Scripts/jqueryPlugins/jquery_uploadify/uploadify.swf',
                'uploader': '/SystemSetManager/Upload/UploadImage',
                'buttonText': '选择图片',
                'selfDefineId': i,
                'combineChar': combineChar,
                'fileTypeDesc': 'Image Files',
                'fileTypeExts': '*.jpg;*.bmp;*.png;*.gif',
                'onUploadSuccess': function (file, data, response) {
                    var json = $.parseJSON(data);
                    if (json.success) {
                        var i = this.settings.selfDefineId;
                        var combineChar = this.settings.combineChar;
                        $('#div_filename' + combineChar + i).html("<span ><img id='img" + combineChar + i + "'  style=\"height:150px;width:150px;\" src=\"" + json.extension.fileFullPath + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.ImageUploadControl.delImage(" + i + "," + combineChar + ")\">删除</a></span>");
                        $("#ImageUrl" + combineChar + i).val(json.extension.fileFullPath);

                    } else {
                        alert(json.msg);
                    }
                }
            });

        },
        initHtml: function (i, combineChar) {
            i = i == undefined ? "" : i;
            combineChar = combineChar == undefined ? "" : combineChar;

            var html = '<div style="display: none">\
                           <input id="ImageUrl' + combineChar + i + '" name="ImageUrl' + combineChar + i + '" type="text" />\
                       </div>\
                       <div id="div_filename' + combineChar + i + '" style="height: 150px; width: 150px">\
                       </div>\
                       <div style="width: 150px;">\
                           <input id="file_upload' + combineChar + i + '"  type="file" />\
                       </div>';

            if ($("#div_ImageArea" + combineChar + i).length > 0) {
                $("#div_ImageArea" + combineChar + i).html(html);
            }
        },
        delImage: function (i, combineChar) {
            i = i == undefined ? "" : i;
            combineChar = combineChar == undefined ? "" : undefined;

            $("#img" + combineChar + i).parent().remove();
            $("#ImageUrl" + combineChar + i).val("");
        },
        bindData: function (imageUrl, i, combineChar) {
            i = i == undefined ? "" : i;
            combineChar = combineChar == undefined ? "" : undefined;

            $('#div_filename' + combineChar + i).html("<span ><img id='img" + combineChar + i + "' name=\"listP\" style=\"height:150px;width:150px;\" src=\"" + imageUrl + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.Product.ImageUploadControl.delImage(" + i + "," + combineChar + ")\">删除</a></span>");
            $("#ImageUrl" + combineChar + i).val(imageUrl);
        }

    }
}
)();





