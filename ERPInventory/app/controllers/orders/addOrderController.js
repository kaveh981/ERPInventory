



angular.module('ERPInventory')
    .controller('addOrderController', ['$scope', 'orderFactory','itemFactory',
        function ($scope, orderFactory,itemFactory) {

            getItems();
            function getItems() {
                itemFactory.getItems()
                    .success(function (items) {
                        $scope.items = items;
                    })
                    .error(function (error) {
                        $scope.status = 'Unable to load item data: ' + error.message;
                    });
            }
            $scope.orderDetails = [];
            $scope.insertOrderDetail = function () {
                var orderDetail = {
                    itemId: $scope.itemId.id,
                    name: $scope.itemId.itemName,
                    price:$scope.itemId.price
                };
                $scope.orderDetails.push(orderDetail);
                
            };

            $scope.insertOrder = function () {
                var order = {
                    OrderDetails: $scope.orderDetails
                };
                orderFactory.insertOrder(order)
                    .success(function () {
                        alert("Order added successfully!");
                        $scope.orderDetails = [];
                        
                    }).
                    error(function (error) {
                        alert(error.message);
                        $scope.status = 'Unable to insert order: ' + error.message;
                    });
            };

        }]);