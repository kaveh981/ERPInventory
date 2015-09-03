angular.module(appConfig.applicationName)
    .factory('treeFactory', ['$http', '$q', function ($http, $q) {

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
        treeFactory.GetDescendentByCategoryId2 = function (id) {
            catData = $http.get(urlBase + "GetDescendentByCategoryId", { params: { id: id } }).success(function (data, status, headers, config) {

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
            return $http.post(urlBase + "PostCategory", category);
        };

        treeFactory.MoveAndSortCategory = function (data) {
            return $http.put(urlBase + "MoveAndSortCategory", data);
        };


  //      treeFactory.GetDescendentByCategoryId = function GetChildsByCategoryId(start, number, params) {

  //          var deferred = $q.defer();

  //          var sort = params.sort.predicate;
  //          var serach = params.search.predicateObject;

  //          $http.get(urlBase + "GetDescendentByCategoryId", { params: { id: 'd23045be-e14d-e511-b97c-002433726434', start: start, number: number } }).success(function (data, status, headers, config) {
  //              deferred.resolve({
  //                  data: data.Results,
  //                  numberOfPages: Math.ceil(data.RowCount / number)
  //              });
  //          }).
  //error(function (data, status, headers, config) {
  //    alert(data.exceptionMessage == undefined ? data : data.exceptionMessage)
  //});
  //          return deferred.promise;
  //      }

        treeFactory.FilterCategories = function FilterCategories(start, number, params) {

            var deferred = $q.defer();
            var search = {};
            search = params.search.predicateObject != undefined ? params.search.predicateObject : {};
            search.start = 0;
            search.number = number;
            search.sortBy = params.sort;
            $http.post(urlBase + "FilterCategories",search).success(function (data, status, headers, config) {
                deferred.resolve({
                    data: data.Results,
                    numberOfPages: Math.ceil(data.RowCount / number)
                });
            }).
            error(function (data, status, headers, config) {
                alert(data.exceptionMessage == undefined ? data : data.exceptionMessage)
            });
            return deferred.promise;
        }



        return treeFactory;
    }]);