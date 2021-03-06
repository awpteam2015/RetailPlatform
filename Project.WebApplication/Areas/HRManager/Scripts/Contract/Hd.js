﻿var pro = pro || {};
(function () {
    pro.Contract = pro.Contract || {};
    pro.Contract.HdPage = pro.Contract.HdPage || {};
    pro.Contract.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Contract.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Contract.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.Contract.ListPage.closeTab("");
            });


            $('#EmployeeCode').combobox({
                url: '/HRManager/EmployeeInfo/GetAllList2',
                valueField: 'EmployeeCode',
                textField: 'EmployeeNameAndEmployeeCode',
                onSelect: function(rec){
                    var postData = {};
                    postData.employeeCode = rec.EmployeeCode;

                    abp.ajax({
                        url: "/HRManager/EmployeeInfo/GetEmployeeInfo",
                        data: JSON.stringify(postData)
                    }).done(
                     function (dataresult, data) {
                         $("#DepartmentCode").val(dataresult.DepartmentCode);
                         $("#DepartmentName").val(dataresult.DepartmentName);

                         $("#DepartmentCode_span").html(dataresult.DepartmentCode);
                         $("#DepartmentName_span").html(dataresult.DepartmentName);

                         $("#IdentityCardNo").val(dataresult.CertNo);
                         $("#SecondParty").val(dataresult.EmployeeName);
                         $("#SecondParty_span").html(dataresult.EmployeeName);
                     }
                 ).fail(
                  function (errordetails, errormessage) {
                      $("#EmployeeCode").val("");
                  }
                 );
                }
            });

            //$("#EmployeeCode").change(function () {

            //    var postData = {};
            //    postData.employeeCode = $("#EmployeeCode").val();

            //    abp.ajax({
            //        url: "/HRManager/EmployeeInfo/GetEmployeeInfo",
            //        data: JSON.stringify(postData)
            //    }).done(
            //     function (dataresult, data) {
            //         $("#DepartmentCode").val(dataresult.DepartmentCode);
            //         $("#DepartmentName").val(dataresult.DepartmentName);

            //         $("#DepartmentCode_span").html(dataresult.DepartmentCode);
            //         $("#DepartmentName_span").html(dataresult.DepartmentName);

            //         $("#IdentityCardNo").val(dataresult.CertNo);
            //         $("#SecondParty").val(dataresult.EmployeeName);
            //         $("#SecondParty_span").html(dataresult.EmployeeName);
            //     }
            // ).fail(
            //  function (errordetails, errormessage) {
            //      $("#EmployeeCode").val("");
            //  }
            // );

            //});



            pro.DepartmentControl.init({ controlId: "FirstParty" });


            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }

                $("#DepartmentCode_span").html(bindEntity.DepartmentCode);
                $("#DepartmentName_span").html(bindEntity.DepartmentName);
                $("#State").html(bindEntity.Attr_State);
                $("#SecondParty_span").html(bindEntity.SecondParty);

                if (bindEntity.FileUrl!="") {
                    $('#div_filename').html("<span ><a name=\"listP\" target=\"_blank\" href=\"" + bindEntity.FileUrl +"/"+ bindEntity.FileName + "\">合同下载</a> </span>");
                }


                var State = pro.commonKit.getUrlParam("State");
                if (State == "2" || State=="4") {
                    $("#EmployeeCode").attr("disabled", "disabled");
                    $("#IdentityCardNo").attr("disabled", "disabled");
                    $("#FirstParty").attr("disabled", "disabled");
                    $("#SecondParty").attr("disabled", "disabled");
                    $("#ContractContent").attr("disabled", "disabled");
                    $('#FirstParty').combotree({ disabled: true });
                
                    
                }

                if (State == "4") {
                    $("#ContractNo").attr("disabled", "disabled");
                    $("#BeginDate").attr("disabled", "disabled");
                    $("#EndDate").attr("disabled", "disabled");
                }
                $('#FirstParty').combotree("setValue", bindEntity.FirstParty);
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

            if (pro.commonKit.getUrlParam("ParentId") != "") {
                postData.RequestEntity.ParentId = pro.commonKit.getUrlParam("ParentId");
                postData.RequestEntity.State = pro.commonKit.getUrlParam("State");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/Contract/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Contract.ListPage.closeTab();
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
                        EmployeeCode: { required: true },
                        BeginDate: { required: true },
                        EndDate: { required: true },

                        ContractNo: { required: true },
                     
                        IdentityCardNo: { required: true }
                    },
                    messages: {
                        EmployeeCode: "工号必填!",
                        DepartmentCode: "部门编号必填!",
                        DepartmentName: "部门名称必填!",
                        BeginDate: "开始时间必填!",
                        EndDate: "结束时间必填!",
                        Remark: "备注必填!",
                        CreatorUserCode: "创建人必填!",
                        CreateTime: "创建时间必填!",
                        LastModifierUserCode: "修改人必填!",
                        LastModificationTime: "最后修改时间必填!",
                        IsDelete: "必填!",
                        State: "1 最初签订 2续订 3 变更 4 终止 必填!",
                        IsActive: "是否有效 1有效 2无效必填!",
                        ContractNo: "合同编号必填!",
                        FirstParty: "甲方必填!",
                        SecondParty: "乙方必填!",
                        ContractContent: "合同内容必填!",
                        IdentityCardNo: "身份证必填!"
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
    pro.Contract.HdPage.initPage();
});


