(function($) {
	"use strict";
	
	// ______________Active Class
	$(".app-sidebar #parentVerticalTab a").each(function() {
		var pageUrl = window.location.href.split(/[?#]/)[0];
		if (this.href == pageUrl) {
			$(this).addClass("active");
			$(this).parent().parent().parent().addClass("active"); // add active to li of the current link
			$(this).parent().parent().prev().addClass("active"); // add active class to an anchor
			$(this).parent().parent().prev().click(); // click the item to make it drop
		}
	});
	
	// ______________Active Class
	$(".app-sidebar .toggle-menu.side-menu a").each(function() {
		var pageUrl = window.location.href.split(/[?#]/)[0];
		if (this.href == pageUrl) {
			$(this).addClass("active");
			$(this).parent().addClass("active"); // add active to li of the current link
			$(this).parent().parent().prev().addClass("active"); // add active class to an anchor
			$(this).parent().parent().prev().click(); // click the item to make it drop
		}
	});
	
	// ______________ SWITCHER1
	$('#myonoffswitch').click(function () {      /* box Switch*/
		if (this.checked) {
			$('body').addClass('left-menu-dark');
			$('body').removeClass('left-menu-light');
			localStorage.setItem("left-menu-dark", "True");
		}
		else {
			$('body').removeClass('left-menu-dark');
			localStorage.setItem("left-menu-dark", "false");
		}
	});
	
	
	// ______________ SWITCHER2
	$('#myonoffswitch2').click(function () {      /* box Switch*/
		if (this.checked) {
			$('body').addClass('left-menu-light');
			$('body').removeClass('left-menu-dark');
			localStorage.setItem("left-menu-light", "True");
		}
		else {
			$('body').removeClass('left-menu-light');
			localStorage.setItem("left-menu-light", "false");
		}
	});
	
	// ______________ SWITCHER3
	$('#myonoffswitch3').click(function () {      /* box Switch*/
		if (this.checked) {
			$('body').addClass('boxed');
			localStorage.setItem("boxed", "True");
		}
		else {
			$('body').removeClass('boxed');
			localStorage.setItem("boxed", "false");
		}
	});
	
	// ______________ SWITCHER4
	$('#myonoffswitch4').click(function () {      /* box Switch*/
		if (this.checked) {
			$('body').addClass('horizontal-menudark');
			localStorage.setItem("horizontal-menudark", "True");
		}
		else {
			$('body').removeClass('horizontal-menudark');
			localStorage.setItem("horizontal-menudark", "false");
		}
	});
	
	
	// ______________ SWITCHER5
	$('#myonoffswitch5').click(function () {      /* box Switch*/
		if (this.checked) {
			$('body').addClass('cardcolor-light');
			localStorage.setItem("cardcolor-light", "True");
		}
		else {
			$('body').removeClass('cardcolor-light');
			localStorage.setItem("cardcolor-light", "false");
		}
	});
	
	// ______________ SWITCHER6
	$('#myonoffswitch6').click(function () {      /* box Switch*/
		if (this.checked) {
			$('body').addClass('body-background');
			localStorage.setItem("body-background", "True");
		}
		else {
			$('body').removeClass('body-background');
			localStorage.setItem("body-background", "false");
		}
	});
	
	// ______________ SWITCHER7
	$('#myonoffswitch7').click(function () {      /* box Switch*/
		if (this.checked) {
			$('body').addClass('body-card-shadow');
			localStorage.setItem("body-card-shadow", "True");
		}
		else {
			$('body').removeClass('body-card-shadow');
			localStorage.setItem("body-card-shadow", "false");
		}
	});
	
	// ______________ SWITCHER8
	$('#myonoffswitch8').click(function () {      /* box Switch*/
		if (this.checked) {
			$('body').addClass('container-fullwidth');
			localStorage.setItem("container-fullwidth", "True");
		}
		else {
			$('body').removeClass('container-fullwidth');
			localStorage.setItem("container-fullwidth", "false");
		}
	});
	
	// ______________ body-horizontal-left-icon
	$('.horizontal-left-icon').click(function () {      /* box Switch*/
		$('body').removeClass('body-horizontal-right-icon');
		$('body').addClass('body-horizontal-left-icon');
	});
	
	
	// ______________ body-horizontal-right-icon
	$('.horizontal-right-icon').click(function () {    
		$('body').addClass('body-horizontal-right-icon');
		$('body').removeClass('body-horizontal-left-icon');
	});
	
	
	// ______________Horizontal-menu
	$("a[data-theme]").click(function() {
		$("head link#theme").attr("href", $(this).data("theme"));
		$(this).toggleClass('active').siblings().removeClass('active');
	});
	$("a[data-font]").click(function() {
		$("head link#font").attr("href", $(this).data("font"));
		$(this).toggleClass('active').siblings().removeClass('active');
	});
	$("a[data-effect]").on("click", function(e) {
		$("head link#effect").attr("href", $(this).data("effect"));
		$(this).toggleClass('active').siblings().removeClass('active');
	});
	
	// ______________Headerfixed
	$(window).on("scroll", function(e){
		if ($(window).scrollTop() >= 66) {
			$('.header').addClass('fixed-header');
		}
		else {
			$('.header').removeClass('fixed-header');
		}
    });
	
	// ______________Cover Image
	$(".cover-image").each(function() {
		var attr = $(this).attr('data-image-src');
		if (typeof attr !== typeof undefined && attr !== false) {
			$(this).css('background', 'url(' + attr + ') center center');
		}
	});
	
	// ______________Ms Menu Trigger
	$(function() {
		if ($('#ms-menu-trigger')[0]) {
			$('body').on('click', '#ms-menu-trigger', function() {
				$('.ms-menu').toggleClass('toggled');
			});
		}
	});
	
	// ______________VerticalTab
	$('#parentVerticalTab').easyResponsiveTabs({
		type: 'vertical',
		width: 'auto', 
		fit: true, 
		closed: 'accordion',
		tabidentify: 'hor_1',
		activate: function(event) {
			var $tab = $(this);
			var $info = $('#nested-tabInfo2');
			var $name = $('span', $info);
			$name.text($tab.text());
			$info.show();
		}
	});
	// ______________Full Screen
	$(document).on("click", "#fullscreen-button", function toggleFullScreen() {
		if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
			if (document.documentElement.requestFullScreen) {
				document.documentElement.requestFullScreen();
			} else if (document.documentElement.mozRequestFullScreen) {
				document.documentElement.mozRequestFullScreen();
			} else if (document.documentElement.webkitRequestFullScreen) {
				document.documentElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
			} else if (document.documentElement.msRequestFullscreen) {
				document.documentElement.msRequestFullscreen();
			}
		} else {
			if (document.cancelFullScreen) {
				document.cancelFullScreen();
			} else if (document.mozCancelFullScreen) {
				document.mozCancelFullScreen();
			} else if (document.webkitCancelFullScreen) {
				document.webkitCancelFullScreen();
			} else if (document.msExitFullscreen) {
				document.msExitFullscreen();
			}
		}
	})
	
	// ______________ PAGE LOADING
	$(window).on("load", function(e) {
		$("#global-loader").fadeOut("slow");
	})
	
	// ______________ BACK TO TOP BUTTON
	$(window).on("scroll", function(e) {
		if ($(this).scrollTop() > 0) {
			$('#back-to-top').fadeIn('slow');
		} else {
			$('#back-to-top').fadeOut('slow');
		}
	});
	$(document).on("click", "#back-to-top", function(e) {
		$("html, body").animate({
			scrollTop: 0
		}, 600);
		return false;
	});
	
	// ______________ Star Rating
	var ratingOptions = {
		selectors: {
			starsSelector: '.rating-stars',
			starSelector: '.rating-star',
			starActiveClass: 'is--active',
			starHoverClass: 'is--hover',
			starNoHoverClass: 'is--no-hover',
			targetFormElementSelector: '.rating-value'
		}
	};
	$(".rating-stars").ratingStars(ratingOptions);
	
	
	/* boYSIqMee+p4uAjskftSrErYaF9PDNDn+EGSzR9N2BspYI8=
		feCz66HNQhyoUIndT6pXQpWta+PA3e1h3yExMyH1EsOo6f8PXnNPyHGLRfchOSF9WSX7exs= */
	// ______________Chart-circle
	if ($('.chart-circle').length) {
		$('.chart-circle').each(function() {
			let $this = $(this);
			$this.circleProgress({
				fill: {
					color: $this.attr('data-color')
				},
				size: $this.height(),
				startAngle: -Math.PI / 4 * 2,
				emptyFill: '#f9f9f9',
				lineCap: 'round'
			});
		});
	}
	
	/*----GlobalSearch----*/
	$(document).on("click", "[data-toggle='search']", function(e) {
		var body = $("body");

		if(body.hasClass('search-gone')) {
			body.addClass('search-gone');
			body.removeClass('search-show');
		}else{
			body.removeClass('search-gone');
			body.addClass('search-show');
		}
	});
	var toggleSidebar = function() {
		var w = $(window);
		if(w.outerWidth() <= 1920) {
			$("body").addClass("sidebar-gone");
			$(document).off("click", "body").on("click", "body", function(e) {
				if($(e.target).hasClass('sidebar-show') || $(e.target).hasClass('search-show')) {
					$("body").removeClass("sidebar-show");
					$("body").addClass("sidebar-gone");
					$("body").removeClass("search-show");
				}
			});
		}else{
			$("body").removeClass("sidebar-gone");
		}
	}
	toggleSidebar();
	$(window).resize(toggleSidebar);
	
	
	const DIV_CARD = 'div.card';
	// ______________Tooltip
	$('[data-toggle="tooltip"]').tooltip();
	
	// ______________Popover
	$('[data-toggle="popover"]').popover({
		html: true
	});
	
	// ______________Remove Card
	$(document).on('click', '[data-toggle="card-remove"]', function(e) {
		let $card = $(this).closest(DIV_CARD);
		$card.remove();
		e.preventDefault();
		return false;
	});
	
	// ______________Card Collapse
	$(document).on('click', '[data-toggle="card-collapse"]', function(e) {
		let $card = $(this).closest(DIV_CARD);
		$card.toggleClass('card-collapsed');
		e.preventDefault();
		return false;
	});
	
	// ______________Card Fullscreen
	$(document).on('click', '[data-toggle="card-fullscreen"]', function(e) {
		let $card = $(this).closest(DIV_CARD);
		$card.toggleClass('card-fullscreen').removeClass('card-collapsed');
		e.preventDefault();
		return false;
	});
	
	//Date range as a button
	$('#daterange-btn').daterangepicker({
		ranges: {
			'Today': [moment(), moment()],
			'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
			'Last 7 Days': [moment().subtract(6, 'days'), moment()],
			'Last 30 Days': [moment().subtract(29, 'days'), moment()],
			'This Month': [moment().startOf('month'), moment().endOf('month')],
			'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
		},
		startDate: moment().subtract(29, 'days'),
		endDate: moment()
	}, function(start, end) {
		$('#daterange-btn span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'))
	})
	
})(jQuery);