function TestDataTablesAdd() {
    $(document).ready(function () {
        $(table).DataTable();
    });
}

function DataTablesRemove(table) {
    // $(document).ready(function () {
    //     $(table).DataTable();
    //     var elem = document.querySelector(table + '_wrapper');
    //     elem.parentNode.removeChild(elem);
    // });
    $(document).ready(function () {
        $(table).DataTable().destroy();
    });

}

function DataTablesAdd(table) {
    if ($.fn.dataTable.isDataTable(table)) {
        table = $(table).DataTable();
    }
    else {
        table = $(table).DataTable({
            "lengthMenu": [10, 25, 50],
            "pageLength": 10
        });
    }
    /*$(document).ready(function () {
        $(table).DataTable(
        {
            "lengthMenu": [10, 25, 50],
            "pageLength": 10
        }
        );
    });*/
}