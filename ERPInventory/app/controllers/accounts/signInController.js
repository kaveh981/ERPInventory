



angular.module('ERPInventory')
    .controller('signInController', ['$scope', '$modalInstance', 'accountFactory',
        function ($scope, $modalInstance, accountFactory) {

            $scope.message = '';


            $scope.signIn = function () {
                var user = {
                    username: $scope.userName,
                    password: $scope.password,
                    grant_type: "password"

                };
                accountFactory.signIn(user).then(function (response) {
                    $modalInstance.close();
                },
             function (err) {
                 alert(err);
             });

                //.success(function () {
                //    alert("loged in");
                //    $scope.users.push(user);
                //}).
                //error(function (error) {
                //    alert(error.message);
                //    $scope.status = 'Unable to insert item Category: ' + error.message;
                //});
            };
        }]);