$(document).ready(function () {
    $('a.test').cluetip();
    $('div.charEquipCellContent').cluetip({ local: true, attribute: 'data-tip', hoverClass: 'highlight' });
});