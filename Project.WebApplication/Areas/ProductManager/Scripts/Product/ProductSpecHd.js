

var pro = pro || {};
(function () {
    pro.Product = pro.Product || {};
    pro.Product.ProductSpecHd = pro.Product.ProductSpecHd || {};
    pro.Product.ProductSpecHd = {
        opData: {
            speclist: [],
            chooseSpecList: [],
            skuList: [],
            skuHistoryList: []
        },
        init: function () {
            this.opData.speclist = JSON.parse($("#SpecVmList").val());

            this.initSpecChooseArea();
            this.initEvent();

            if ($("#BindEntity").val()) {
                var bindEntity = JSON.parse($("#BindEntity").val());
                pro.Product.ProductSpecHd.opData.skuList = bindEntity.GoodsEntityList;
                pro.Product.ProductSpecHd.opData.skuHistoryList = bindEntity.GoodsEntityList;
                pro.Product.ProductSpecHd.chooseSpec();
            }
        },
        initEvent: function () {

            $('#skuArea').on('change', "input[name^=GoodsPrice_]", function () {
                var spliteArr = $(this).attr("name").split('_');
                var combineId = spliteArr.splice(1, spliteArr.length).join('_');

                var newData = $(this).val();

                JSLINQ(pro.Product.ProductSpecHd.opData.skuList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.GoodsPrice = newData;
                    }
                });

                JSLINQ(pro.Product.ProductSpecHd.opData.skuHistoryList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.GoodsPrice = newData;
                    }
                });
            });


            $('#skuArea').on('change', "input[name^=GoodsStock_]", function () {
                var spliteArr = $(this).attr("name").split('_');
                var combineId = spliteArr.splice(1, spliteArr.length).join('_');

                var newData = $(this).val();

                JSLINQ(pro.Product.ProductSpecHd.opData.skuList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.GoodsStock = newData;
                    }
                });

                JSLINQ(pro.Product.ProductSpecHd.opData.skuHistoryList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.GoodsStock = newData;
                    }
                });
            });


            $('#skuArea').on('change', "input[name^=GoodsCode_]", function () {
                var spliteArr = $(this).attr("name").split('_');
                var combineId = spliteArr.splice(1, spliteArr.length).join('_');

                var newData = $(this).val();

                JSLINQ(pro.Product.ProductSpecHd.opData.skuList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.GoodsCode = newData;
                    }
                });

                JSLINQ(pro.Product.ProductSpecHd.opData.skuHistoryList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.GoodsCode = newData;
                    }
                });
            });


            $('#skuArea').on('change', "input[name^=SkuCode_]", function () {
                var spliteArr = $(this).attr("name").split('_');
                var combineId = spliteArr.splice(1, spliteArr.length).join('_');

                var newData = $(this).val();

                JSLINQ(pro.Product.ProductSpecHd.opData.skuList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.SkuCode = newData;
                    }
                });

                JSLINQ(pro.Product.ProductSpecHd.opData.skuHistoryList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.SkuCode = newData;
                    }
                });
            });


            $('#skuArea').on('click', "span[name^=BtnIsUse_]", function () {
                var spliteArr = $(this).attr("name").split('_');
                var combineId = spliteArr.splice(1, spliteArr.length).join('_');

                var text = $(this).html();

                if (text == "不启动") {
                    $(this).html("启动");
                    $("input[name=IsUse_" + combineId + "]").val(0);
                } else {
                    $(this).html("不启动");
                    $("input[name=IsUse_" + combineId + "]").val(1);
                }

                var newData = $("input[name=IsUse_" + combineId + "]").val();

                JSLINQ(pro.Product.ProductSpecHd.opData.skuList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.IsUse = newData;
                    }
                });

                JSLINQ(pro.Product.ProductSpecHd.opData.skuHistoryList).ForEach(function (skuRow) {
                    if (skuRow.CombineId == combineId) {
                        skuRow.IsUse = newData;
                    }
                });
            });


        },
        //初始化规格选择区域
        initSpecChooseArea: function () {

            var html = "";
            JSLINQ(this.opData.speclist).ForEach(function (spec) {

                var specValueHtml = "";
                JSLINQ(spec.SpecValueList).ForEach(function (specValue) {

                    var checkHmtl = "";
                    if (specValue.IsCheck == "1") {
                        checkHmtl = 'checked="true"';
                    }

                    specValueHtml += '<div style="width: 200px;float: left">\
                        <input id="spec_' + specValue.SpecId + '_' + specValue.SpecValueId + '" name="spec_' + specValue.SpecId + '"  ' + checkHmtl + ' type="checkbox" value="' + specValue.SpecValueId + '"    onchange="pro.Product.ProductSpecHd.chooseSpec(this)" />\
                        <input for="spec_' + specValue.SpecId + '_' + specValue.SpecValueId + '" type="text" value="' + specValue.SpecValueName + '"  orgValue="' + specValue.SpecValueName + '"/>\
                    </div>';


                });

                html += '<tr>\
                        <td>\
                           ' + spec.SpecName + '：\
                        </td>\
                        <td>\
                            <div style="width: 100%">\
                             '+ specValueHtml + '\
                             </div>\
                        </td>\
                        </tr>';

            });

            $(html).insertAfter("#specArea tr:eq(0)");

        },
        //选中规格
        chooseSpec: function () {
            pro.Product.ProductSpecHd.opData.chooseSpecList = [];
            $("input[name^=spec_]:checked").each(
                function () {
                    var pkid = $(this).attr("Id");
                    var pkidArr = pkid.split('_');
                    var specId = pkidArr[1];
                    var specValueId = pkidArr[2];

                    var specValueOtherName = $("input[for=" + pkid + "]").val();
                    var specValueName = $("input[for=" + pkid + "]").attr("orgValue");
                    var chooseSpecJson = { PkId: pkid, SpecId: specId, SpecValueId: specValueId, SpecValueName: specValueName, SpecValueOtherName: specValueOtherName };

                    pro.Product.ProductSpecHd.opData.chooseSpecList.push(chooseSpecJson);
                }
            );

            this.addSku();

        },
        //新增Sku
        addSku: function () {

            var needSpec = this.opData.speclist;
            var chooseSpec = JSLINQ(pro.Product.ProductSpecHd.opData.chooseSpecList).Distinct(function (item) {
                return item.SpecId;
            }).items;


            //存在组合的情况下
            if (needSpec.length == chooseSpec.length) {
                var combination = [];
                JSLINQ(chooseSpec).ForEach(
                    function (specId) {

                        var chooseSpecValueList = JSLINQ(pro.Product.ProductSpecHd.opData.chooseSpecList).Where(function (specValue) {
                            return specValue.SpecId == specId;
                        }).items;
                        combination.push(chooseSpecValueList);
                    }
                );

                pro.combinationCount.Count(combination);

                this.combineSku();

                this.addSkuHtml();
            } else {
                $("#skuArea").html("");
            }

        },
        //组合最终的sku集合
        combineSku: function () {

            //新sku
            var newSkuList = [];

            JSLINQ(pro.combinationCount.combinationResult).ForEach(function (spec) {
                var skuRow = { CombineId: "Sku", GoodsSpecValueList: spec, GoodsPrice: "", GoodsStock: "", GoodsCode: "", SkuCode: "", IsUse: 1, Op: 'Add' };
                JSLINQ(spec).ForEach(function (specValue) {
                    skuRow.CombineId += "_" + specValue.SpecValueId;
                });

                var orgRow = JSLINQ(pro.Product.ProductSpecHd.opData.skuHistoryList).Where(function (item) {
                    return item.CombineId == skuRow.CombineId;
                }).FirstOrDefault();


                if (orgRow != null) {
                    newSkuList.push(orgRow);
                } else {
                    newSkuList.push(skuRow);
                    pro.Product.ProductSpecHd.opData.skuHistoryList.push(skuRow);
                }

                //if (!ifExist) {
                //    skuList.push(skuRow);
                //}
            });

            pro.Product.ProductSpecHd.opData.skuList = newSkuList;


            ////操作前的原始sku
            //var orgSkuList = JSON.parse(JSON.stringify(this.opData.skuList));//深拷贝

            //var timelySkuList = this.opData.skuList;

            //var addList = JSLINQ(newSkuList).Where(function (newSkuRow) {
            //    var ifExist = JSLINQ(orgSkuList).Any(function (item) {
            //        return item.CombineId == newSkuRow.CombineId;
            //    });
            //    return !ifExist;
            //}).items;


            //var delList = JSLINQ(orgSkuList).Where(function (orgSkuRow) {
            //    var ifExist = JSLINQ(newSkuList).Any(function (item) {
            //        return item.CombineId == orgSkuRow.CombineId;
            //    });
            //    return !ifExist;
            //}).items;


            //if (timelySkuList.length > 0) {
            //    JSLINQ(timelySkuList).ForEach(function (item) {
            //        item.Op = "";
            //    });
            //}

            //JSLINQ(addList).ForEach(function (item) {
            //    item.Op = "Add";
            //    timelySkuList.push(item);
            //});

            //JSLINQ(delList).ForEach(function (delSku) {
            //    JSLINQ(timelySkuList).Where(function (timeSku) {
            //        if (timeSku.CombineId == delSku.CombineId) {
            //            timeSku.Op = "Del";
            //        }
            //    });

            //});


        },
        addSkuHtml: function () {

            var skuAreaHtml = "";

            var headSpecHtml = "";
            JSLINQ(this.opData.speclist).ForEach(function (spec) {
                headSpecHtml += '<td> ' + spec.SpecName + '</td>';
            });

            var headHtml = '<tr>\
            '+ headSpecHtml + '\
                            <td>\
                                价格\
                            </td>\
                            <td>\
                                数量\
                            </td>\
                            <td>\
                                商家编码\
                            </td>\
                            <td>\
                                条形码\
                            </td>\
                            <td>\
                                操作\
                            </td>\
                        </tr>';
            skuAreaHtml += headHtml;


            JSLINQ(this.opData.skuList).ForEach(function (skuRow) {
                if (skuRow.Op == "Del") {
                    return;
                }

                var rowSpecHtml = "";
                JSLINQ(skuRow.GoodsSpecValueList).ForEach(function (specValue) {
                    rowSpecHtml += "<td>" + specValue.SpecValueOtherName + "</td>";
                });

                var rowHtml = ' <tr style="display:">\
                           '+ rowSpecHtml + '\
                            <td>\
                                <input type="text"  name="GoodsPrice_' + skuRow.CombineId + '"   value="' + skuRow.GoodsPrice + '"/> \
                            </td>\
                            <td>\
                                <input type="text" name="GoodsStock_' + skuRow.CombineId + '"  value="' + skuRow.GoodsStock + '"/>\
                            </td>\
                            <td>\
                                <input type="text" name="GoodsCode_' + skuRow.CombineId + '"  value="' + skuRow.GoodsCode + '"/>\
                            </td>\
                            <td>\
                                <input type="text" name="SkuCode_' + skuRow.CombineId + '"  value="' + skuRow.SkuCode + '"/>\
                            </td>\
                            <td>\
                                <span   name="BtnIsUse_' + skuRow.CombineId + '" >不启用</span> <input type="text" name="IsUse_' + skuRow.CombineId + '" value="' + skuRow.IsUse + '"/>\
                            </td>\
                        </tr>';
                skuAreaHtml += rowHtml;

            });

            $("#skuArea").html("");

            $("#skuArea").html(skuAreaHtml);

        }

    }
})();


$(function () {
    pro.Product.ProductSpecHd.init();

});




