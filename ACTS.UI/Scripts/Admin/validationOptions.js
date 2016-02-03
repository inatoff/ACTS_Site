(function ($) {
    var defaultOptions = {
        errorClass: 'has-error',
        highlight: function (element, errorClass, validClass) {
            var form = $(element).closest(".form-group");
            form.addClass('has-error');
            form.find(".control-label").not(":has(i.fa.fa-times-circle-o)").prepend("<i class=\"fa fa-times-circle-o\">&nbsp;</i>");
        },
        unhighlight: function (element, errorClass, validClass) {
            var form = $(element).closest(".form-group");
            form.find("i.fa.fa-times-circle-o").remove();
            form.removeClass('has-error');
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
    };
})(jQuery);