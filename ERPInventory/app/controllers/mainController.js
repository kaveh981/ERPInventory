


angular.module(appConfig.applicationName).controller('mainController', ['$scope', '$location', '$http',  'accountFactory', function ($scope, $location, $http, accountFactory) {


    $scope.logOut = function () {
        accountFactory.logOut();
        $location.path('/');
    }

    //$scope.logIn = function () {
    //    $scope.currentDisplay = Enums.Views.Login;
    //}

    //$scope.register = function () {
    //    $scope.currentDisplay = Enums.Views.Register;
    //}

    $scope.authentication = accountFactory.authentication;

  
}]);
