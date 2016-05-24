module Tournament {
    var tournamentSelector: string = '#tournamentBattleDetails';
    var battleInfoDialogSelector: string = '#tournamentBattleDetailsDialog';
    var characterDetailsDialogSelector: string = '#tournamentCharacterDetailsDialog';
    var battleGuidAttributeSelector: string = 'data-battleguid';
    var characterNameAttributeSelector: string = 'data-charactername';

    export function initialize() : void {
        $.blockUI();
        $(tournamentSelector).find('input[' + battleGuidAttributeSelector + ']').on('click', function () {
            displayBattleInfo($(this));
        });
        $(battleInfoDialogSelector).dialog({
            width: 1060,
            height: 850,
            title: 'Battle Result',
            resizable: false,
            modal: true,
            autoOpen: false
        });
        $(tournamentSelector).find('a[' + characterNameAttributeSelector + ']').on('click', function () {
            displayCharacterDetails($(this));
        });
        $(characterDetailsDialogSelector).dialog({
            width: 1060,
            height: 850,
            title: 'Character Info',
            resizable: false,
            modal: true,
            autoOpen: false
        });
        $.unblockUI();
    }

    function displayBattleInfo($btn) : void {
        $.blockUI();
        var guid : string = $btn.attr(battleGuidAttributeSelector);
        var model : Object = { battleGuid: guid };
        $.ajax({
            type: "POST",
            url: "/Tournament/GetBattleResult",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            dataType: "html",
            success: getBattleDetailsSuccess,
            error: getBattleDetailsError
        });
    }

    function getBattleDetailsSuccess(result: any): void {
        var $dlg = $(battleInfoDialogSelector);
        $dlg.html(result);
        $dlg.dialog('open');
        $.unblockUI();
    }

    function getBattleDetailsError(): void {
        $.unblockUI();
    }

    function displayCharacterDetails($link): void {
        $.blockUI();
        var name: string = $link.attr(characterNameAttributeSelector);
        var model: Object = { characterName: name };
        $.ajax({
            type: "POST",
            url: "/Tournament/GetCharacterDetails",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            dataType: "html",
            success: getCharacterDetailsSuccess,
            error: getCharacterDetailsError
        });
    }

    function getCharacterDetailsSuccess(result: any): void {
        var $dlg = $(characterDetailsDialogSelector);
        $dlg.html(result);
        $dlg.dialog('open');
        $.unblockUI();
    }

    function getCharacterDetailsError(): void {
        $.unblockUI();
    }
}