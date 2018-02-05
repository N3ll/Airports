(function($) {
    $(function () {
        'use strict';

        //BOOTSTRAP
        var url = window.location;
        console.log(url);
        $('ul.nav a').filter(function () {
            return this.href == url;
        }).parent().addClass('active');

        $(".nav a").on("click", function () {
            $(".nav").find(".active").removeClass("active");
            $(this).parent().addClass("active");
        });

  
    });
})(jQuery);