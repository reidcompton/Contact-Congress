var contact = angular.module('contact', []);

contact.controller("ContactCtrl", function ($scope, $http) {
    $('#AddressSubmit').off('click').on('click', function (e) {
        e.preventDefault();
        var address = $('#Address').val();
        $http.get('/Home/Legislators', {
            params: {
                address: address
            }
        }).success(function (data) {
            $scope.contacts = data || [];
            $scope.contacts.switchForm = function (event) {
                var formId = $(event.currentTarget).attr('data-form-id');
                $(event.currentTarget).addClass('selected').siblings('.selected').removeClass('selected');
                $('#responseBox form#' + formId).addClass('selected').siblings('.selected').removeClass('selected');
            }
        });
    });
});