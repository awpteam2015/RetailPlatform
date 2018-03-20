var pro = pro || {};
(function () {
    pro.PageContentCategoryControl = pro.PageContentCategoryControl || {};
    pro.PageContentCategoryControl = {
        init: function (paramter) {
            var defaultParamter = {
                controlId: "PageContentCategoryId",
                editable: false,
                width:200,
                valueField: 'PkId',
                textField: 'PageContentCategoryName',
                url: '/ContentManager/PageContentCategory/GetList_Combotree'
            };

            var options = $.extend({}, defaultParamter, paramter);
            $('#' + options.controlId).combotree(options);

        }
    }
}
)();

