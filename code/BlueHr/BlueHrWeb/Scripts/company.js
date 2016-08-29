var Company = {};

Company.init = function () {
    $('.table-body').css({ height: $(window).height() - 310 + 'px' });

    $(window).resize(function () {
        $('.table-body').css({ height: $(window).height() - 310 + 'px' });
    });

    $('.table-body').mCustomScrollbar({
        scrollInertia: 600,        autoDraggerLength: false
    });
}