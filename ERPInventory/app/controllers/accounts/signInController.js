



angular.module('ERPInventory')
    .controller('signInController', ['$scope', 'accountFactory',
        function ($scope, accountFactory) {


            $scope.user = { username: 'john.doe', password: 'foobar' };
            $scope.message = '';
            $scope.signIn = function () {
                $http
                  .post('/authenticate', $scope.user)
                  .success(function (data, status, headers, config) {
                      $window.sessionStorage.token = data.token;
                      $scope.message = 'Welcome';
                  })
                  .error(function (data, status, headers, config) {
                      // Erase the token if the user fails to log in
                      delete $window.sessionStorage.token;

                      // Handle login errors here
                      $scope.message = 'Error: Invalid user or password';
                  });
            };

            $scope.signIn = function () {
                var user = {
                    username: $scope.userName,
                    password: $scope.password,
                    grant_type: "password"

                };
                accountFactory.signIn(user)
                    .success(function () {
                        alert("loged in");
                        $scope.users.push(user);
                    }).
                    error(function (error) {
                        alert(error.message);
                        $scope.status = 'Unable to insert item Category: ' + error.message;
                    });
            };
        }]);