jQuery(document).ready(function ($) {

    var Site = {
        init: function () {
            Site.slider.init();
            Site.scrollUp.init();
            Site.readProgress.init();
            Site.shareButtons.init();
        },
        slider: {
            init: function () {
                var $currentSlide = $(".active.slide");
                var firstRun = true;
                var switching = false;

                setTimeout(nextSlide, 5000);

                function nextSlide($nextSlide) {
                    if (!switching) {
                        switching = true;
                        var $currentSlide = $(".active.slide");
                        var auto = true;

                        if (typeof $nextSlide == 'undefined') {
                            $nextSlide = $currentSlide.next();
                            if (!$nextSlide.length) {
                                $nextSlide = $(".slide").first();
                            }
                        }
                        else {
                            auto = false;
                        }

                        if (firstRun) {
                            $nextSlide.fadeIn(function () {
                                $currentSlide.removeClass('active');
                                $nextSlide.addClass('active');
                                switching = false;
                            });
                        }
                        else {
                            $nextSlide.css('z-index', 1).show().show();
                            $currentSlide.css('z-index', 2).fadeOut(500, function () {
                                $nextSlide.addClass('active');
                                $currentSlide.removeClass('active');
                                $nextSlide.css("display", "");
                                $currentSlide.css("display", "");
                                switching = false;
                            });
                        }

                        firstRun = false;

                        var timer = null;
                        if (auto) {
                            navigatorNext();
                            timer = setTimeout(nextSlide, 5000);
                        }
                    }

                }

                function navigatorNext($nextButton) {
                    var $currentButton = $("#slider .navigation ul .active");

                    if (typeof $nextButton == 'undefined') {
                        $nextButton = $currentButton.next();

                        if (!$nextButton.length) {
                            $nextButton = $("#slider .navigation ul li").first();
                        }
                    }

                    $currentButton.removeClass("active");
                    $nextButton.addClass("active");
                }

                $("#slider .navigation ul li").click(function () {
                    if (!switching && !$(this).hasClass('active')) {
                        nextSlide($('.slide').eq($(this).index()));
                        navigatorNext($(this));
                        switching = false;
                    }
                });
            }
        },
        scrollUp: {
            active: false,
            $btn: $('.scrollUp'),

            init: function () {
                var ns = Site.scrollUp;

                if ($(window).width() < 768) return; // dont show it on mobiles

                ns.$btn.click(ns.scrollUpClick);

                $(window).scroll(function () {
                    var top = $(window).scrollTop();

                    if (top > 600) {
                        if (!ns.$btn.is(':visible')) {
                            ns.$btn.fadeIn();
                        }
                    }
                    else {
                        if (ns.$btn.is(':visible')) {
                            ns.$btn.fadeOut();
                        }
                    }
                });
            },

            scrollUpClick: function () {
                $('html, body').animate({ scrollTop: 0 }, 500);
            }
        },
        readProgress: {
            init: function () {
                var $anchor = $('.read-style');

                if ($anchor.length) {

                    var getMax = function () {
                        return $anchor.height() - $anchor.offset().top - 220;
                    }

                    var getValue = function () {
                        var val = $(window).scrollTop() - $anchor.offset().top;
                        val = val < 0 ? 0 : val;
                        return val;
                    }

                    var progressBar = $('.progress-bar'),
                        max = getMax(),
                        value, width;

                    var getWidth = function () {
                        // Calculate width in percentage
                        value = getValue();
                        width = (value / max) * 100;
                        width = width + '%';
                        return width;
                    }

                    var setWidth = function () {
                        progressBar.css({ width: getWidth() });
                    }

                    $(document).on('scroll', setWidth);
                    $(window).on('resize', function () {
                        // Need to reset the Max attr
                        max = getMax();
                        setWidth();
                    });
                }
            }
        },
        shareButtons: {
            init: function () {
                $('.share-btn').click(function (e) {
                    e.preventDefault();

                    var target = $(this).data('target');
                    if (!target) return;

                    window.open(target + window.location.href,
                        "_blank",
                        "resizable=yes, scrollbars=yes, titlebar=yes, width=600, height=400, top=100, left=400"
                    );
                });
            }
        }
    };

    Site.init();
});