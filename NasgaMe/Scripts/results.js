$(document).ready(function () {
    var $table = $('.table');
    var $fixedColumn = $table.clone().insertBefore($table).addClass('fixed-column');

    $fixedColumn.find('th:not(:first-child),td:not(:first-child)').remove(); //nth-child(2) if needs adapted

    $fixedColumn.find('tr').each(function (i, elem) {
        $(this).height($table.find('tr:eq(' + i + ')').height());
    });
});