



angular.module('ERPInventory')
    .controller('accountController', ['$scope', 'accountFactory',
        function ($scope, accountFactory) {


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
                    .success(function () {
                        //getItemCategories();
                        $scope.users.push(user);
                    }).
                    error(function (error) {
                        alert(error.message);
                        $scope.status = 'Unable to insert item Category: ' + error.message;
                    });
            };
        }]);