JSLINQ.fn.Take = function (count) {
    var newArray = new Array();
    for (var i = 0; i < count && i < this.items.length; i++) {
        newArray[newArray.length] = this.items[i];
    }
    return new JSLINQ(newArray);
};

JSLINQ.fn.TakeWhile = function (clause) {
    for (var count = 0; count < this.items.length; count++) {
        var item = this.items[count];

        if (clause.call(item, item) === false) {

            return this.Take(count);
        }
    }
    return this;
};

JSLINQ.fn.Skip = function (count) {
    var newArray = new Array();
    for (var i = count; i < this.items.length; i++) {
        newArray[newArray.length] = this.items[i];
    }
    return new JSLINQ(newArray);
};

JSLINQ.fn.ForEach = function (action, callback) {
    var i = 0;
    for (var value = this.items[0]; i < this.items.length && callback.call(value, value, i) !== false; value = this.items[++i]) {
        action(this.items[i]);
    }
    return this;
};

JSLINQ.fn.ForEach = function (action) {
    for (var i = 0; i < this.items.length; i++) {
        action(this.items[i], i);
    }
    return this;
};

JSLINQ.fn.Add = function (item) {
    //console.log(this.items);
    this.items.push(item);
    return this;
}; 




