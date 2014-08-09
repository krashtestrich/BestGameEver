module Character {
    export function initialize() : void {
        $('a.test').cluetip();
        $('div.charEquipCellContent').cluetip({ local: true, attribute: 'data-tip', hoverClass: 'highlight' });
    }
}