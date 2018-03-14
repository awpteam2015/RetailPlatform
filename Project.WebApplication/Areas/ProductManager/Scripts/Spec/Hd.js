var pro = pro || {};
(function () {
    pro.Spec = pro.Spec || {};
    pro.Spec.HdPage = pro.Spec.HdPage || {};

    pro.Spec.HdPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false)
            };
        },
        initPage: function () {


            var initObj = this.init();
            var tabObj = initObj.tabObj;
            var gridObj = initObj.gridObj;
            gridObj.grid({
                url: '/ProductManager/Spec/GetSpecValueList?SpecId=' + pro.commonKit.getUrlParam("PkId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', title: '', hidden: true, width: 100,height:150,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("PkId", value);
                            }
                        },
                        {
                            field: 'SpecValueName',
                            title: '规格值名称',
                            width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("SpecValueName_" + row.PkId, value);
                            }
                        },
                         {
                             field: 'ImageUrl',
                             title: '图片地址',
                             width: 200,
                             formatter: function (value, row, index) {
                                 var html = '<div style="display:none"> <input id="ImageUrl_' + row.PkId + '" name="ImageUrl_' + row.PkId + '" type="text" /></div>\
                        <div  id="div_filename_' + row.PkId + '" style="height: 30px; width: 30px">\
                        </div>\
                            <input id="file_upload_' + row.PkId + '" name="file_upload" type="file" />\
                       </div>';
                                 return html;
                             }
                         },
                        {
                            field: 'Sort',
                            title: '排序',
                            width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("Sort_" + row.PkId, value);
                            }
                        }

                    ]
                ],
                pagination: false,
                onLoadSuccess: function (data) {

                    $.each(data.rows, function (key, obj) {
                        pro.ImageUploadControl.init(obj.PkId);

                        $('#div_filename_' + obj.PkId).html("<span ><img id='img_" + obj.PkId + "' name=\"listP\" style=\"height:30px;width:30px;\" src=\"" + obj.ImageUrl + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"pro.Product.ProductImageHd.delImage(" + obj.PkId + ")\">删除</a></span>");

                        $("#ImageUrl_" + obj.PkId).val(obj.ImageUrl);

                    });
                }
            }
            );

            $("#btnAdd_ToolBar").click(function () {
                gridObj.insertRow({
                    PkId: gridObj.PkId,
                    FunctionDetailCode: ""
                });

                //console.log(JSON.stringify($("#datagrid").datagrid('getRows')));
                //console.log(gridObj.PkId + 1);
               
                pro.ImageUploadControl.init(gridObj.PkId+1);
                $("#datagrid").datagrid('selectRecord', gridObj.PkId + 1);
            });


            $("#btnDel_ToolBar").click(function () {
                gridObj.delRow();
            });


            $("#btnAdd").click(function () {
                pro.Spec.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Spec.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.Spec.ListPage.closeTab("");
            });

            var bindEntity;
            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }



            $('#SpecType').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/SystemSetManager/Dictionary/GetList_Combobox?ParentKeyCode=SpecType',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#SpecType").combobox('setValue', bindEntity['SpecType']);
                    }
                }
            });


            $('#ShowType').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/SystemSetManager/Dictionary/GetList_Combobox?ParentKeyCode=ShowType',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#ShowType").combobox('setValue', bindEntity['ShowType']);
                    }
                }
            });

        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            postData.RequestEntity.SpecTypeName = $("#SpecType").combobox('getText');
            postData.RequestEntity.ShowTypeName = $("#ShowType").combobox('getText');


            pro.submitKit.config.columnPkidName = "PkId";
            pro.submitKit.config.columns = ["SpecValueName", "ImageUrl", "Sort"];
            postData.RequestEntity.SpecValueList = pro.submitKit.getRowJson();

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/ProductManager/Spec/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Spec.ListPage.closeTab();
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
                        PkId: { required: true },
                        SpecName: { required: true },
                        Remark: { required: true },
                        SpecType: { required: true },
                        ShowType: { required: true }
                    },
                    messages: {
                        PkId: "必填!",
                        SpecName: "必填!",
                        Remark: "必填!",
                        SpecType: "0text 1image必填!",
                        ShowType: "0平铺 1下拉框必填!"
                    },
                    errorPlacement: function (error, element) {
                        pro.commonKit.errorPlacementHd(error, element);
                    },
                    debug: false
                });
            },
            logicValidate: function () {

                if (!$("#SpecType").combogrid("isValid")) {
                    return false;
                }

                if (!$("#ShowType").combogrid("isValid")) {
                    return false;
                }
                return true;
            }
        },
        //openImageUploadPage: function (i) {

        //    var html = ' <div id="div_UploadImage">\
        //    <iframe id="iFrame_UploadImage" src="/SystemSetManager/Upload/UploadImagePage?Pkid='+i+'" frameborder="0" width="100%" height="100%"></iframe>\
        //</div>';

        //    $("#div_Dialog").html(html);

        //    $('#div_UploadImage').dialog({
        //        width: 400,
        //        height: 400,
        //        title: "上传图片",
        //        modal: true,
        //        resizable: false
        //    });

        //},
        addTab: function (subtitle, url) {

        }

    };
})();



$(function () {
    pro.Spec.HdPage.initPage();
});


