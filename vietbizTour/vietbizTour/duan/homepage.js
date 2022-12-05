function loadSlideFullpage() {
	$(window).width() <= 992 ? $("#section_banner .bxslider .item").css("height", "") : $("#section_banner .bxslider .item").height($(window).height())
}
$(document).ready(function() {
	awe_category();
	$('.nav-category .fa').on('click', function(e){
		e.preventDefault();
		var $this = $(this);
		$this.parents('.nav-category').find('.dropdown-menu').stop().slideToggle();
		return false;
	});
	var a = $("#section_banner .bxslider .item");
	loadSlideFullpage(), $("#section_banner .bxslider").bxSlider({
		mode: "fade",
		easing: "ease",
		speed: 1500,
		pause: 5e3,
		pager: !0,
		controls: !1,
		auto: !0,
		onSliderLoad: function(i) {
			var t = "";
			t = $(a[i]).data("title"), $(".slider-content").append(t), $(".slider-content .doanimation").addClass("animated")
		},
		onSlideBefore: function(i, t, e) {
			title = $(a[e]).data("title"), $(".slider-content").html(title), $(".slider-content .doanimation").addClass("animated go")
		}
	}),
		$("#section_news .slick").slick({
		dots: !1,
		infinite: !1,
		speed: 300,
		slidesToShow: 4,
		slidesToScroll: 4,
		responsive: [{
			breakpoint: 1024,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 3,
				infinite: !0,
				dots: !0,
				arrows: !1
			}
		}, {
			breakpoint: 600,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 2,
				dots: !0
			}
		}, {
			breakpoint: 480,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				dots: !0
			}
		}]
	}),
		$(".fancy-trigger").click(function(a) {
		$("a:first", $(this).next()).trigger("click")
	}),
		$("#master_plan area").each(function() {
		var a = $(this);
		$(a).hover(function() {
			$("#master_plan img.img-hover").css("opacity", "0"), $("#master_plan img." + $(a).attr("data-key")).css("opacity", "1"), $("#master_plan img.map").stop().animate({
				opacity: "0"
			}, 500)
		}, function() {
			$("#master_plan img.map").stop().animate({
				opacity: "1"
			}, 500, function() {
				$("#master_plan img." + $(a).attr("data-key")).css("opacity", "0")
			})
		})
	}),
		$("#master_utilities area").each(function() {
		var a = $(this);
		$(a).hover(function() {
			$("#master_utilities img.img-hover").css("opacity", "0"), $("#master_utilities img." + $(a).attr("data-key")).css("opacity", "1"), $("#master_utilities img.map").stop().animate({
				opacity: "0"
			}, 500)
		}, function() {
			$("#master_utilities img.map").stop().animate({
				opacity: "1"
			}, 500, function() {
				$("#master_utilities img." + $(a).attr("data-key")).css("opacity", "0")
			})
		})
	}),
		$(".ls-block .item").click(function(a) {
		var i = $(this).data("link");
		"" != i && (i = "#" + i, $(".ls-block .item").removeClass("active"), $(this).addClass("active"), $(".master-block").addClass("hidden"), $(i).removeClass("hidden"), 0 == isBlockC && "#master_blockc" == i && (isBlockC = !0, $("#master_blockc img[usemap]").mapster({
			fillColor: "3dad65",
			fillOpacity: 0,
			stroke: !1,
			singleSelect: !0,
			clickNavigate: !1
		})))
	}), $(".nav-menu .menu-link a").click(function(a) {
		var i = $(this).data("link");
		if ("" != i) {
			var t = $("#" + i).offset().top - 67;
			$(window).width() <= 1190 && (t += 7), $("html, body").animate({
				scrollTop: t
			}, 500)
		}
		$(".collapse").collapse("hide")
	});
	var i = $.urlParam("idsection");
	if (null != i && "" != i) {
		var t = $("#" + i).offset().top - 67;
		$(window).width() <= 992 && (t -= 7), $("html, body").animate({
			scrollTop: t
		}, 500)
	}
	$(document).click(function(a) {
		$(a.target).is("a") || $(".collapse").collapse("hide")
	}), $(".navbar-ex1-collapse").on("show.bs.collapse", function() {
		$(".page-header .navbar-toggle").addClass("active")
	}), $(".navbar-ex1-collapse").on("hide.bs.collapse", function() {
		$(".page-header .navbar-toggle").removeClass("active")
	})
})
function awe_category(){
	$('.nav-category .fa-angle-down').click(function(e){
		$(this).parent().toggleClass('active');
	});
} window.awe_category=awe_category;
function awe_menumobile(){
	$('.menu-bar').click(function(e){
		e.preventDefault();
		$('#nav').toggleClass('open');
	});
	$('.nav-category .fa').click(function(e){		
		e.preventDefault();
		$(this).parent().parent().toggleClass('open');
	});
} window.awe_menumobile=awe_menumobile;