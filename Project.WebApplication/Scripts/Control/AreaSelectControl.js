var pro = pro || {};
(function () {
    pro.AreaSelectControl = pro.AreaSelectControl || {};
    pro.AreaSelectControl = {
        init: function (paramter) {

            var defaultParamterProvince = {
                editable: false, //不可编辑状态    
                cache: false,
                panelHeight: '150', //自动高度适合    
                valueField: 'ProvinceId',
                textField: 'Province',
                url: "/SystemSetManager/Area/GetList_Combobox_Province",
                onSelect: function(record) {
                    $("#CityId").combobox("setValue", ''); //清空市  
                    $("#AreaId").combobox("setValue", ''); //清空县  
                    var shengid = $('#ProvinceId').combobox('getValue');

                    $.ajax({
                        async: false,
                        url: "/SystemSetManager/Area/GetList_Combobox_City",
                        data: { ProvinceId: shengid },
                        type: "POST",
                        dataType: "json",
                        success: function(data) {
                            //alert(data);  
                            $("#CityId").combobox("loadData", data);
                        }

                    });
                },
                onLoadSuccess: function() {
                    var shengid = $('#ProvinceId').combobox('getValue');
                    if (shengid != "") {
                        $.ajax({
                            async: false,
                            url: "/SystemSetManager/Area/GetList_Combobox_City",
                            data: { ProvinceId: shengid },
                            type: "POST",
                            dataType: "json",
                            success: function(data) {
                                //alert(data);  
                                $("#CityId").combobox("loadData", data);
                            }

                        });
                    }
                }
            };

            var optionsProvince = $.extend({}, defaultParamterProvince, paramter);

            $('#ProvinceId').combobox(optionsProvince);


            var defaultParamterCity = {
                editable: false, //不可编辑状态    
                cache: false,
                panelHeight: '150', //自动高度适合    
                valueField: 'CityId',
                textField: 'City',
                onSelect: function(record) {
                    $("#AreaId").combobox("setValue", ''); //清空县  
                    var shiid = $('#CityId').combobox('getValue');
                    $.ajax({
                        async: false,
                        url: "/SystemSetManager/Area/GetList_Combobox_Area",
                        cache: false,
                        data: { CityId: shiid },
                        type: "POST",
                        dataType: "json",
                        success: function(data) {
                            $("#AreaId").combobox("loadData", data);
                        }
                    });
                },
                onLoadSuccess: function() {
                    var shiid = $('#CityId').combobox('getValue');
                    if (shiid != "") {
                        $.ajax({
                            async: false,
                            url: "/SystemSetManager/Area/GetList_Combobox_Area",
                            cache: false,
                            data: { CityId: shiid },
                            type: "POST",
                            dataType: "json",
                            success: function(data) {
                                $("#AreaId").combobox("loadData", data);
                            }

                        });
                    }

                }
            };
            var optionsCity = $.extend({}, defaultParamterCity, paramter);

            $('#CityId').combobox(optionsCity);


            var defaultParamterArea = {
                editable: false, //不可编辑状态    
                cache: false,
                panelHeight: '150', //自动高度适合    
                valueField: 'AreaId',
                textField: 'Area'
            };
            var optionsArea = $.extend({}, defaultParamterArea, paramter);
            $('#AreaId').combobox(optionsArea);

        }
    }
}
)();

