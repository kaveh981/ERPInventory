


angular.module(appConfig.applicationName).controller('mainController', ['$rootScope', '$scope', '$location', '$http', 'accountFactory', function ($rootScope,$scope, $location, $http, accountFactory) {

    $scope.logOut = function () {
        accountFactory.logOut();
        $location.path('/');
    }
    $rootScope.catData = [];
    $scope.authentication = accountFactory.authentication;

  
}])

