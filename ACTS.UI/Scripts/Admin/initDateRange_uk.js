$(function () {
	moment.locale('uk');

	var startInput = $('.start');
	var startMoment = moment(startInput.val(), ["DD.MM.YYYY hh:mm:ss", "YYYY-MM-DDTHH:mm:ssZ"]);
	startInput.val(startMoment.format());

	var endInput = $('.end');
	var endMoment = moment(endInput.val(), ["DD.MM.YYYY hh:mm:ss", "YYYY-MM-DDTHH:mm:ssZ"]);
	endInput.val(endMoment.format());

	var dateRange = $('#daterange');
	 
	dateRange.daterangepicker({
		showDropdowns: true,
		timePicker: true,
		timePicker24Hour: true,
		startDate: startMoment,
		endDate: endMoment,
		locale: {
			format: 'DD.MM.YYYY hh:mm:ss',
			applyLabel: 'Застосувати',
			cancelLabel: 'Відмінити',
			fromLabel: 'З',
			toLabel: 'По',
			customRangeLabel: 'Налаштувати',
		}
	},
	function cb(start, end) {
	    startInput.val(start.format());
	    endInput.val(end.format());
	    dateRange.html(start.format('DD.MM.YYYY hh:mm:ss') + ' - ' + end.format('DD.MM.YYYY hh:mm:ss'));
	});
});