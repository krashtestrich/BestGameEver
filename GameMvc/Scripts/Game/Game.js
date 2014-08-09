var Game;
(function (Game) {
    var chooseOpponentDialogSelector = '#chooseOpponent';
    function showChooseOpponentDialog() {
        $(chooseOpponentDialogSelector).dialog('open');
    }
    Game.showChooseOpponentDialog = showChooseOpponentDialog;
})(Game || (Game = {}));
//# sourceMappingURL=Game.js.map
