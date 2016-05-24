module SkillTree {
    var dialogSelector: string = '#skillTreeDialogContent';
    var $dialog: JQuery;
    var availablePathSelector: string = 'div.skillTreePathContainerAvailable';
    var availablePathHoverClass: string = 'skillTreePathContainerHover';
    var selectWarningSelector: string = '#skillTreeSelectLabel';
    var selectSkillLabelSelector: string = '#skillTreeSelectSkillText';

    export function openSkillTreeDialog(selector: string) : void{
        $dialog = $(selector);
        setupUi();
        initialiseBindings();
        $dialog.dialog('open');
    }

    function setupUi() : void {
        $(selectWarningSelector).hide();
    }

    function initialiseBindings() : void {
        $dialog.find(availablePathSelector).mouseenter(function () {
            var $this = $(this);
            availablePathHoverIn($this);
        });

        $dialog.find(availablePathSelector).mouseleave(function () {
            var $this = $(this);
            availablePathHoverOut($this);
        });

        $dialog.find(availablePathSelector).click(function() {
            var $this = $(this);
            chooseSkillPath($this);
        });
    }

    function chooseSkillPath($container : JQuery) {
        var skillName : string = getSkillPathNameFromContainer($container);
        $.blockUI();
        var json = { skillName: skillName};
        $.ajax({
            type: "POST",
            url: "/Character/SelectSkill",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            dataType: "html",
            success: chooseSkillSuccess,
            error: chooseSkillError
        });
    }

    function chooseSkillSuccess(result: any): void {
        var $dlg = $(dialogSelector);
        $dlg.html(result);
        $dlg.dialog("widget")
            .find(".ui-dialog-titlebar-close")
            .show();
        $.unblockUI();
    }

    function chooseSkillError() : void {
        $.unblockUI();
    }


    function getSkillPathNameFromContainer($container: JQuery): string {
       return $container.find('span.skillTreeBaseHeading:first,span.skillTreePathHeading:first').html();
    }

    function availablePathHoverIn($this : JQuery) : void {
        var skillText = getSkillPathNameFromContainer($this);
        $(selectSkillLabelSelector).html(skillText);
        $(selectWarningSelector).show();
        $this.addClass(availablePathHoverClass);
    }

    function availablePathHoverOut($this: JQuery) : void {
        $(selectSkillLabelSelector).html('');
        $(selectWarningSelector).hide();
        $this.removeClass(availablePathHoverClass);
    }
}