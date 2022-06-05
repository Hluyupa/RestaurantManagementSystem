$(document).ready(function () {
    $('.bookingButton').on('click', function () {
        $('.modalWindowShadow, .modalWindow').addClass('active');
    });
    $('.modalWindowShadow, .cancelButton').on('click', function () {
        $('.modalWindowShadow, .modalWindow').removeClass('active');
    });
});