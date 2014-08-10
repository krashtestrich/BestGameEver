  
module Game {
    var chooseOpponentDialogSelector : string = '#chooseOpponent';
    var battleOverDialogSelector : string = '#battleOver';
    var chooseOpponentDlgOptions: JQueryUI.DialogOptions = {
        modal: true,
        title: "Choose your opponent",
        close: () => {
            location.href = '/Home/Character';
        }
    };
    var battleOverDialogOptions : JQueryUI.DialogOptions = {
        modal: true,
        title: "Battle Over!",
        close: () => {
            processBattleOver();
        }
    }

    function showChooseOpponentDialog() {
        $(chooseOpponentDialogSelector).dialog('open');
    }

    function showBattleOverDialog() {
        $(battleOverDialogSelector).dialog('open');
    }

    export function checkChooseOpponent() {
        $.ajax({
            type: "POST",
            url: "/Game/CheckChooseOpponent",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: checkChooseOpponentSuccess,
            error: checkChooseOpponentError
        });
    }

    function checkChooseOpponentSuccess(result: any) {
        if (result != null && result != '') {
            $(chooseOpponentDialogSelector).dialog(chooseOpponentDlgOptions);
            $(chooseOpponentDialogSelector).html(result);
            $(chooseOpponentDialogSelector + ' a').on('click', () => {
                $.blockUI();
            });
            showChooseOpponentDialog();
        }
        $.unblockUI();
    }

    function checkChooseOpponentError() {
        $.unblockUI();
    }

    export function checkBattleStatus() {
        $.ajax({
            type: "POST",
            url: "/Game/CheckBattleStatus",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: checkBattleStatusSuccess,
            error: checkBattleStatusError
        });
    }

    function checkBattleStatusSuccess(result: any) {
        if (result != null && result != '') {
            $(battleOverDialogSelector).dialog(battleOverDialogOptions);
            $(battleOverDialogSelector).html(result);
            $('#btnBattleOver').off('click').on('click', () => {
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
}