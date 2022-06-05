var booking = {};

function sendBooking() {

    booking.TableId = $("input[name='TableId']").val();
    booking.DateBooking = $("input[name='DateBooking']").val();
}

function sendVisitor() {
    booking.Visitor = {
        VisitorFullName: $("input[name='VisitorFullName']").val(),
        VisitorTelephone: $("input[name='VisitorTelephone']").val(),
        VisitorEmail: $("input[name='VisitorEmail']").val(),
    };
   
    $.ajax({
        type: "POST",
        url: "/Home/CreateBooking",
        data: booking,
        success: function (responce) {
            UpdateBookingList();
        },
        faulure: function (responce) {
            alert(responce.responseText);
        },
        error: function (responce) {
            alert(responce.responseText);
        }
    });
    
}

function UpdateBookingList() {
    var dateNow = new Date($('.dateTimeBooking').val());

    $.ajax({
        type: "GET",
        url: "/Home/GetBookingList",

        success: function (responce) {
            responce.forEach(function (item, i, arr) {

                var dateStartBooking = new Date(item.dateBooking);
                var dateFinishBooking = new Date(dateStartBooking);
                dateFinishBooking.setHours(dateFinishBooking.getHours() + 2);

                if ((dateNow >= dateStartBooking) && (dateNow <= dateFinishBooking)) {
                    $('#' + item.tableId).attr('fill', '#FF6666')
                }
                else {
                    $('.table').attr('fill', '#C8D35B');
                }
            });

        },
        faulure: function (responce) {
            alert(responce.responseText);
        },
        error: function (responce) {
            alert(responce.responseText);
        }
    });
}

$(document).ready(function () {
    $('.bookingButton').click(function ()
    {
        sendBooking();
    });
    $('.okButton').click(function () {
        sendVisitor();
       
    });
    /*$('.okButton').click(function () {
        s
    })*/
});