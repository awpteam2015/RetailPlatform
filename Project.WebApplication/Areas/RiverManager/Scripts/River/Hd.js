function chooseRiverChief(userCode, userName, isInit) {

    if (isInit) {
        $("#div_riverSelect").append('<div id="div_' + userCode + '" style="float:left;width:100px"> <input name="UserCode" type="hidden" value="' + userCode + '" /> ' + userName + '<input  value="删除" type="button" onclick="del(\'' + userCode + '\',\'' + userName + '\')" /></div>');
        return true;
    } else {
        var validate = true;
        $("input[name=UserCode]").each(
                    function (i) {
                        if ($(this).val() == userCode) {
                            validate = false;
                        }
                    }
                );
        if (!validate) {
            $.alertExtend.infoOp("请不要重复选择！");
            return false;
        }

        var postData = {};
        postData.RiverId = pro.commonKit.getUrlParam("PkId");
        postData.UserCode = userCode;
        postData.UserName = userName;
        postData.IsCheck = 1;

        abp.ajax({
            contentType: abp.ajax.contentTypeForm,
            url: "/RiverManager/River/SetRiverChief",
            data: postData
        }).done(
        function (dataresult, data) {

            $("#div_riverSelect").append('<div id="div_' + userCode + '" style="float:left;width:100px"> <input name="UserCode" type="hidden" value="' + userCode + '" /> ' + userName + '<input  value="删除" type="button" onclick="del(\'' + userCode + '\',\'' + userName + '\')" /></div>');

        }
        ).fail(
        function (errordetails, errormessage) {
            // $.alertExtend.error(errormessage);
        }
        );
    }




}

function del(userCode, userName) {


    var postData = {};
    postData.RiverId = pro.commonKit.getUrlParam("PkId");
    postData.UserCode = userCode;
    postData.UserName = userName;
    postData.IsCheck = 0;

    abp.ajax({
        contentType: abp.ajax.contentTypeForm,
        url: "/RiverManager/River/SetRiverChief",
        data: postData
    }).done(
    function (dataresult, data) {

        $("#div_" + userCode).remove();

    }
    ).fail(
    function (errordetails, errormessage) {
        // $.alertExtend.error(errormessage);
    }
    );
}



var pro = pro || {};
(function () {
    pro.River = pro.River || {};
    pro.River.HdPage = pro.River.HdPage || {};
    pro.River.HdPage = {
        init: function () {
            return {
                gridObj: new pro.GridBase("#datagrid", false)
            };
        },
        initPage: function () {
            var initObj = this.init();
            initObj.gridObj.grid({
                url: '/PermissionManager/UserInfo/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [
                    [
                         {
                             field: 'PkId', title: '选择', width: 100, formatter: function (value, row) {
                                 return '<input  type="button" value="选择" onclick="chooseRiverChief(\'' + row.UserCode + '\',\'' + row.UserName + '\')"/>';
                             }
                         },
                          { field: 'UserCode', title: '河长手机', width: 100 },
         { field: 'UserName', title: '河长姓名', width: 100 },
           { field: 'Jb', title: '河长级别', width: 100 },
         //{ field: 'Email', title: '电子邮件', width: 100 },
         //{ field: 'Mobile', title: '手机号', width: 100 },
         //{ field: 'Tel', title: '家庭电话', width: 100 },
         { field: 'Duty', title: '职务', width: 100 }
                    ]
                ],
                //onCheck: function (index, row) {

                //},
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
              );

         




            $("#btnSearch").click(function () {
                initObj.gridObj.search();
            });

            $("#btnAdd").click(function () {
                pro.River.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.River.HdPage.submit("Edit");
            });

            $("#btnSave").click(function () {
                pro.River.HdPage.submit("SetRiverChief");
            });

            $("#btnClose").click(function () {
                parent.pro.River.ListPage.closeTab("");
            });

            $('#RiverRank').combobox({
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=RiverRank',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#RiverRank").combobox('setValue', bindEntity['RiverRank']);
                    }
                }
            });
            $('#DepartmentCode').combotree({
                required: true,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            });


            $('#RiverRank').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=RiverRank',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#RiverRank").combobox('setValue', bindEntity['RiverRank']);
                    }
                }
            });


            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }

                if (bindEntity["PicUrl"]) {
                    $("#a_PicUrl").css("display", "");

                    $("#a_PicUrl").attr("href", bindEntity["PicUrl"]);
                }


                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }


            if ($("#UserList").length>0&&$("#UserList").val() != "undefined") {
                var bindEntity2 = JSON.parse($("#UserList").val());
                $(bindEntity2.rows).each(
                    function (item, row) {
                        chooseRiverChief(row.UserCode, row.UserName, true);
                    }
                );
            }

        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            postData.RequestEntity.DepartmentName = $('#DepartmentCode').combotree('getText');
            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            var selectUserCode = "";
            $("input[name=UserCode]").each(
                function (i) {
                    selectUserCode += $(this).val() + ",";
                }
            );

            if (selectUserCode != "") {
                postData.RequestEntity.Attr_SelectUserCodes = selectUserCode.substring(0, selectUserCode.length - 1);

            }


            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/RiverManager/River/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.River.ListPage.closeTab();
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
                        RiverName: { required: true },
                        RiverRank: { required: true }
                    },
                    messages: {
                        PkId: "必填!",
                        RiverName: "河道名称必填!",
                        RiverRank: "河道等级必填!",
                        RiverArea: "河道范围必填!",
                        RiverLength: "长度必填!",
                        RiverCrossArea: "流经乡（镇）必填!",
                        Coords: "坐标必填!",
                        IsActive: "是否有效必填!",
                        CreatorUserCode: "创建人必填!",
                        CreationTime: "创建时间必填!",
                        LastModifierUserCode: "修改人必填!",
                        LastModificationTime: "修改时间必填!",
                        Remark: "备注必填!"
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
    pro.River.HdPage.initPage();
});


