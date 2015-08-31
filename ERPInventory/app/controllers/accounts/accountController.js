



angular.module(appConfig.applicationName)
    .controller('accountController', ['$scope', '$modalInstance', 'accountFactory',
        function ($scope, $modalInstance, accountFactory) {


            function getUsers() {
                itemFactory.getItemCategories()
                    .success(function (itemCategories) {
                        $scope.itemCategories = itemCategories;
                    })
                    .error(function (error) {
                        alert(error.exceptionMessage);
                        $scope.status = 'Unable to load item categories data: ' + error.message;
                    });
            }


            $scope.insertUser = function () {
                var user = {
                    Username: $scope.userName,
                    Email: $scope.email,
                    FirstName: $scope.firstName,
                    LastName: $scope.lastName,
                    Username: $scope.userName,
                    Password: $scope.password,
                    ConfirmPassword: $scope.confirmPassword

                };
                accountFactory.insertUser(user)
                   .then(function (response) {
                       $modalInstance.close();
                   },
             function (err) {
                 alert(err);
             });
            };
        }]);