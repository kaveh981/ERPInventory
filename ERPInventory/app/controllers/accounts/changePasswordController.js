



angular.module('ERPInventory')
    .controller('changePasswordController', ['$scope', 'accountFactory',
        function ($scope, accountFactory) {



        

            $scope.changePassword = function () {
                var data = {
                    OldPassword: $scope.oldPassword,
                    NewPassword: $scope.newPassword,
                    ConfirmPassword: $scope.confirmPassword

                };
                accountFactory.changePassword(data)
                    .success(function () {
                        //getItemCategories();
                        $scope.users.push(data);
                    }).
                    error(function (error) {
                        alert(error.message);
                        $scope.status = 'Unable to insert item Category: ' + error.message;
                    });
            };
        }]);