(function (self, $, undefined) {

    Index.Start = function () {


        $('.owl-carousel').owlCarousel({
            dots: true,
            nav: true,
            navText: ["<i class='glyphicon glyphicon-chevron-left' style='color:#005eb8; font-size: 2em; line-height:unset'></i>", "<span class='glyphicon glyphicon-chevron-right' style='color:#005eb8; font-size: 2em; line-height:unset'></span>"],
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 3
                },
                1000: {
                    items: 4
                }
            }
        });

    };

}(window.Index = window.Index || {}, jQuery));

jQuery(function () {
    Index.Start();
});
