
var appConfig = {};

appConfig.applicationName = "ERPInventory";

appConfig.serviceBase = "http://localhost:9594/";

var MyHomeApp = angular.module('ERPInventory', ['ngRoute', 'ui.bootstrap', 'xeditable', 'ui.tree', 'LocalStorageModule', 'smart-table','hierarchical-selector']);

MyHomeApp.config(['$routeProvider',
                function ($routeProvider) {
                    $routeProvider.
                         when('/changePassword', {
                             templateUrl: 'app/views/accounts/changePassword.html',
                             controller: 'changePasswordController'
                         }).
                        when('/manageTree', {
                            templateUrl: 'app/views/tree/manageTree.html',
                            controller: 'manageTreeController'
                        }).
                        when('/home', {
                            templateUrl: 'app/views/Home/home.html'
                        }).
                      otherwise({
                          redirectTo: 'home'
                      });
                }]);
MyHomeApp.config(function (localStorageServiceProvider) {
    localStorageServiceProvider
      .setPrefix('ERPInventory');
});

MyHomeApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

angular.module(appConfig.applicationName).run(['$route', '$rootScope', '$location', 'accountFactory', function ($route, $rootScope, $location, accountFactory) {

    accountFactory.fillAuthData();

  
}]);

var _authentication = {
    isAuth: false,
    userName: "nn",
    useRefreshTokens: false
};


