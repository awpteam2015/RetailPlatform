var pro = pro || {};
(function () {
    pro.debugKit = pro.debugKit || {};
    pro.debugKit = {
        alert: function (msg) {
            alert(msg);
        },
        consoleLog: function (msg) {
            console.log(JSON.stringify(msg));
        }
    };
})();