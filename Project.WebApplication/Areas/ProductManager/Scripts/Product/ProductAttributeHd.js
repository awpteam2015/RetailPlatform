var pro = pro || {};
(function () {
    pro.Product = pro.Product || {};
    pro.Product.ProductAttributeHd = pro.Product.ProductAttributeHd || {};
    pro.Product.ProductAttributeHd = {

        opData: {
            attributeList: []

        },
        init: function () {
            this.opData.attributeList = JSON.parse($("#AttributeVmList").val());

            this.initAttributeArea();
        },
        initEvent: function () { },
        initAttributeArea: function () {

            var html = "";
            JSLINQ(this.opData.attributeList).ForEach(function (attribute) {

                var checkHmtl = "";
                if (attribute.IsCheck == "1") {
                    checkHmtl = 'checked="true"';
                }


                var attributeValueHtml = "";
                if (attribute.ShowType == 1) {//复选框

                    JSLINQ(attribute.AttributeValueList).ForEach(function (attributeValue) {
                        attributeValueHtml += ' <div style="width: 200px;float: left">\
                            <input id="attr_' + attributeValue.AttributeId + '_' + attributeValue.AttributeValueId + '" name="attr_' + attributeValue.AttributeId + '"  ' + checkHmtl + ' type="checkbox" value="' + attributeValue.AttributeValueId + '"     />' + attributeValue.AttributeValueName + "\
                        </div>";
                    });


                } else if (attribute.ShowType == 2) {//下拉框

                    JSLINQ(attribute.AttributeValueList).ForEach(function (attributeValue) {
                        attributeValueHtml += '<option value="' + attributeValue.AttributeValueId + '">' + attributeValue.AttributeValueName + '</option>';
                    });
                    attributeValueHtml = '<select id="attr_' + attribute.AttributeId + '" name="attr_' + attribute.AttributeId + '">' + attributeValueHtml + '</select>';
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
        }
    }
})();


$(function () {
    pro.Product.ProductAttributeHd.init();
});