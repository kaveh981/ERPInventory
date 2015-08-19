
var appConfig = {};

appConfig.applicationName = "ERPInventory";

appConfig.serviceBase = "http://localhost:9594/";

var MyHomeApp = angular.module('ERPInventory', ['ngRoute', 'ui.bootstrap', 'xeditable','ui.tree', 'LocalStorageModule']);

MyHomeApp.config(['$routeProvider',
                function ($routeProvider) {
                    $routeProvider.
                         when('/changePassword', {
                             templateUrl: 'app/views/accounts/changePassword.html',
                             controller: 'changePasswordController'
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

//'use strict';

//var app = angular.module('ERPInventory', [
//  'ui.router',
//  'ui.bootstrap',
//  'ui.router.tabs'
//]);

//app.config(['$stateProvider', function ($stateProvider) {
//    $stateProvider.state('user', {
//        url: '',
//        controller: 'itemCategoryController',
//        templateUrl: 'app/views/items/addItemCategories.html'
//    }).state('user.accounts', {
//        url: '/user/accounts',
//        controller: 'menuController',
//        templateUrl: 'app/views/menu/menu.html'
//    }).state('user.settings', {
//        url: '/user/settings',
//        controller: 'SettingsCtrl',
//        templateUrl: 'app/views/menu/menu.html'
//    }).state('user.heading2', {
//        url: '/user/heading2',
//        templateUrl: 'app/views/menu/menu.html'
//    })
//        .state('user.settings.one', {
//            url: '/one',
//            template: '<div>Settings nested route 1</div>'
//        }).state('user.settings.two', {
//            url: '/two',
//            template: '<div>Settings nested route 2</div>'
//        });
//}]);


//'use strict';


//'use strict';

//var SettingsCtrl = ['$rootScope', '$scope', '$stateParams', function ($rootScope, $scope) {

//    $scope.initialise = function () {

//        $scope.tabData = [
//          {
//              heading: 'One',
//              route: 'user.settings.one',
//              url: 'user/settings/one/:test',
//              controller: 'tabController'
//          },
//          {
//              heading: 'Two',
//              route: 'user.settings.two'
//          }
//        ];
//    };

//    $scope.initialise();
//}];

//angular.module('ERPInventory').controller('SettingsCtrl', SettingsCtrl);

