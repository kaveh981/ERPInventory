angular.module('ERPInventory')
    .factory('accountFactory', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

        var urlBase = '/api/accounts/';
        var accountFactory = {};
        var _authentication = {
            isAuth: false,
            userName: "",
            useRefreshTokens: false
        };
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

        accountFactory.signIn = function (loginData) {
            var data = "grant_type=password&UserName=" + loginData.username + "&Password=" + loginData.password;

            //if (loginData.useRefreshTokens) {
            //    data = data + "&client_id=" + ngAuthSettings.clientId;
            //}

            var deferred = $q.defer();

            $http.post('oauth/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                //if (loginData.useRefreshTokens) {
                //    localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true });
                //}
                //else {
                    localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false });
                //}
                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;
                _authentication.useRefreshTokens = loginData.useRefreshTokens;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });
            return deferred.promise;
        };

        accountFactory.logOut = function () {

            localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;

        };


        accountFactory.changePassword = function (data) {
            var data = "OldPassword=" + data.OldPassword + "&NewPassword=" + data.NewPassword + "&ConfirmPassword=" + data.ConfirmPassword;
            return $http.post(
                urlBase + "ChangePassword",
                data,
                { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
                ).success(function (data, status, headers, config) {
                    alert("pass changed successfuly");
                })
                 .error(function (data, status, headers, config) {
                     alert(data.exceptionMessage == undefined ? data : data.exceptionMessage)
                 });
        };

        return accountFactory;
    }]);