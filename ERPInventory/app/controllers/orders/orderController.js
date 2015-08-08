



angular.module('ERPInventory')
    .controller('orderController', ['$scope', 'orderFactory',
        function ($scope, orderFactory) {

            getOrders();



            function getOrders() {
                orderFactory.getOrders()
                    .success(function (orders) {
                        $scope.orders = orders;
                    })
                    .error(function (error) {
                        $scope.status = 'Unable to load orders data: ' + error.message;
                    });
            }


            $scope.getOrderById = function (id) {
                orderFactory.getOrdeForPrint(id)
                    .success(function (printOrder) {
                        $scope.printOrder = printOrder;
                    }).
                    error(function (error) {
                        alert(error.message);
                    });
            };

            $scope.getOrderDetailsByOrderId = function (id) {
                orderFactory.getOrderDetailsByOrderId(id)
                    .success(function (orderDetails) {
                        $scope.orderDetails = orderDetails;
                    }).
                    error(function (error) {
                        alert(error.message);
                    });
            };

            $scope.deleteOrder = function (id) {
 
                orderFactory.deleteOrder(id)
                    .success(function () {
                        alert("Order deleted successfully!");
                        $scope.orderDetails = [];

                    }).
                    error(function (error) {
                        alert(error.message);
                        $scope.status = 'Unable to delete order: ' + error.message;
                    });
            };
        }]);