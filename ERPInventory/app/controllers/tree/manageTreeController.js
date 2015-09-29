



angular.module(appConfig.applicationName)
    .controller('manageTreeController', ['$scope', '$q', 'treeFactory',
        function ($scope, $q, treeFactory) {


            $scope.loadAsyncData = function (id) {
                var defer = $q.defer();
                id = id == undefined ? "" : id.id;
                treeFactory.GetChildsByCategoryId(id).then(
                 function (response) {
                     defer.resolve(response.data);
                 },
                 function (err) {
                     alert(err.statusText);
                 }
                 )
                return defer.promise;
            };


            $scope.onSelectionChanged = function (items, table) {
                if (items)
                    $scope.query = items[0].id;
                else
                    $scope.query = "";

            };
            $scope.GetDescendentByCategoryId = function (id) {

                treeFactory.GetDescendentByCategoryId(id).then(
                 function (response) {
                     angular.forEach(response.data, function (value, key) {
                         $scope.rowCollection = response.data;
                     })
                 },
                 function (err) {
                     alert(err.statusText);
                 }
                 )
            };


            $scope.displayed = [];

            $scope.FilterCategories = function (tableState) {

                $scope.isLoading = true;

                var pagination = tableState.pagination;
                treeFactory.FilterCategories(tableState).then(function (result) {
                    $scope.displayed = result.data;
                    tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
                    $scope.isLoading = false;
                });
            };


        }]);