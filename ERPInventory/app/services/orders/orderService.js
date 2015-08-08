angular.module('ERPInventory')
    .factory('orderFactory', ['$http', function ($http) {

        var urlBase = "/api/Order/";
        var orderFactory = {};

        orderFactory.getOrdeForPrint = function (id) {
            return $http.get(urlBase + "getOrdeForPrint/" + id);
        };

        orderFactory.getOrderDetailsByOrderId = function (id) {
            return $http.get(urlBase + "getOrderDetailsByOrderId/" + id);
        };

        orderFactory.getOrders = function () {
            return $http.get(urlBase + "getOrders");
        };


        orderFactory.insertOrder = function (order) {
            return $http.post(urlBase + "PostOrder", order);
        };
        orderFactory.deleteOrder = function (id) {
            return $http.delete(urlBase + "DeleteOrder/"+ id);
        };

        return orderFactory;
    }]);