$(document).ready(function () {

    $('#firstFloor').click(function () {
        if ($('#firstFloor').is(':checked')) {
            $('.firstFloor').css({ 'visibility': 'visible' });
            $('.secondFloor').css({ 'visibility': 'collapse' });
        }
    });
    $('#secondFloor').click(function () {
        if ($('#secondFloor').is(':checked')) {
            $('.secondFloor').css({ 'visibility': 'visible' });
            $('.firstFloor').css({ 'visibility': 'collapse' });
        }
    });
});