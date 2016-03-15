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
});