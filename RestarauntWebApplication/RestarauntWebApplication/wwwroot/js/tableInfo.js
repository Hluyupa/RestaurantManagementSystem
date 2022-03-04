$(document).ready(function () {

	$('.table').click(
		function () {
			$('.userInfo').html($(this).attr('description-data'));
		}
	)
});
