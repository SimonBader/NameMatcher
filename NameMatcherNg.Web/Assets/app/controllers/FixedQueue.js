function FixedQueue(size, initialValues) {
    initialValues = (initialValues || []);
    var queue = Array.apply(null, initialValues);
    queue.fixedSize = size;
    queue.enqueue = FixedQueue.enqueue;
    queue.remove = FixedQueue.remove;
    FixedQueue.trimTail.call(queue);
    return (queue);
}

FixedQueue.trimHead = function () {
    if (this.length <= this.fixedSize) {
        return;
    }

    Array.prototype.splice.call(
        this,
        0,
        (this.length - this.fixedSize)
    );
};

FixedQueue.trimTail = function () {
    if (this.length <= this.fixedSize) {
        return;
    }

    Array.prototype.splice.call(
       this,
       this.fixedSize,
       (this.length - this.fixedSize)
    );
};

FixedQueue.enqueue = function (value) {
    this.unshift(value);
    FixedQueue.trimTail.call(this);
};

FixedQueue.remove = function(value) {
    var index = this.indexOf(value);

    if (index > -1) {
        Array.prototype.splice.call(
           this,
           index,
           1
        );
    }
}