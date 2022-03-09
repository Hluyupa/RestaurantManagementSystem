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

			$.ajax({
				type: "POST",
				url: "Home/GetBookingList",
				success: function (responce) {
					$(responce).each(function () {
						alert(this.TableId);
					})
				},
				f
			});
		}

		
	)


	$('.dateTimeBooking').change(
		function () {
			$.ajax({
				type: "POST",
				url: "/Home/GetBookingList",
				success: function (responce) {
					$(responce).each(function () {
						alert(this.TableId);
					})
				},

				error: function (responce) {
					alert(responce.responseText);
				}
			});
		}
	)
	
	/*$('.button').click(
		function () {
			if (value == false) {
				alert("Выберите столик, который хотите забронировать");
				$('.userInfoForm').html("Выберите столик, который хотите забронировать").css({ "text-align": "center" }).fadeIn();
				$('.userInfoFormShadow').fadeIn();
			}
			else
			{
				$('.userInfoForm').fadeIn();
				$('.userInfoFormShadow').fadeIn();
			}
		}
	)

	$('.userInfoFormShadow').click(
		function () {
			$('.userInfoForm').fadeOut();
			$('.userInfoFormShadow').fadeOut();
		}
	)*/
});
