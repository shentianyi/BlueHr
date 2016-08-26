var Layout = {};

Layout.init = function () {
    $('.nav-left dt').removeClass('active');

    var pathname = window.location.pathname.split('/')[1];

    console.log(pathname);

    switch (pathname) {
        case "Company":
            $('.nav-company').addClass('active');
            break;
        default:
            break;
    }
}