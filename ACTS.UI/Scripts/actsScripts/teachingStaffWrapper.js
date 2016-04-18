$(document).ready(function ($) {
    var $descriptionBlock = $('#description');

    $('.person__wrapper').click(function () {
        if (getDescription() == $(this).data('id')) {
            hideDescription();
        }
        else {
            hideDescription();
            showDescription($(this));
        }
    });

    /**
     * Show description
     */
    function showDescription($block) {
        $block.addClass('active');
        $descriptionBlock.detach().insertAfter($block.parent());
        $descriptionBlock.data('id', $block.data('id'));

        $('html, body').animate({
            scrollTop: $block.offset().top
        }, 500);

        $("#h_avatar").attr('src', $block.data('img'));
        $("#h_name").text($block.data('name'));
        $("#h_position").text($block.data('position'));

        $("#h_description").html($block.find('.person__description').html());
        $("#h_links").html($block.find('.person__links').html());

        $descriptionBlock.show();
    }

    /**
     * Hide description block
     */
    function hideDescription() {
        $('.row div').removeClass('active');
        $descriptionBlock.hide();
    }

    /**
     * Get description block ID
     * @returns {*}
     */
    function getDescription() {
        if (!$descriptionBlock.is(':visible')) {
            return false;
        }
        else {
            return $descriptionBlock.data('id');
        }
    }

});