var Animations;
(function (Animations) {
    var animationsPath = "http://localhost:53450/Animations/Hero/";
    var idleImages = [
        'idle/Tuscan_Idle_00000.png',
        'idle/Tuscan_Idle_10000.png',
        'idle/Tuscan_Idle_20000.png',
        'idle/Tuscan_Idle_30000.png',
        'idle/Tuscan_Idle_40000.png',
        'idle/Tuscan_Idle_50000.png',
        'idle/Tuscan_Idle_60000.png',
        'idle/Tuscan_Idle_70000.png'];
    function idle($img) {
        for (var i = 0; i < idleImages.length; i++) {
            setInterval(updateImage($img, animationsPath + idleImages[i]), 500);
        }
    }
    Animations.idle = idle;

    function updateImage($img, path) {
        $img.attr('src', path);
    }
})(Animations || (Animations = {}));
//# sourceMappingURL=PlayerAnimations.js.map
