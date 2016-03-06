$(function () {
	var startInput = $('#StartDate');
	var startMoment = moment(startInput.val(), ['MM/DD/YYYY hh:mm:ss A', 'YYYY-MM-DDTHH:mm:ssZ']);
	startInput.val(startMoment.format());

	var endInput = $('#EndDate');
	var endMoment = moment(endInput.val(), ['MM/DD/YYYY hh:mm:ss A', 'YYYY-MM-DDTHH:mm:ssZ']);
	endInput.val(endMoment.format());

	var dateFilter = $('#datefilter');

	dateFilter.daterangepicker({
		showDropdowns: true,
		timePicker: true,
		startDate: startMoment,
		endDate: endMoment,
		locale: {
			format: 'MM/DD/YYYY hh:mm:ss A'
		}
	}, 
	function cb(start, end) {
		startInput.val(start.format());
		endInput.val(end.format());
		dateFilter.html(start.format('MM/DD/YYYY hh:mm:ss A') + ' - ' + end.format('MM/DD/YYYY hh:mm:ss A'));
	});
});