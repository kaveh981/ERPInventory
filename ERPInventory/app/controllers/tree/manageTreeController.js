



angular.module(appConfig.applicationName)
    .controller('manageTreeController', ['$scope', 'treeFactory',
        function ($scope, treeFactory) {

      //      $scope.rowCollection = [
      //{ firstName: 'Laurent', lastName: 'Renard', birthDate: new Date('1987-05-21'), balance: 102, email: 'whatever@gmail.com' },
      //{ firstName: 'Blandine', lastName: 'Faivre', birthDate: new Date('1987-04-25'), balance: -2323.22, email: 'oufblandou@gmail.com' },
      //{ firstName: 'Francoise', lastName: 'Frere', birthDate: new Date('1955-08-27'), balance: 42343, email: 'raymondef@gmail.com' }
      //      ];
            //$scope.getChilds(null);
            $scope.GetDescendentByCategoryId = function (id) {

                treeFactory.GetDescendentByCategoryId(id).then(
                 function (response) {
                     angular.forEach(response.data, function (value, key) {
                         $scope.rowCollection = response.data;
                     })

                     //treeFactory.catData = $scope.data;
                 },
                 function (err) {
                     alert(err.statusText);
                 }
                 )
            };


            var ctrl = this;

            $scope.displayed = [];

            $scope.callServer = function GetDescendentByCategoryId(tableState) {

                $scope.isLoading = true;

                var pagination = tableState.pagination;

                var start = pagination.start || 0;     // This is NOT the page number, but the index of item in the list that you want to use to display the table.
                var number = pagination.number || 2;  // Number of entries showed per page.

                treeFactory.GetDescendentByCategoryId(start, number, tableState).then(function (result) {
                    $scope.displayed = result.data;
                    tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
                    $scope.isLoading = false;
                });
            };


        }]);