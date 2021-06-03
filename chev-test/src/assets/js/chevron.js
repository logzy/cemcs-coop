$(function () {
    $(document).scroll(function () {
        var $nav = $(".navbar-sticky-top");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
    });
});

// Form toggle
$('.message a').click(function() {
  $('form').animate({
    height: "toggle",
    opacity: "toggle"
  }, "fast");
});