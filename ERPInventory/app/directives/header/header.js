
angular.module('ERPInventory').controller('headerController', function ($scope, $modal) {

    $scope.showLoginForm = function () {

        var modalInstance = $modal.open({

            templateUrl: 'app/views/accounts/signIn.html',
            controller: 'signInController',
        });
        
    };

    $scope.showRegisterForm = function () {

        var modalInstance = $modal.open({

            templateUrl: 'app/views/accounts/addUsers.html',
            controller: 'accountController',

        });

    };



});

angular.module('ERPInventory').directive('erpHeader', function () {
    return {
        templateUrl: function (elem, attr) {
            return 'app/directives/header/header.html';
        }
    };
});
