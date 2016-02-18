$(document).ready(function () {
    $('#dataTablePlagin').DataTable({
        scrollX: true,
        scrollY: false,
        scrollCollapse: true,
        columnDefs: [
            { orderable: false, targets: 'no-orderable' },
            { className: "dt-head-center", targets: 'no-center-body' },
            { className: "dt-center", targets: '_all' }
        ],
        lengthMenu: [
        [5, 10, 25, 50, -1],
        ['5', '10', '25', '50', 'All']
        ]
    });
});