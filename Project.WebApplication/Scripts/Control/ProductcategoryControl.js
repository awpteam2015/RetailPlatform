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
                textField: 'ProductcategoryName',
                url: '/ProductManager/ProductCategory/GetList_Combotree'
            };

            var options = $.extend({}, defaultParamter, paramter);
            $('#' + options.controlId).combotree(options);

        }
    }
}
)();

