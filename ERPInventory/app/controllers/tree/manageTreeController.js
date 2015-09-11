



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

            $scope.callServer = function FilterCategories(tableState) {

                $scope.isLoading = true;

                var pagination = tableState.pagination;

                //var start = pagination.start || 0;     // This is NOT the page number, but the index of item in the list that you want to use to display the table.
                //var number = pagination.number || 2;  // Number of entries showed per page.
                treeFactory.FilterCategories(tableState).then(function (result) {
                    $scope.displayed = result.data;
                    tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
                    $scope.isLoading = false;
                });
            };


        }]);