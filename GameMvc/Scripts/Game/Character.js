var Character;
(function (Character) {
    var skillTreeDialogContentSelector = '#skillTreeDialogContent';
    function initialize() {
        $('a.test').cluetip();
        $('div.charEquipCellContent').cluetip({ local: true, attribute: 'data-tip', hoverClass: 'highlight' });
        var $dlg = $(skillTreeDialogContentSelector);
        $dlg.dialog({
            width: 1060,
            height: 850,
            title: 'Skill Tree',
            resizable: false,
            modal: true,
            autoOpen: false,
            close: onSkillTreeClose
        });

        $('#characterSkillTreeLink').off('click').on('click', function () {
            SkillTree.openSkillTreeDialog(skillTreeDialogContentSelector);
        });

        var skillPointExists = parseInt($(skillTreeDialogContentSelector).attr('data-skillpoints')) > 0;
        if (skillPointExists) {
            $dlg.dialog("widget").find(".ui-dialog-titlebar-close").hide();
            $dlg.dialog("option", "closeOnEscape", false);
            SkillTree.openSkillTreeDialog(skillTreeDialogContentSelector);
        }
    }
    Character.initialize = initialize;

    function onSkillTreeClose() {
        window.location.href = '/Home/Character';
    }
})(Character || (Character = {}));
