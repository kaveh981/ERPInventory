angular.module('ERPInventory')
    .factory('accountFactory', ['$http','$window', function ($http,$window) {

        var urlBase = '/api/accounts/';
        var accountFactory = {};

        accountFactory.getUsers = function () {
            return $http.get(urlBase + "getItemCategories").success(function (data, status, headers, config) {
            }).
  error(function (data, status, headers, config) {
      alert(data.exceptionMessage == undefined ? data : data.exceptionMessage)
  });
        };
        accountFactory.getItems = function () {
            return $http.get(urlBase + "getItems");
        };


        accountFactory.insertUser = function (user) {
            return $http.post(urlBase + "create", user);
        };

        accountFactory.signIn = function (credential) {
            var data = "grant_type=password&username=" + credential.username + "&password=" + credential.password;
            return $http.post(
                "/oauth/token",
                data,
                { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
                ).success(function (data, status, headers, config) {
                    $window.sessionStorage.token = data.access_token;
                })
                 .error(function (data, status, headers, config) {
                     alert(data.exceptionMessage == undefined ? data : data.exceptionMessage)
                });
        };

        accountFactory.changePassword = function (data) {
            var data = "OldPassword=" + data.OldPassword + "&NewPassword=" + data.NewPassword + "&ConfirmPassword=" + data.ConfirmPassword;
            return $http.post(
                urlBase + "ChangePassword",
                data,
                { headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': 'bearer ' + $window.sessionStorage.token } }
                ).success(function (data, status, headers, config) {
                    $window.sessionStorage.token = data.access_token;
                })
                 .error(function (data, status, headers, config) {
                     alert(data.exceptionMessage == undefined ? data : data.exceptionMessage)
                 });
        };

        return accountFactory;
    }]);