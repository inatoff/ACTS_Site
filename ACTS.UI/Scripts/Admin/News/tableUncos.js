//$(document).ready(function () {
//	$('#dataTablePlagin').DataTable();
//});

$(document).ready(function () {
    var table = $('#dataTablePlagin').DataTable({
        scrollX: true,
        scrollY: false,
        scrollCollapse: true,
        //select: {
        //	style: 'single'
        //},
        columns: [
       { className: "dt-center" },
       { className: "dt-center" },
       { className: "dt-center" },
       { className: "dt-center" },
       { className: "dt-center", orderable: false },
       { className: "dt-head-center" },
       { className: "dt-center", orderable: false }
        ],
        //lengthChange: true,
        //buttons: false,
        lengthMenu: [
        [5, 10, 25, 50, -1],
        ['5', '10', '25', '50', 'All']
        ]
        //buttons: [
        //	'pageLength'
        //{
        //	extend: 'selectedSingle',
        //	text: 'Edit',
        //	action: function (e, dt, node, config) {
        //		var newsId = dt.row({ selected: true }).data()[0];
        //		//var href = '@Url.Action("Edit", "News", new { })/' + newsId
        //		@*location.href = @Url.Action("EditNews", "Admin", new {
        //								@:id
        //							})*@
        //		$.ajax({ type: "GET", url: "@Url.Action("Edit", "News")", data: { NewsID: newsId }, dataType: "html"});
        //		console.log(newsId);
        //	}
        //},
        //{
        //	extend: 'selectedSingle',
        //	text: 'Delete',
        //	action: function (e, dt, node, config) {
        //		var newsId = dt.row({ selected: true }).data()[0];
        //		//var href = '@Url.Action("Delete", "News", new { })/' + newsId
        //		@*location.href = @Url.Action("EditNews", "Admin", new {
        //								@:id
        //							})*@
        //		$.ajax({ type: "POST", url: "@Url.Action("Delete", "News")", data: { NewsID: newsId }, dataType: "html"});
        //		console.log(newsId);
        //	}
        //}
        //]
    });

    //table.buttons().container()
    //	.appendTo('#dataTablePlagin_wrapper .col-sm-6:eq(0)');
})