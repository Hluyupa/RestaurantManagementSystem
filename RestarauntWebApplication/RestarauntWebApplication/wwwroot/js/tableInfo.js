//const { type } = require("jquery");

$(document).ready(function () {
	var value = false;
	$('.table').click(
		function () {
			value = true;
			$('.table').css({ "opacity": "0.3" })
			$('.userInfo').html($(this).attr('description-data'));
			//$(this).attr('fill', '#C8D35B');
			$(this).css({ "opacity": "0.8" });

			
		}

		
	)


	$('.dateTimeBooking').change(
		function () {
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
	)
	
	
});
