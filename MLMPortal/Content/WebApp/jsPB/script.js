$(document).ready(function ($) {

    var hv = $('.hhdd').text();
    //alert(hv);
		function animateElements() {
			$('.progressbar').each(function () {
				var elementPos = $(this).offset().top;
				var topOfWindow = $(window).scrollTop();
				var percent = $(this).find('.circle').attr('data-percent');
				var percentage = parseInt(percent, 10) / parseInt(100, 10);
				var animate = $(this).data('animate');
				if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
					$(this).data('animate', true);
					$(this).find('.circle').circleProgress({
						startAngle: -Math.PI / 2,
						value: percent / 100,
						size: 180,
						thickness: 30,
						emptyFill: "rgba(0,0,0, .2)",
						fill: {
							color: '#ffaf04'
						}
					}).on('circle-animation-progress', function (event, progress, stepValue) { 
					   // $(this).find('div').text((stepValue * 100).toFixed(1) + "%");
					    $(this).find('div').text(hv+ " IFC/Day");//+ "Coin"
					}).stop();
				}
			});
		}

		// Show animated elements
		animateElements();
		$(window).scroll(animateElements);
		
		 
	}); //end document ready function