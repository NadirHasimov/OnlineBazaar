var categoriesTable;
$(document).ready(function () {
    createDataTable();
    createEventHandlers();
    showNotification();

});

function createDataTable() {
    categoriesTable = $('#categories-table').dataTable({
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "pageLength": 10,
        "destroy": true,
        "ordering": false,
        "language": {
            "search": "",
            searchPlaceholder: "Search"
        },
        "autoWidth": false,
        "columns": [
            { "visible": false },
            { "width": "35%" },
            { "width": "50%" },
            { "width": "15%" }
        ],
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
            $(nRow).children("td").css("white-space", "nowrap");
        },
        "drawCallback": function (settings) {
            enableDescPopover();
        }
    });
}

function enableDescPopover() {
    $('.long-desc').popover({
        trigger: 'hover',
        placement: 'top'
    });
}

function createEventHandlers() {

    $('#categories-table-container').on('click', 'a.delete', function (e) {
        e.preventDefault();
        if (confirm('Are you sure?')) {
            var row = $(this).closest("tr");
            $.ajax({
                type: "POST",
                url: $(this).attr('href'),
                success: function (response) {
                    if (response.result) {
                        categoriesTable.fnDeleteRow(row[0]);
                        showSuccessNotification(response.message);
                    }
                    else {
                        showErrorNotification(response.message);
                    }
                }
            });
        }
    });

    $("#reload-categories").click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "GET",
            url: $(this).attr('href'),
            dataType: 'html',
            success: function (response) {
                $('#categories-table-container .panel .panel-body').html(response);
                createDataTable();
            }
        });
    });
}

function showNotification() {
    if (message) {
        if (success) {
            showSuccessNotification(message)
        }
        else {
            showErrorNotification(message);
        }
    }

}
