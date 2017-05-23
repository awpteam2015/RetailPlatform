
var pro = pro || {};
(function () {
    pro.RiverProblemApply = pro.RiverProblemApply || {};
    pro.RiverProblemApply.HdPage = pro.RiverProblemApply.HdPage || {};
    pro.RiverProblemApply.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.RiverProblemApply.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.RiverProblemApply.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.RiverProblemApply.ListPage.closeTab("");
            });

            $("#btn_RiverChoose").click(function () {
                pro.RiverProblemApply.HdPage.openPage();
            });

            var bindEntity;
            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }

                if (bindEntity["PicUrl1"]) {
                    $("#div_img").append('<div  style="float: left;width:200px;padding-left: 20px"><a href="' + bindEntity["PicUrl1"] + '" target="_blank"><img src="' + bindEntity["PicUrl1"] + '" width="200"  height="200"></a></div>');
                }
                if (bindEntity["PicUrl2"]) {
                    $("#div_img").append('<div  style="float: left;width:200px;padding-left: 20px"><a href="' + bindEntity["PicUrl2"] + '" target="_blank"><img src="' + bindEntity["PicUrl2"] + '" width="200"  height="200"></a></div>');
                }
                if (bindEntity["PicUrl3"]) {
                    $("#div_img").append('<div  style="float: left;width:200px;padding-left: 20px"><a href="' + bindEntity["PicUrl3"] + '" target="_blank"><img src="' + bindEntity["PicUrl3"] + '" width="200"  height="200"></a></div>');
                }


                if (bindEntity["FinishPicUrl"]) {
                    $("#div_img2").append('<div  style="float: left;width:200px;padding-left: 20px"><a href="' + bindEntity["FinishPicUrl"] + '" target="_blank"><img src="' + bindEntity["FinishPicUrl"] + '" width="200"  height="200"></a></div>');
                }
                if (bindEntity["FinishPicUrl2"]) {
                    $("#div_img2").append('<div  style="float: left;width:200px;padding-left: 20px"><a href="' + bindEntity["FinishPicUrl2"] + '" target="_blank"><img src="' + bindEntity["FinishPicUrl2"] + '" width="200"  height="200"></a></div>');
                }
                if (bindEntity["FinishPicUrl3"]) {
                    $("#div_img2").append('<div  style="float: left;width:200px;padding-left: 20px"><a href="' + bindEntity["FinishPicUrl3"] + '" target="_blank"><img src="' + bindEntity["FinishPicUrl3"] + '" width="200"  height="200"></a></div>');
                }

                this.setRelationInfo(bindEntity['RiverId'], bindEntity);
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }


            if ($("#DbList").val()) {

                var html = "<table class=\"mtable\">";
                html += "<tr><td style='width:300px'>督办历史</td><td  style='width:100px'>督办人</td><td style='width:100px'>时间</td><tr>";

                var bindEntity2 = JSON.parse($("#DbList").val());
                $(bindEntity2).each(
                    function (item, row) {
                        html += "<tr><td>" + row.DbRemark + "</td><td>" + row.UserName + "</td><td>" + row.CreateTime + "</td><tr>";
                    }
                );
                html += "</table>";
                $("#div_db").html(html);
            }

            //$('#RiverId').combobox({
            //    required: true,
            //    editable: true,
            //    valueField: 'PkId',
            //    textField: 'RiverName',
            //    url: '/RiverManager/River/GetListNoPage',
            //    onLoadSuccess: function () {
            //        if (pro.commonKit.getUrlParam("PkId") > 0) {
            //            $("#RiverId").combobox('setValue', bindEntity['RiverId']);
            //            setRelationInfo(bindEntity['RiverId']);
            //            //$("[name=DepartmentCode]").val(bindEntity["DepartmentCode"]);
            //            //$("[name=UserCode]").val(bindEntity["UserCode"]);


            //        }
            //    },
            //    onChange: function (newValue, oldValue) {

            //        setRelationInfo(newValue);

            //    }
            //});


        },
        openPage: function () {
            $("#iFrame_RiverPage").attr("src", "/RiverManager/River/PageList?datetime=" + Math.random());
            $('#div_RiverPage').dialog({
                width: 500,
                height: 500,
                title: "河道选择",
                modal: true,
                resizable: false
            });
        },
        setRelationInfo: function (riverId, bindEntity,rivername) {

            $("[name=RiverId]").val(riverId);
            $("[name=RiverName]").html(rivername);

            abp.ajax({
                url: "/RiverManager/River/GetRiverDetail?PkId=" + riverId
            }).done(
                    function (dataresult, data) {

                        $("#DepartmentCode").html('<option value="' + dataresult.DepartmentCode + '">' + dataresult.DepartmentName + '</option>');

                        var html = "";
                        $.each(dataresult.RiverOwerList, function (i, item) {
                            html += '<option value="' + item.UserCode + '">' + item.UserName + '</option>';
                        });

                        $("#UserCode").html(html);


                        if (bindEntity != null) {
                            $("[name=DepartmentCode]").val(bindEntity["DepartmentCode"]);
                            $("[name=UserCode]").val(bindEntity["UserCode"]);
                            $("[name=RiverId]").val(bindEntity["RiverId"]);
                            $("[name=RiverName]").html(bindEntity["RiverName"]);
                        }


                    }
                );
        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            postData.RequestEntity.DepartmentName = $('#DepartmentCode').find('option:selected').text();
            postData.RequestEntity.UserName = $('#UserCode').find('option:selected').text();
            postData.RequestEntity.RiverName = $('#RiverName').html();
            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/RiverManager/RiverProblemApply/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.RiverProblemApply.ListPage.closeTab();
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
                        Title: { required: true },
                        Des: { required: true },
                        ProblemType: { required: true },
                        PicUrl: { required: true },
                        DepartmentCode: { required: true },
                        RiverId: { required: true },
                        RiverName: { required: true },
                        //UserCode: { required: true },
                        //UserName: { required: true },

                        State: { required: true },
                        DepartmentRemark: { required: true },
                        DepartmentOpTime: { required: true },
                        TopDepartmentRemark: { required: true },
                        TopDepartmentOpTime: { required: true },
                        FinishOpTime: { required: true },
                        FinishRemark: { required: true },
                        ReturnRemark: { required: true },
                        ReturnOpTime: { required: true },
                        IsExposure: { required: true },
                        ExposureLever: { required: true },
                        IsSendMessage: { required: true },
                        IsActive: { required: true },
                        CreatorUserName: { required: true },
                        CreatorUserCode: { required: true },
                        CreationTime: { required: true },
                        LastModifierUserName: { required: true },
                        LastModifierUserCode: { required: true },
                        LastModificationTime: { required: true },
                        Remark: { required: true },
                        DeleteRemark: { required: true },
                        IsDeleted: { required: true },
                        DeleteUserName: { required: true },
                        DeleteUserCode: { required: true },
                        DeleteTime: { required: true }
                    },
                    messages: {
                        PkId: "必填!",
                        Title: "必填!",
                        Des: "问题描述必填!",
                        ProblemType: "问题类型 1日常巡河 2问题上报必填!",
                        PicUrl: "图片地址 多个必填!",
                        DepartmentCode: "所属部门必填!",
                        RiverId: "必填!",
                        RiverName: "必填!",
                        UserCode: "必填!",
                        UserName: "必填!",
                        Coords: "坐标必填!",
                        State: "问题状态 1.部门待处理 2河长待处理 3完结 4重新申请作废 5回退部门待处理 必填!",
                        DepartmentRemark: "部门处理意见必填!",
                        DepartmentOpTime: "部门操作时间必填!",
                        TopDepartmentRemark: "治水办处理意见必填!",
                        TopDepartmentOpTime: "治水办处理意见时间必填!",
                        FinishOpTime: "河长结束问题时间必填!",
                        FinishRemark: "处理结果必填!",
                        ReturnRemark: "回退说明必填!",
                        ReturnOpTime: "河长回退问题时间必填!",
                        IsExposure: "是否曝光必填!",
                        ExposureLever: "曝光等级必填!",
                        IsSendMessage: "是否已发送短信必填!",
                        IsActive: "是否有效必填!",
                        CreatorUserName: "必填!",
                        CreatorUserCode: "创建人必填!",
                        CreationTime: "创建时间必填!",
                        LastModifierUserName: "必填!",
                        LastModifierUserCode: "修改人必填!",
                        LastModificationTime: "修改时间必填!",
                        Remark: "备注必填!",
                        DeleteRemark: "删除原因必填!",
                        IsDeleted: "是否删除必填!",
                        DeleteUserName: "删除人必填!",
                        DeleteUserCode: "删除人编码必填!",
                        DeleteTime: "删除时间必填!",
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
    pro.RiverProblemApply.HdPage.initPage();
});


