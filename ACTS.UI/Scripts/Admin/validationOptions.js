(function ($) {
    var defaultOptions = {
        errorClass: 'has-error',
        highlight: function (element, errorClass, validClass) {
            if (!($(element).parents('.modal').length)) {
                var form = $(element).closest(".form-group");
                form.addClass(errorClass);
                form.find(".control-label")
                    .not(":has(i.fa.fa-times-circle-o)")
                    .prepend("<i class=\"fa fa-times-circle-o\">&nbsp;</i>");
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            if (!($(element).parents('.modal').length)) {
                var form = $(element).closest(".form-group");
                if (!(form.contents().find("[aria-invalid*='true']").length > 0)) {
                    form.find("i.fa.fa-times-circle-o")
                        .remove();
                    form.removeClass(errorClass);
                }
            }
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
    };
})(jQuery);