$(document).ready(function () {
    var start; var finish;
    $('.selectable').sortable({
        axis: "y",
        cursor: "move",
        containment: "parent",
        tolerance: "pointer",
        opacity: 0.8,
        placeholder: "sortable-placeholder",

        start: function (e, ui) {
            start = ui.item.index() + 1;
        },

        update: function (e, ui) {
            finish = ui.item.index() + 1;
        },

        stop: function (e, ui) {
            $("#beginVal").val(start);
            $("#finalVal").val(finish);
            $("#loginform").submit();
        }
    });
    $('.selectable').disableSelection();
});