var TextAnimationController = function () {
    var typed;
    var array = [];
    var animate = function (textBlockId, text) {
        array = text;
        typed = new Typed(textBlockId, {
            strings: array,
            typeSpeed: 80,
            backSpeed: 30,
            backDelay: 8000,
            startDelay: 500,
            loop: true
        });
    };
    var refresh = function () {
        array = [];
    };
    return {
        refresh: refresh,
        animateText: animate
    };
}();