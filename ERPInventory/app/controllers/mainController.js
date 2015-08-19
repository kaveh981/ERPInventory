


angular.module(appConfig.applicationName).controller('mainController', ['$rootScope', '$scope', '$location', '$http', 'accountFactory', function ($rootScope,$scope, $location, $http, accountFactory) {


    $scope.logOut = function () {
        accountFactory.logOut();
        $location.path('/');
    }
    $rootScope.catData = [];
    //$scope.logIn = function () {
    //    $scope.currentDisplay = Enums.Views.Login;
    //}

    //$scope.register = function () {
    //    $scope.currentDisplay = Enums.Views.Register;
    //}

    $scope.authentication = accountFactory.authentication;

  
}]);
