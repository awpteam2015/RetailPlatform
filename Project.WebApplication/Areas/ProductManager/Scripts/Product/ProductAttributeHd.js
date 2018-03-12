var pro = pro || {};
(function () {
    pro.Product = pro.Product || {};
    pro.Product.ProductAttributeHd = pro.Product.ProductAttributeHd || {};
    pro.Product.ProductAttributeHd = {

        opData: {
            attributeList: [],
            productAttributeValueList: []
        },
        init: function () {
            this.opData.attributeList = JSON.parse($("#AttributeVmList").val());

            this.initAttributeArea();
        },
        initEvent: function () { },
        initAttributeArea: function () {

            var html = "";
            JSLINQ(this.opData.attributeList).ForEach(function (attribute) {

                var attributeValueHtml = "";
                if (attribute.ShowType == 1) {//复选框

                    JSLINQ(attribute.AttributeValueList).ForEach(function (attributeValue) {
                        var checkHmtl = "";
                        if (attributeValue.IsCheck == "1") {
                            checkHmtl = 'checked="true"';
                        }
                        attributeValueHtml += ' <div style="width: 200px;float: left">\
                            <input id="attr_' + attributeValue.AttributeId + '_' + attributeValue.AttributeValueId + '" name="attr_' + attributeValue.AttributeId + '"  ' + checkHmtl + ' type="checkbox" value="' + attributeValue.AttributeValueId + '"    onchange="pro.Product.ProductAttributeHd.chooseAttribute(this)"  />' + attributeValue.AttributeValueName + "\
                        </div>";
                    });


                } else if (attribute.ShowType == 2) {//下拉框

                    attributeValueHtml += '<option value=""></option>';
                    JSLINQ(attribute.AttributeValueList).ForEach(function (attributeValue) {
                        var checkHmtl = "";
                        if (attributeValue.IsCheck == "1") {
                            checkHmtl = 'selected="selected"';
                        }

                        attributeValueHtml += '<option value="' + attributeValue.AttributeValueId + '"  ' + checkHmtl + '>' + attributeValue.AttributeValueName + '</option>';

                    });
                    attributeValueHtml = '<select onchange="pro.Product.ProductAttributeHd.chooseAttribute(this)"  id="attr_' + attribute.AttributeId + '" name="attr_' + attribute.AttributeId + '">' + attributeValueHtml + '</select>';
                }

                html += '<tr>\
                        <td style="width:150px">\
                           ' + attribute.AttributeName + '：\
                        </td>\
                        <td>\
                             ' + attributeValueHtml + '\
                        </td>\
                        </tr>';
            });

            $("#attributeArea").html(html);
        },
        chooseAttribute: function () {

            this.opData.productAttributeValueList = [];

            JSLINQ(this.opData.attributeList).ForEach(function (attribute) {

                if (attribute.ShowType == 1) {//复选框

                    JSLINQ(attribute.AttributeValueList).ForEach(function (attributeValue) {

                        if ($('#attr_' + attributeValue.AttributeId + '_' + attributeValue.AttributeValueId + '').is(':checked')) {
                            attributeValue.IsCheck = 1;

                            pro.Product.ProductAttributeHd.opData.productAttributeValueList.push(
                                { AttributeValueId: attributeValue.AttributeValueId, AttributeValueName: attributeValue.AttributeValueName, AttributeId: attributeValue.AttributeId });
                        }

                    });

                } else if (attribute.ShowType == 2) {//下拉框

                    var selectValue = $('#attr_' + attribute.AttributeId + '').val();
                    JSLINQ(attribute.AttributeValueList).ForEach(function (attributeValue) {

                        if (selectValue == attributeValue.AttributeValueId) {
                            attributeValue.IsCheck = 1;
                            pro.Product.ProductAttributeHd.opData.productAttributeValueList.push(
                                { AttributeValueId: attributeValue.AttributeValueId, AttributeValueName: attributeValue.AttributeValueName, AttributeId: attributeValue.AttributeId });
                        }
                    });

                }

            });

            pro.debugKit.consoleLog(this.opData.productAttributeValueList);

        }

    }
})();


$(function () {
    pro.Product.ProductAttributeHd.init();
});