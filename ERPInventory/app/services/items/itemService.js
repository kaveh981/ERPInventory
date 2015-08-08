angular.module('ERPInventory')
    .factory('itemFactory', ['$http', function ($http) {

        var urlBase = '/api/Item/';
        var itemFactory = {};

        itemFactory.getItemCategories = function () {
            return $http.get(urlBase + "getItemCategories").success(function (data, status, headers, config) {
            }).
  error(function (data, status, headers, config) {
      alert(data.exceptionMessage == undefined ? data : data.exceptionMessage)
  });
        };
        itemFactory.getItems = function () {
            return $http.get(urlBase + "getItems");
        };


        itemFactory.insertItemCategory = function (itemCategory) {
            return $http.post(urlBase + "PostItemCategory", itemCategory);
        };
        itemFactory.updateItemCategory = function (itemCategory) {
            return $http.put(urlBase + "UpdateItemCategory", itemCategory);
        };
        itemFactory.deleteItemCategory = function (idd) {
            return $http.delete(urlBase + "DeleteItemCategory/"+idd);
        };

        itemFactory.insertItem = function (item) {
            return $http.post(urlBase + "PostItem", item);
        };
        itemFactory.updateItem = function (item) {
            return $http.put(urlBase + "UpdateItem", item);
        };
        itemFactory.deleteItem = function (id) {
            return $http.delete(urlBase + "DeleteItem/"+ id);
        };
        return itemFactory;
    }]);