var pro = pro || {};
(function () {
    pro.PageContent = pro.PageContent || {};
    pro.PageContent.HdPage = pro.PageContent.HdPage || {};
    pro.PageContent.HdPage = {
        initPage: function () {
            UE.getEditor('Description1', {
                initialFrameHeight: 320,
                autoHeightEnabled: false,
                initialFrameWidth: 800
            });

            UE.getEditor('Description2', {
                initialFrameHeight: 320,
                autoHeightEnabled: false,
                initialFrameWidth: 800
            });

            UE.getEditor('Description3', {
                initialFrameHeight: 320,
                autoHeightEnabled: false,
                initialFrameWidth: 800
            });


            pro.ImageUploadControl.init(1);
            pro.ImageUploadControl.init(2);
            pro.ImageUploadControl.init(3);



            $("#btnAdd").click(function () {
                pro.PageContent.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.PageContent.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.PageContent.ListPage.closeTab("");
            });
            var bindEntity = "";
            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                 bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
 
                if (bindEntity["ImageUrl1"]!="") {
                    pro.ImageUploadControl.bindData( bindEntity["ImageUrl1"],1);
                }

                if (bindEntity["ImageUrl2"] != "") {
                    pro.ImageUploadControl.bindData( bindEntity["ImageUrl2"],2);
                }

                if (bindEntity["ImageUrl3"] != "") {
                    pro.ImageUploadControl.bindData( bindEntity["ImageUrl3"],3);
                }


            }

            pro.PageContentCategoryControl.init({
                required: true,
                onChange: function(newValue, oldValue) {

                    pro.PageContent.HdPage.onChangeCategory(newValue);
                },
                onLoadSuccess: function () {
                    if ($("#BindEntity").val()) {
                        pro.PageContent.HdPage.onChangeCategory(bindEntity["PageContentCategoryId"]);
                    }

                }
            });

        },
        onChangeCategory: function (newValue) {

            abp.ajax({
                url: "/ContentManager/PageContentCategory/GetPageContentCategoryEntity?PkId=" + newValue
            }).done(
                       function (dataresult, data) {

                           if (dataresult != null) {

                               if (dataresult.IsShowTitle1 == 1) {
                                   $("#tr_Title1").css("display", "");
                                   if (dataresult.TitleOtherName1 != "") {
                                       $("#span_Title1").html(dataresult.TitleOtherName1);
                                   }
                               }

                               if (dataresult.IsShowTitle2 == 1) {
                                   $("#tr_Title2").css("display", "");
                                   if (dataresult.TitleOtherName2 != "") {
                                       $("#span_Title2").html(dataresult.TitleOtherName2);
                                   }
                               }

                               if (dataresult.IsShowTitle3 == 1) {
                                   $("#tr_Title3").css("display", "");
                                   if (dataresult.TitleOtherName3 != "") {
                                       $("#span_Title3").html(dataresult.TitleOtherName3);
                                   }
                               }

                               if (dataresult.IsShowDescription1 == 1) {
                                   $("#tr_Description1").css("display", "");
                                   if (dataresult.DescriptionOtherName1 != "") {
                                       $("#span_Description1").html(dataresult.DescriptionOtherName1);
                                   }
                               }

                               if (dataresult.IsShowDescription2 == 1) {
                                   $("#tr_Description2").css("display", "");
                                   if (dataresult.DescriptionOtherName2 != "") {
                                       $("#span_Description2").html(dataresult.DescriptionOtherName2);
                                   }
                               }

                               if (dataresult.IsShowDescription3 == 1) {
                                   $("#tr_Description3").css("display", "");
                                   if (dataresult.DescriptionOtherName3 != "") {
                                       $("#span_Description3").html(dataresult.DescriptionOtherName3);
                                   }
                               }

                               if (dataresult.IsShowImageUrl1 == 1) {
                                   $("#tr_ImageUrl1").css("display", "");
                                   if (dataresult.ImageUrlOtherName1 != "") {
                                       $("#span_ImageUrl1").html(dataresult.ImageUrlOtherName1);
                                   }
                               }

                               if (dataresult.IsShowImageUrl2 == 1) {
                                   $("#tr_ImageUrl2").css("display", "");
                                   if (dataresult.ImageUrlOtherName2 != "") {
                                       $("#span_ImageUrl2").html(dataresult.ImageUrlOtherName2);
                                   }
                               }

                               if (dataresult.IsShowImageUrl3 == 1) {
                                   $("#tr_ImageUrl3").css("display", "");
                                   if (dataresult.ImageUrlOtherName3 != "") {
                                       $("#span_ImageUrl3").html(dataresult.ImageUrlOtherName3);
                                   }
                               }


                           }

                       }
                   );
        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            postData.RequestEntity.PageContentCategoryName = $('#PageContentCategoryId').combotree("getText");

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/ContentManager/PageContent/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.PageContent.ListPage.closeTab();
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
                        PageContentCategoryId: { required: true },
                        //Title1: { required: true  },
                        //Title2: { required: true  },
                        //Title3: { required: true  },

                        //Description1: { required: true  },
                        //Description2: { required: true  },
                        //Description3: { required: true  },
                        //ImageUrl1: { required: true  },
                        //ImageUrl2: { required: true  },
                        //ImageUrl3: { required: true  },
                        //DeletionTime: { required: true  },
                        //DeleterUserCode: { required: true  },
                        //IsDeleted: { required: true  },
                        //LastModificationTime: { required: true  },
                        //LastModifierUserCode: { required: true  },
                        //CreationTime: { required: true  },
                        //CreatorUserCode: { required: true  },
                    },
                    messages: {
                        PageContentCategoryId: "必填!",
                        Title1: "必填!",
                        Title2: "必填!",
                        Title3: "必填!",
                        Description1: "必填!",
                        Description2: "必填!",
                        Description3: "必填!",
                        ImageUrl1: "必填!",
                        ImageUrl2: "必填!",
                        ImageUrl3: "必填!",
                        DeletionTime: "删除时间必填!",
                        DeleterUserCode: "删除人必填!",
                        IsDeleted: "是否删除必填!",
                        LastModificationTime: "修改时间必填!",
                        LastModifierUserCode: "修改人必填!",
                        CreationTime: "创建时间必填!",
                        CreatorUserCode: "创建人必填!",
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
    pro.PageContent.HdPage.initPage();
});


