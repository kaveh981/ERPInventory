
angular.module(appConfig.applicationName).controller('treeController', ['$rootScope', '$scope', 'treeFactory', function ($rootScope, $scope, treeFactory) {
    {

        if (treeFactory.catData == null || treeFactory.catData.length < 1) {

            GetChildsByCategoryId("");
        }
        else {
            $scope.data = treeFactory.catData;
        }

        $scope.dynamicPopover = {
            templateUrl: 'addCategoryTemplate.html',
            title: 'Title'
        };

        $scope.editPopover = {
            templateUrl: 'editCategoryTemplate.html',
            title: 'Title'
        };


        $scope.getChilds = function (scope, id) {
            var nodeData = scope.$modelValue;
            nodeData.nodes = [];
            treeFactory.GetChildsByCategoryId(id).then(
             function (response) {
                 angular.forEach(response.data, function (value, key) {
                     nodeData.nodes.push(value);
                 })
                 scope.toggle(scope);
                 treeFactory.catData = $scope.data;
             },
             function (err) {
                 alert(err.statusText);
             }
             )
        };


        $scope.moveLastToTheBegginig = function () {
            var a = $scope.data.pop();
            $scope.data.splice(0, 0, a);
        };


        $scope.postCategory = function (scope, id) {
            var category = {
                Cat_Title: scope.categoryTitle,
                Cat_ParentId: id
            };
            treeFactory.postCategory(category).then(function (response) {
                var cat = response.data;
                var nodeData = scope.$modelValue;
                if (!scope.collapsed) {
                    nodeData == undefined ? $scope.data.push({ id: cat.CategoryId, title: cat.Cat_Title })
                          : nodeData.nodes.push({ id: cat.CategoryId, title: cat.Cat_Title });
                }

                else {
                    $scope.getChilds(scope, id);
                }

                treeFactory.catData = $scope.data;
            },
         function (err) {
             alert(err.statusText);
         });
        }

        $scope.putCategory = function (scope, id) {
            var category = {
                Cat_Title: scope.editTitle,
                CategoryId: id
            };
            treeFactory.putCategory(category).then(function (response) {
                var cat = response.data;
                var nodeData = scope.$modelValue;
                nodeData.title = scope.editTitle;
                treeFactory.catData = $scope.data;
            },
         function (err) {
             alert(err.statusText);
         });
        }

        $scope.deleteCategory = function (scope, id) {
            if (confirm("Are you sure?!")) {
                treeFactory.deleteCategory(id).then(function (response) {
                    scope.remove();
                    treeFactory.catData = $scope.data;
                },
             function (err) {
                 alert(err.statusText);
             });
            }
        };


        $scope.treeOptions = {
            dropped: function (e) {
                var category = e.source.nodeScope.$modelValue;
                var parent = e.dest.nodesScope.$parent.$modelValue;
                var destOrder = getLevelNodes(e.dest.nodesScope.$modelValue)
                var data = {
                    id: category.id,
                    parentId: (parent != undefined ? parent.id : null),
                    destOrder: destOrder
                }
                treeFactory.MoveAndSortCategory(data).then(function (response) {
                    treeFactory.catData = $scope.data;
                },
       function (err) {
           alert(err.statusText);
       });
            }
        };

        var getRootNodesScope = function () {
            return angular.element(document.getElementById("tree-root")).scope();
        };

        $scope.collapseAll = function () {
            var scope = getRootNodesScope();
            scope.collapseAll();
        };

        $scope.expandAll = function () {
            var scope = getRootNodesScope();
            scope.expandAll();
        };


        function GetChildsByCategoryId(id) {
            treeFactory.GetChildsByCategoryId(id).then(
                function (response) {
                    $scope.data = response.data;
                    treeFactory.catData = $scope.data;
                    if (id == null) {
                        var scope = getRootNodesScope();
                        scope.collapseAll();
                    }
                },
                function (err) {
                    alert(err.statusText);
                }
                )
        }

        function getLevelNodes(nodes) {
            var sortedNodes = [];
            angular.forEach(nodes, function (value, key) {
                sortedNodes.push(value.id);
            })
            return sortedNodes;
        }


        //if (treeFactory.catData != null) {
        //    $scope = treeFactory.catData;
        //    var test = $scope.data;
        //    $scope.data = $scope.data;
        //}
    }
}]);



angular.module(appConfig.applicationName).directive('erpTree', function () {
    return {
        templateUrl: function (elem, attr) {
            return 'app/directives/Tree/tree.html';
        }
    };
});
