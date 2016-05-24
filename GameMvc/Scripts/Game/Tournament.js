var Tournament;
(function (Tournament) {
    var tournamentSelector = '#tournamentBattleDetails';
    var battleInfoDialogSelector = '#tournamentBattleDetailsDialog';
    var characterDetailsDialogSelector = '#tournamentCharacterDetailsDialog';
    var battleGuidAttributeSelector = 'data-battleguid';
    var characterNameAttributeSelector = 'data-charactername';

    function initialize() {
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
    Tournament.initialize = initialize;

    function displayBattleInfo($btn) {
        $.blockUI();
        var guid = $btn.attr(battleGuidAttributeSelector);
        var model = { battleGuid: guid };
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

    function getBattleDetailsSuccess(result) {
        var $dlg = $(battleInfoDialogSelector);
        $dlg.html(result);
        $dlg.dialog('open');
        $.unblockUI();
    }

    function getBattleDetailsError() {
        $.unblockUI();
    }

    function displayCharacterDetails($link) {
        $.blockUI();
        var name = $link.attr(characterNameAttributeSelector);
        var model = { characterName: name };
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

    function getCharacterDetailsSuccess(result) {
        var $dlg = $(characterDetailsDialogSelector);
        $dlg.html(result);
        $dlg.dialog('open');
        $.unblockUI();
    }

    function getCharacterDetailsError() {
        $.unblockUI();
    }
})(Tournament || (Tournament = {}));
