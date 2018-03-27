var pro = pro || {};
(function () {
    pro.listKit = pro.listKit || {};
    pro.listKit = {

        onSelCheck: function (value) {
            $("input[name=PkId]").each(
                function () {
                    if ($(this).val() == value) {
                        $(this).attr("checked", "checked");
                    } else {
                        $(this).attr("checked", false);
                    }
                }
            );
        },
        getSelData: function () {
            var pkId = $("input[name=PkId]:checked").val();
            var divIndex = $("input[name=PkId]:checked").attr("forgrid");

            if (divIndex >= 0) {
                return $('#ddv-' + divIndex).datagrid("getSelected");
            }

            return "";
        }

    };
})();