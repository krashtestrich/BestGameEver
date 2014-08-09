var Arena;
(function (Arena) {
    var $playerTargetTile;
    var targettedClassName = "targetted";

    function initialize() {
        $('div.arenaFloorTile').off('click').on('click', function () {
            Arena.targetTile($(this));
        });
        $('#chooseOpponent').dialog();
    }
    Arena.initialize = initialize;

    function targetTile($tile) {
        $.blockUI();
        targetTileFormatUi($tile);
        var model = { xCoord: parseInt($tile.attr('data-xcoord')), yCoord: parseInt($tile.attr('data-ycoord')) };
        var json = { model: model };
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
    Arena.targetTile = targetTile;

    function targetTileFormatUi($tile) {
        if (!$tile.hasClass(targettedClassName)) {
            if ($playerTargetTile != null) {
                $playerTargetTile.removeClass(targettedClassName);
            }
            $tile.addClass(targettedClassName);
            $playerTargetTile = $tile;
        }
    }

    function targetTileSuccess(result) {
        $('#playerActions').html(result);
        $.unblockUI();
    }

    function targetTileError() {
        $.unblockUI();
    }
})(Arena || (Arena = {}));
//# sourceMappingURL=Arena.js.map
