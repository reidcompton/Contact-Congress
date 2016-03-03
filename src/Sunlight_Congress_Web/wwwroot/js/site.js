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

    function switchForm() {
        var formId = $(this).attr('data-form-id');
        $(this).addClass('selected').siblings('selected').removeClass('selected');
        $('#responseBox form#' + formId).addClass('selected').siblings('selected').removeClass('selected');
    }
});