$(document).ready(function () {
    var filterHeight = $('#expanded-filter').height();
    var table = $('#GridView1');
    margin_below_table(filterHeight, table);

    $('#filter-collapse').click(function() {
        $('#expanded-filter').addClass('no-show');
        $('#collapsed-filter').removeClass('no-show');
        filterHeight = $('#collapsed-filter').height();
        margin_below_table(filterHeight, table);
    });
    $('#filter-expand').click(function () {
        $('#collapsed-filter').addClass('no-show');
        $('#expanded-filter').removeClass('no-show');
        filterHeight = $('#expanded-filter').height();
        margin_below_table(filterHeight, table);
    });

    function margin_below_table(filterHeight, table) {
        table.css('margin-bottom', filterHeight + "px");
    }
});