$(function () {
	var startInput = $('.start');
	var startMoment = moment(startInput.val(), ['MM/DD/YYYY hh:mm:ss A', 'YYYY-MM-DDTHH:mm:ssZ']);
	startInput.val(startMoment.format());

	var endInput = $('.end');
	var endMoment = moment(endInput.val(), ['MM/DD/YYYY hh:mm:ss A', 'YYYY-MM-DDTHH:mm:ssZ']);
	endInput.val(endMoment.format());

	var dateRange = $('#daterange');

	dateRange.daterangepicker({
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
		dateRange.html(start.format('MM/DD/YYYY hh:mm:ss A') + ' - ' + end.format('MM/DD/YYYY hh:mm:ss A'));
	});
});