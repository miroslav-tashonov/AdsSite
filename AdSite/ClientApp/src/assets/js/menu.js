"use strict";
$(document).ready(function() {
  $('#main-menu').smartmenus({
      subMenusSubOffsetX: 1,
      subMenusSubOffsetY: -8
  });
  $('#sub-menu').smartmenus({
      subMenusSubOffsetX: 1,
      subMenusSubOffsetY: -8
  })
});

if ($(window).width() > '1200') {
  $('#hover-cls').hover(
    function () {
      $('.sm').addClass('hover-unset');
    }
  )

}
