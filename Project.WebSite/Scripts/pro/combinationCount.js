//组合计算
var pro = pro || {};
(function () {
    pro.combinationCount = pro.combinationCount || {};
    pro.combinationCount = {
        combinationResult: [],
        needcombinationData: [],
        middleResult: [],
        doExchange: function (arr, depth) {
            for (var i = 0; i < arr[depth].length; i++) {
                pro.combinationCount.middleResult[depth] = arr[depth][i];
                if (depth != arr.length - 1) {
                    pro.combinationCount.doExchange(arr, depth + 1);
                } else {
                    var result = JSON.parse(JSON.stringify(pro.combinationCount.middleResult));
                    pro.combinationCount.combinationResult.push(result);
                }
            }
        },
        Count: function (arr) {
            pro.combinationCount.combinationResult = [];
            //var arr = [
            //       ['a', 'b', 'c'],
            //       ['1'],
            //       ['x']
            //];

            //  alert(JSON.stringify(arr));
            pro.combinationCount.doExchange(arr, 0);
       
            //pro.debugKit.consoleLog(JSON.stringify(pro.combinationCount.combinationResult));

            //pro.debugKit.consoleLog(pro.combinationCount.combinationResult.length);
            //  alert(JSON.stringify(pro.combinationCount.combinationResult));
        }
    }
})();