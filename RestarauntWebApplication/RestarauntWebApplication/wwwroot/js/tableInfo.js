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
	
	$('.button').click(
		function () {
			if (value == false) {
				alert("Выберите столик, который хотите забронировать");
				/*$('.userInfoForm').html("Выберите столик, который хотите забронировать").css({ "text-align": "center" }).fadeIn();
				$('.userInfoFormShadow').fadeIn();*/
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
	)
});
