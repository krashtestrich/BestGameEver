
module Animations {
    var animationsPath : string = "http://localhost:53450/Animations/Hero/";
    var idleImages: Array<string> = ['idle/Tuscan_Idle_00000.png',
        'idle/Tuscan_Idle_10000.png',
        'idle/Tuscan_Idle_20000.png',
        'idle/Tuscan_Idle_30000.png',
        'idle/Tuscan_Idle_40000.png',
        'idle/Tuscan_Idle_50000.png',
        'idle/Tuscan_Idle_60000.png',
        'idle/Tuscan_Idle_70000.png'];
    export function idle($img: JQuery): void {
        for (var i = 0; i < idleImages.length; i++) {
            setInterval(updateImage($img, animationsPath + idleImages[i]), 500);
        }
    }

    function updateImage($img : JQuery, path : string)
    {
        $img.attr('src', path);
    }
}