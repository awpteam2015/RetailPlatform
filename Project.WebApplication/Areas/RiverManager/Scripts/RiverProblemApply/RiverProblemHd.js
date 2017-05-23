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

            function setRelationInfo(riverId) {
                abp.ajax({
                    url: "/RiverManager/River/GetRiverDetail?PkId=" + riverId
                }).done(
                        function (dataresult, data) {
                            $("#DepartmentCode").html('<option value="' + dataresult.DepartmentCode + '">' + dataresult.DepartmentName + '</option>');


                            //var html = "";
                            //$.each(dataresult.RiverOwerList, function (i, item) {
                            //    html += '<option value="' + item.UserCode + '">' + item.UserName + '</option>';
                            //});

                            //$("#UserCode").html(html);


                            if (bindEntity != null) {
                                $("[name=DepartmentCode]").val(bindEntity["DepartmentCode"]);
                              //  $("[name=UserCode]").val(bindEntity["UserCode"]);
                            }

                        }
                    );
            }

          
         


            //$('#RiverId').combobox({
            //    required: true,
            //    editable: false,
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


            var bindEntity = "";

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                 bindEntity = JSON.parse($("#BindEntity").val());
                 for (var filedname in bindField) {

                     $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }

                 this.setRelationInfo(bindEntity['RiverId'], bindEntity);
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }


            $('#ExposureArea').combobox({
                editable: true,
                multiple: true,
                valueField: 'KeyValue',
                textField: 'KeyName',
                data: [{
                    KeyValue: '微信',
                    KeyName: '微信'
                }, {
                    KeyValue: 'App',
                    KeyName: 'App'
                }],
                //url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=RiverRank',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        if (bindEntity['ExposureArea']) {
                            $("#ExposureArea").combobox('setValues', bindEntity['ExposureArea']);
                        }
                       
                    }
                }
            });


            if ($('#ZfCsUserCode').length > 0) {
                $('#ZfCsUserCode').combobox({
                    editable: false,
                    valueField: 'UserCode',
                    textField: 'UserName',
                    url: '/RiverManager/RiverProblemApply/GetAllRoleUserList?departmentCode=' + bindEntity['DepartmentCode'],
                    onLoadSuccess: function () {
                        if (pro.commonKit.getUrlParam("PkId") > 0) {
                            $("#ZfCsUserCode").combobox('setValue', bindEntity['ZfCsUserCode']);
                        }
                    }
                });
            }

            if ($('#DbCsUserCode').length > 0) {
                $('#DbCsUserCode').combobox({
                    editable: false,
                    valueField: 'UserCode',
                    textField: 'UserName',
                    url: '/RiverManager/RiverProblemApply/GetAllRoleUserList?departmentCode=' + bindEntity['DepartmentCode'],
                    onLoadSuccess: function () {
                        if (pro.commonKit.getUrlParam("PkId") > 0) {
                            $("#DbCsUserCode").combobox('setValue', bindEntity['DbCsUserCode']);
                        }
                    }
                });
            }

            if ($('#DbUserCode').length > 0) {
                $('#DbUserCode').combobox({
                    editable: false,
                    valueField: 'UserCode',
                    textField: 'UserName',
                    url: '/RiverManager/RiverProblemApply/GetRoleUserList?roleName=2&departmentCode=' + bindEntity['DepartmentCode'],
                    onLoadSuccess: function () {
                        if (pro.commonKit.getUrlParam("PkId") > 0) {
                            $("#DbUserCode").combobox('setValue', bindEntity['DbUserCode']);
                        }
                    }
                });
            }


            if ($('#UserCode').length > 0) {
                $('#UserCode').combobox({
                    editable: false,
                    valueField: 'UserCode',
                    textField: 'UserName',
                    url: '/RiverManager/RiverProblemApply/GetRoleUserList?roleName=1&departmentCode=' + bindEntity['DepartmentCode'],
                    onLoadSuccess: function () {
                        if (pro.commonKit.getUrlParam("PkId") > 0) {
                            $("#UserCode").combobox('setValue', bindEntity['UserCode']);
                        }
                    }
                });
            }




            $("[name=UrgentRemark]").val("");
        },
        setRelationInfo: function (riverId, bindEntity, rivername) {

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
            postData.RequestEntity.DepartmentName =  $('#DepartmentCode').find('option:selected').text();
            postData.RequestEntity.UserName =  $('#UserCode').find('option:selected').text();
            postData.RequestEntity.RiverName = $('#RiverName').html();
            if ($('#ExposureArea').length > 0) {
                postData.RequestEntity.ExposureArea = $('#ExposureArea').combobox('getText');
            }
         
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
                        UrgentRemark: { required: true },
                        DepartmentRemark: { required: true },
                        TopDepartmentRemark: { required: true },
                        FinishRemark: { required: true },
                        ReturnRemark: { required: true },
                        DeleteRemark: { required: true },
                        IsExposure: { required: true },
                        ExposureLever: { required: true }
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
                        DeleteTime: "删除时间必填!"
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


