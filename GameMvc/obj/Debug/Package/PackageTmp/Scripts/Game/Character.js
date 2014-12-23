var Character;
(function (Character) {
    var skillTreeDialogSelector = '#skillTreeDialog';
    var characterUrl = 'http://localhost/Home/Character';
    function initialize() {
        $('a.test').cluetip();
        $('div.charEquipCellContent').cluetip({ local: true, attribute: 'data-tip', hoverClass: 'highlight' });

        $('#skillTreeDialog').dialog({
            width: 1060,
            height: 850,
            title: 'Skill Tree',
            resizable: false,
            modal: true,
            autoOpen: false,
            close: onSkillTreeClose
        });

        $('#characterSkillTreeLink').off('click').on('click', function () {
            SkillTree.openSkillTreeDialog(skillTreeDialogSelector);
        });
    }
    Character.initialize = initialize;

    function onSkillTreeClose() {
        window.location = characterUrl;
    }
})(Character || (Character = {}));
