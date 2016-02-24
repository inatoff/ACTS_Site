$(document).ready(function () {
    $('#dataTablePlagin').DataTable({
        order: [],
        language: {
            "sProcessing": "Зачекайте...",
            "sLengthMenu": "Показати _MENU_ записів",
            "sZeroRecords": "Записи відсутні.",
            "sInfo": "Записи з _START_ по _END_ із _TOTAL_ записів",
            "sInfoEmpty": "Записи з 0 по 0 із 0 записів",
            "sInfoFiltered": "(відфільтровано з _MAX_ записів)",
            "sInfoPostFix": "",
            "sSearch": "Пошук:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Перша",
                "sPrevious": "Попередня",
                "sNext": "Наступна",
                "sLast": "Остання"
            },
            "oAria": {
                "sSortAscending": ": активувати для сортування стовпців за зростанням",
                "sSortDescending": ": активувати для сортування стовпців за спаданням"
            }
        },
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
        ['5', '10', '25', '50', 'Всі']
        ]
    });
});