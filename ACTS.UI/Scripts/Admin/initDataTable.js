$(document).ready(function () {
    $('#dataTablePlagin').DataTable({
        order: [],
        scrollX: true,
        scrollY: false,
        scrollCollapse: true,
        columnDefs: [
            { orderable: false, targets: 'no-orderable' },
            { className: "dt-head-center", targets: 'no-center-body' },
            { className: "dt-center", targets: '_all' },
            { orderSequence: ["desc", "asc", "desc"], targets: 'desc' },
            { orderData: [0, 2], targets: 'time' }
        ],
        lengthMenu: [
        [5, 10, 25, 50, -1],
        ['5', '10', '25', '50', 'All']
        ]
    });
});