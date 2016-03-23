// Write your Javascript code.
$(document).ready(function () {
    //$('#AddressSubmit').off('click').on('click', function (e) {
    //    e.preventDefault();
    //    var address = $('#Address').val();
    //    $.ajax({
    //        url: "Home/Legislators?address=" + address,
    //        success: function (data) {
    //            console.log(data);
    //        }
    //    });
    //});

    $('.fullpage').fullpage({
        navigation : true,
        autoScrolling : false
    });
    
    var currentLeft = ($(window).width()/2) - 250; 
    $('#slideshow li').each(function(e, i){
        $(this).css('left', currentLeft);
        currentLeft += 600;
    });
    
    function checkPrevAndNextButtons() {
       if($('#slideshow li.selected').is(':first-child')) {
          $('.prev').addClass('hide');   
       } else if($('#slideshow li.selected').is(':last-child')) {
          $('.next').addClass('hide'); 
       } else {
           $('.next, .prev').removeClass('hide');
       }
    }
    checkPrevAndNextButtons();    
    
    $('.prev').off('click').on('click', function(e){
        e.preventDefault();
        $('#slideshow li').animate({ 'left' : '+=600px'});
        if($('#slideshow li.selected').is(':first-child'))
            $('#slideshow li.selected').removeClass('selected').siblings(':last-child').addClass('selected');
        else
            $('#slideshow li.selected').removeClass('selected').prev().addClass('selected');
        checkPrevAndNextButtons();
    });
    $('.next').off('click').on('click', function(e){
        e.preventDefault();
        $('#slideshow li').animate({ 'left' : '-=600px'});
        if($('#slideshow li.selected').is(':last-child'))
            $('#slideshow li.selected').removeClass('selected').siblings(':first-child').addClass('selected');
        else
            $('#slideshow li.selected').removeClass('selected').next().addClass('selected');
        checkPrevAndNextButtons();
    });
});