$(document).ready(function () {
    var table = $('#dataTablePlagin').DataTable({
        scrollX: true,
        scrollY: false,
        scrollCollapse: true,
        columns: [
       { className: "dt-center" },
       { className: "dt-center" },
       { className: "dt-center" },
       { className: "dt-center" },
       { className: "dt-center" },
       { className: "dt-center" },
       { className: "dt-head-center" },
       { className: "dt-center" },
       { className: "dt-center", orderable: false }
        ],
        lengthMenu: [
        [5, 10, 25, 50, -1],
        ['5', '10', '25', '50', 'All']
        ]
    });
});