var pro = pro || {};
(function () {
    pro.ProductcategoryControl = pro.ProductcategoryControl || {};
    pro.ProductcategoryControl = {
        init: function (paramter) {
            var defaultParamter = {
                controlId: "Productcategory",
                editable: false,
                width:200,
                valueField: 'PkId',
                textField: 'ProductCategoryName',
                url: '/ProductManager/ProductCategory/GetList_Combotree?SystemCategoryId=' + pro.commonKit.getUrlParam("SystemCategoryId")
            };

            var options = $.extend({}, defaultParamter, paramter);
            $('#' + options.controlId).combotree(options);

        }
    }
}
)();

