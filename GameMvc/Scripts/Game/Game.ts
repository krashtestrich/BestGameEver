  
module Game {

    interface IChooseOpponent {
        name: string
    }

    var chooseOpponentDialogSelector: string = '#chooseOpponent';
    var chooseOpponentDlgOptions: JQueryUI.DialogOptions = {
        modal: true,
        title: "Choose your opponent"
    };

    function showChooseOpponentDialog() {
        $(chooseOpponentDialogSelector).dialog('open');
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

    function chooseOpponent(): void {
        $.blockUI();
        var $this = $(this);
        var model: IChooseOpponent = { name: ($this.attr('data-opponent')) }
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
}