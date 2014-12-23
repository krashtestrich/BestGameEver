var SkillTree;
(function (SkillTree) {
    var dialogSelector = '#skillTreeDialog';
    var $dialog;
    var availablePathSelector = 'div.skillTreePathContainerAvailable';
    var availablePathHoverClass = 'skillTreePathContainerHover';
    var selectWarningSelector = '#skillTreeSelectLabel';
    var selectSkillLabelSelector = '#skillTreeSelectSkillText';

    function openSkillTreeDialog(selector) {
        $dialog = $(selector);
        setupUi();
        initialiseBindings();
        $dialog.dialog('open');
    }
    SkillTree.openSkillTreeDialog = openSkillTreeDialog;

    function setupUi() {
        $(selectWarningSelector).hide();
    }

    function initialiseBindings() {
        $dialog.find(availablePathSelector).mouseenter(function () {
            var $this = $(this);
            availablePathHoverIn($this);
        });

        $dialog.find(availablePathSelector).mouseleave(function () {
            var $this = $(this);
            availablePathHoverOut($this);
        });

        $dialog.find(availablePathSelector).click(function () {
            var $this = $(this);
            chooseSkillPath($this);
        });
    }

    function chooseSkillPath($container) {
        var skillName = getSkillPathNameFromContainer($container);
        $.blockUI();
        var json = { skillName: skillName };
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

    function chooseSkillSuccess(result) {
        $(dialogSelector).html(result);
        $.unblockUI();
    }

    function chooseSkillError() {
        $.unblockUI();
    }

    function getSkillPathNameFromContainer($container) {
        return $container.find('span.skillTreeBaseHeading:first,span.skillTreePathHeading:first').html();
    }

    function availablePathHoverIn($this) {
        var skillText = getSkillPathNameFromContainer($this);
        $(selectSkillLabelSelector).html(skillText);
        $(selectWarningSelector).show();
        $this.addClass(availablePathHoverClass);
    }

    function availablePathHoverOut($this) {
        $(selectSkillLabelSelector).html('');
        $(selectWarningSelector).hide();
        $this.removeClass(availablePathHoverClass);
    }
})(SkillTree || (SkillTree = {}));
