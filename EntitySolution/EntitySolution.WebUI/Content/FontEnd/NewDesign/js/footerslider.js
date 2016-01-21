$(function() {
    $("#carousel_man").jCarouselLite({
       btnNext: ".next, .nextsmall",
        btnPrev: ".prev, .prevsmall",
//		mouseWheel: true,
		circular: true,
		auto: 5000,
		visible: 6,  //set this to 8 if you have more than 8 brands else set to manufacturer number you have
//		scroll: 7,	// uncomment (enable) this option if you have more than 8 brands. or set it as you like ;)
		speed: 1000
	});
});

