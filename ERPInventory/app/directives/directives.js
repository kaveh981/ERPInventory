
angular.module(appConfig.applicationName).directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    // console.info(elem.val() === $(firstPassword).val());
                    ctrl.$setValidity('pwmatch', elem.val() === $(firstPassword).val());
                });
            });
        }
    }
}]);

angular.module(appConfig.applicationName)
    .directive('searchWatchModel', function () {
        return {
            require: '^stTable',
            scope: {
                searchWatchModel: '='
            },
            link: function (scope, ele, attr, ctrl) {
                scope.$watch('searchWatchModel', function (val) {
                    ctrl.search(val, "parentId");
                });

            }
        };
    });

angular.module(appConfig.applicationName)
    .directive('pageSelect', function () {
        return {
            restrict: 'E',
            template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="selectPage(inputPage)">',
            link: function (scope, element, attrs) {
                scope.$watch('currentPage', function (c) {
                    scope.inputPage = c;
                });
            }
        }
    });


angular.module(appConfig.applicationName)
.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);

            element.css('width', ratio + '%');

        }
    };
});


angular.module(appConfig.applicationName)
.directive('ngKaveh', function () {
    return {
        restrict: 'A',
        //template: '<div> collapsed= {{scope.collapsed}} </div>',
        link: function (scope, iElement, iAttrs, ctrl) {
            //alert(iAttrs.haschildren)
            //scope.collapsed = true;
            //iElement.removeClass().addClass("glyphicon");
            //c = ctrl;
            ////alert(iAttrs.haschildren);
            ////ng-class="{'glyphicon-chevron-right': collapsed, 'glyphicon-chevron-down': !collapsed}"
            //if (iAttrs.haschildren) {
            //    alert("has")
            //    iElement.addClass("glyphicon-chevron-right");
            //}
            //else {
            //    alert("not")
            //    iElement.addClass("glyphicon-chevron-down");
            //}
            //var s = scope;
            //var i = iElement;
            //var a = iAttrs;
        }
    }
});