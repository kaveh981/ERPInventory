



angular.module('ERPInventory')
    .controller('itemController', ['$scope', 'itemFactory',
        function ($scope, itemFactory) {

            getItemCategories();
            getItems();


            function getItemCategories() {
                itemFactory.getItemCategories()
                    .success(function (itemCategories) {
                        $scope.itemCategories = itemCategories;
                    })
                    .error(function (error) {
                        $scope.status = 'Unable to load item categories data: ' + error.message;
                    });
            }

            function getItems() {
                itemFactory.getItems()
                    .success(function (items) {
                        $scope.items = items;
                    })
                    .error(function (error) {
                        $scope.status = 'Unable to load items data: ' + error.message;
                    });
            }

            $scope.insertItem = function () {
                var item = {
                    CategoryId: $scope.categoryId,
                    ImportedSaleTax: $scope.importedSaleTax,
                    Price: $scope.price,
                    ItemName: $scope.itemName
                };
                itemFactory.insertItem(item)
                    .success(function () {
                        getItems();
                    }).
                    error(function (error) {
                        alert(error.message);
                        $scope.status = 'Unable to insert item: ' + error.message;
                    });
            };

            $scope.updateItem = function (item,id) {
                var item = {
                    Id:id,
                    CategoryId:item.categoryId,
                    ImportedSaleTax:item.importedSaleTax,
                    Price:item.price,
                    ItemName: item.itemName
                };
                itemFactory.updateItem(item)
                    .success(function () {
                        getItems();
                    }).
                    error(function (error) {
                        alert(error.message);
                        $scope.status = 'Unable to update item: ' + error.message;
                    });
            };

            $scope.deleteItem = function (id) {

                itemFactory.deleteItem(id)
                    .success(function () {
                        getItems();
                    }).
                    error(function (error) {
                        alert(error.message);
                        $scope.status = 'Unable to delete item: ' + error.message;
                    });
            };

        }]);