angular.module(appConfig.applicationName)
    .factory('treeFactory', ['$http', function ($http) {

        var urlBase = '/api/Category/';
        var treeFactory = {};

        var catData = [];

        treeFactory.catData = null;

        treeFactory.GetChildsByCategoryId = function (id) {
            catData = $http.get(urlBase + "GetChildsByCategoryId", { params: { id: id } }).success(function (data, status, headers, config) {

            }).
  error(function (data, status, headers, config) {
      alert(data.exceptionMessage == undefined ? data : data.exceptionMessage)
  });
            return catData;
        };
        treeFactory.putCategory = function (category) {
            return $http.put(urlBase + "PutCategory", category);
        };
        treeFactory.deleteCategory = function (id) {
            return $http.delete(urlBase + "Delete", { params: { id: id } });
        };

        treeFactory.postCategory = function (category) {
            return $http.post(urlBase + "PostCategory",   category);
        };

        treeFactory.MoveAndSortCategory = function (data) {
            return $http.put(urlBase + "MoveAndSortCategory", data);
        };

        return treeFactory;
    }]);