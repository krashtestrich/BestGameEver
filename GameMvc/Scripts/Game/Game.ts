  
module Game {

    var chooseOpponentDialogSelector: string = '#chooseOpponent';
    export function showChooseOpponentDialog() {
        $(chooseOpponentDialogSelector).dialog('open');
    }
}