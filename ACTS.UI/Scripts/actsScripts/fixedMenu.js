$(document).ready(function () {

    var menu = $('.menu');
    var origOffsetY = menu.offset().top;

    function scroll() {
        if ($(window).scrollTop() >= origOffsetY) {
            $('.menu').addClass('navbar-fixed-top');
            $('.menu').removeClass('navbar-static-top');
        } else {
            $('.menu').removeClass('navbar-fixed-top');
            $('.menu').addClass('navbar-static-top');
        }


    }

    document.onscroll = scroll;

});