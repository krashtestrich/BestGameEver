var Game;
(function (Game) {
    var chooseOpponentDialogSelector = '#chooseOpponent';
    var chooseOpponentDlgOptions = {
        modal: true,
        title: "Choose your opponent"
    };

    function showChooseOpponentDialog() {
        $(chooseOpponentDialogSelector).dialog('open');
    }

    function checkChooseOpponent() {
        $.ajax({
            type: "POST",
            url: "/Game/CheckChooseOpponent",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: checkChooseOpponentSuccess,
            error: checkChooseOpponentError
        });
    }
    Game.checkChooseOpponent = checkChooseOpponent;

    function checkChooseOpponentSuccess(result) {
        if (result != null) {
            $(chooseOpponentDialogSelector).dialog(chooseOpponentDlgOptions);
            $(chooseOpponentDialogSelector).html(result);
            $('#opponentList a').off('click').on('click', chooseOpponent);
            showChooseOpponentDialog();
        }
        $.unblockUI();
    }

    function checkChooseOpponentError() {
        $.unblockUI();
    }

    function chooseOpponent() {
        $.blockUI();
        var $this = $(this);
        var model = { name: ($this.attr('data-opponent')) };
        var json = { model: model };
        $.ajax({
            type: "POST",
            url: "/Game/ChooseOpponent",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            dataType: "html",
            success: chooseOpponentSuccess,
            error: chooseOpponentError
        });
    }

    function chooseOpponentSuccess() {
        $(chooseOpponentDialogSelector).dialog('close');
        $.unblockUI();
    }

    function chooseOpponentError() {
        $.unblockUI();
    }
})(Game || (Game = {}));
//# sourceMappingURL=Game.js.map
