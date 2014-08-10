var Character;
(function (Character) {
    function initialize() {
        $('a.test').cluetip();
        $('div.charEquipCellContent').cluetip({ local: true, attribute: 'data-tip', hoverClass: 'highlight' });
    }
    Character.initialize = initialize;
})(Character || (Character = {}));
