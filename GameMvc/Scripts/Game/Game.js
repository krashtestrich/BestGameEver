var Game;
(function (Game) {
    var chooseOpponentDialogSelector = '#chooseOpponent';
    var battleOverDialogSelector = '#battleOver';
    var chooseOpponentDlgOptions = {
        modal: true,
        title: "Choose your opponent",
        close: function () {
            location.href = '/Home/Character';
        }
    };
    var battleOverDialogOptions = {
        modal: true,
        title: "Battle Over!",
        close: function () {
            processBattleOver();
        }
    };

    function showChooseOpponentDialog() {
        $(chooseOpponentDialogSelector).dialog('open');
    }

    function showBattleOverDialog() {
        $(battleOverDialogSelector).dialog('open');
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
        if (result != null && result != '') {
            $(chooseOpponentDialogSelector).dialog(chooseOpponentDlgOptions);
            $(chooseOpponentDialogSelector).html(result);
            $(chooseOpponentDialogSelector + ' a').on('click', function () {
                $.blockUI();
            });
            showChooseOpponentDialog();
        }
        $.unblockUI();
    }

    function checkChooseOpponentError() {
        $.unblockUI();
    }

    function checkBattleStatus() {
        $.ajax({
            type: "POST",
            url: "/Game/CheckBattleStatus",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: checkBattleStatusSuccess,
            error: checkBattleStatusError
        });
    }
    Game.checkBattleStatus = checkBattleStatus;

    function checkBattleStatusSuccess(result) {
        if (result != null && result != '') {
            $(battleOverDialogSelector).dialog(battleOverDialogOptions);
            $(battleOverDialogSelector).html(result);
            $('#btnBattleOver').off('click').on('click', function () {
                $(battleOverDialogSelector).dialog('close');
            });
            showBattleOverDialog();
        }
        $.unblockUI();
    }

    function checkBattleStatusError() {
        $.unblockUI();
    }

    function processBattleOver() {
        $.blockUI();
        $.ajax({
            type: "POST",
            url: "/Game/ProcessBattleOver",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: processBattleOverSuccess,
            error: processBattleOverError
        });
    }

    function processBattleOverSuccess() {
        location.href = '/Home/Character';
        $.unblockUI();
    }

    function processBattleOverError() {
        $.unblockUI();
    }
})(Game || (Game = {}));
