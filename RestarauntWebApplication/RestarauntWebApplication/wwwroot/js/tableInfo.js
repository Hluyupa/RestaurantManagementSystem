$(document).ready(function () {

	$('.table').click(
		function () {

			$('.table').css({ "opacity": "0.3" })
			$('.userInfo').html($(this).attr('description-data'));
			//$(this).attr('fill', '#C8D35B');
			$(this).css({ "opacity": "0.8" });

		}
	)
});
