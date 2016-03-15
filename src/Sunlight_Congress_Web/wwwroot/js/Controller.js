var contact = angular.module('contact', []);

contact.controller("ContactCtrl", function ($scope, $http) {
    $('#AddressSubmit').off('click').on('click', function (e) {
        e.preventDefault();
        var address = $('#Address').val();
        $('#loading').removeClass('hide');
        $http.get('/Home/Legislators', {
            params: {
                address: address
            }
        }).success(function (data) {
            $('#loading').addClass('hide');
            $scope.contacts = data || [];
            $scope.contacts.switchForm = function (event) {
                var formId = $(event.currentTarget).attr('data-form-id');
                $(event.currentTarget).addClass('selected').siblings('.selected').removeClass('selected');
                $('#responseBox form#' + formId).addClass('selected').siblings('.selected').removeClass('selected');
            }
        });
    });
    $('#LocationSubmit').off('click').on('click', function (e) {
        e.preventDefault();
        if (navigator.geolocation)
            navigator.geolocation.getCurrentPosition(getLegislatorsByLocation);
        else
            alert("Geolocation not supported by this browser");
        function getLegislatorsByLocation(position) {
            $http.get('/Home/LegislatorsByLatLong', {
                params: {
                    longitude: parseFloat(position.coords.latitude.toFixed(6)),
                    latitude: parseFloat(position.coords.longitude.toFixed(6))
                }
            }).success(function (data) {
                $scope.contacts = data || [];
                $scope.contacts.switchForm = function (event) {
                    var formId = $(event.currentTarget).attr('data-form-id');
                    $(event.currentTarget).addClass('selected').siblings('.selected').removeClass('selected');
                    $('#responseBox form#' + formId).addClass('selected').siblings('.selected').removeClass('selected');
                }
            });
        }
    });
    $scope.formData = {};
    $scope.processForm = function (toEmails) {
        $scope.formData.toEmails = toEmails;
        $http({
            method: 'POST',
            url: '/Home/SendEmail',
            data: $.param($scope.formData),  // pass in data as strings
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
        .success(function (data) {
            console.log(data);
            $('.formSent').html(data);
        });
    }
});