var TextAnimationController = function () {
    var animate = function (textBlockId, text) {
        var typed = new Typed(".js-sidebar-text-animation", {
            strings: text,
            typeSpeed: 80,
            backSpeed: 30,
            backDelay: 8000,
            startDelay: 2000,
            loop: true
        });
    }
    return {
        animateText: animate
    }
}();