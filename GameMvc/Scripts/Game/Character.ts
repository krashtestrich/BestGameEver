module Character {
    var skillTreeDialogSelector: string = '#skillTreeDialog';
    export function initialize() : void {
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

        $('#characterSkillTreeLink').off('click').on('click', () => {
            SkillTree.openSkillTreeDialog(skillTreeDialogSelector);
        });
    }

    function onSkillTreeClose() {
        window.location.href = '/Home/Character';
    }
}