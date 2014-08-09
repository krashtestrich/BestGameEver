 
module Arena {

    var $playerTargetTile: JQuery;
    var targettedClassName: string = "targetted";

    interface ITileLocation {
        xCoord: number
        yCoord: number
    }

    export function initialize(): void {
        $('div.arenaFloorTile').off('click').on('click', function () {
            Arena.targetTile($(this));
        });
        $('#chooseOpponent').dialog();
    }

    export function targetTile($tile: JQuery) {
        $.blockUI(); 
        targetTileFormatUi($tile);
        var model : ITileLocation = { xCoord: parseInt($tile.attr('data-xcoord')), yCoord: parseInt($tile.attr('data-ycoord')) }
        var json = { model: model};
        $.ajax({
            type: "POST",
            url: "/Arena/TargetTile",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            dataType: "html",
            success: targetTileSuccess,
            error: targetTileError
        });
    }

    function targetTileFormatUi($tile) {
        if (!$tile.hasClass(targettedClassName)) {
            if ($playerTargetTile != null) {
                $playerTargetTile.removeClass(targettedClassName);
            }
            $tile.addClass(targettedClassName);
            $playerTargetTile = $tile;
        }
    }

    function targetTileSuccess(result: any) {
        $('#playerActions').html(result);
        $.unblockUI();
    }

    function targetTileError() {
        $.unblockUI();
    }  
}