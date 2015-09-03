(function (ng) {
    angular.module(appConfig.applicationName).directive('stDateRange', ['$timeout', function ($timeout) {
            return {
                restrict: 'E',
                require: '^stTable',
                scope: {
                    from: '=',
                    to: '='
                },
                templateUrl: 'app/directives/rangeSelector/stDateRange.html',

                link: function (scope, element, attr, table) {

                    var inputs = element.find('input');
                    var inputBefore = ng.element(inputs[0]);
                    var inputAfter = ng.element(inputs[1]);
                    var predicateName = attr.predicate;


                    [inputBefore, inputAfter].forEach(function (input) {

                        input.bind('blur', function () {


                            var query = {};

                            if (!scope.isBeforeOpen && !scope.isAfterOpen) {

                                if (scope.from) {
                                    query.from = scope.from;
                                }

                                if (scope.to) {
                                    query.to = scope.to;
                                }

                                scope.$apply(function () {
                                    table.search(query, predicateName);
                                })
                            }
                        });
                    });

                    function open(from) {
                        return function ($event) {
                            $event.preventDefault();
                            $event.stopPropagation();

                            if (from) {
                                scope.isBeforeOpen = true;
                            } else {
                                scope.isAfterOpen = true;
                            }
                        }
                    }

                    scope.openBefore = open(true);
                    scope.openAfter = open();
                }
            }
        }])
        .directive('stNumberRange', ['$timeout', function ($timeout) {
            return {
                restrict: 'E',
                require: '^stTable',
                scope: {
                    from: '=',
                    to: '='
                },
                templateUrl: 'app/directives/rangeSelector/stNumberRange.html',
                link: function (scope, element, attr, table) {
                    var inputs = element.find('input');
                    var inputLower = ng.element(inputs[0]);
                    var inputHigher = ng.element(inputs[1]);
                    var predicateName = attr.predicate;

                    [inputLower, inputHigher].forEach(function (input, index) {

                        input.bind('blur', function () {
                            var query = {};

                            if (scope.from) {
                                query.from = scope.from;
                            }

                            if (scope.to) {
                                query.to = scope.to;
                            }

                            scope.$apply(function () {
                                table.search(query, predicateName)
                            });
                        });
                    });
                }
            };
        }])
        .filter('customFilter', ['$filter', function ($filter) {
            var filterFilter = $filter('filter');
            var standardComparator = function standardComparator(obj, text) {
                text = ('' + text).toLowerCase();
                return ('' + obj).toLowerCase().indexOf(text) > -1;
            };

            return function customFilter(array, expression) {
                function customComparator(actual, expected) {

                    var isBeforeActivated = expected.from;
                    var isAfterActivated = expected.to;
                    var isLower = expected.lower;
                    var isHigher = expected.higher;
                    var higherLimit;
                    var lowerLimit;
                    var itemDate;
                    var queryDate;


                    if (ng.isObject(expected)) {

                        //date range
                        if (expected.from || expected.to) {
                            try {
                                if (isBeforeActivated) {
                                    higherLimit = expected.from;

                                    itemDate = new Date(actual);
                                    queryDate = new Date(higherLimit);

                                    if (itemDate > queryDate) {
                                        return false;
                                    }
                                }

                                if (isAfterActivated) {
                                    lowerLimit = expected.to;


                                    itemDate = new Date(actual);
                                    queryDate = new Date(lowerLimit);

                                    if (itemDate < queryDate) {
                                        return false;
                                    }
                                }

                                return true;
                            } catch (e) {
                                return false;
                            }

                        } else if (isLower || isHigher) {
                            //number range
                            if (isLower) {
                                higherLimit = expected.lower;

                                if (actual > higherLimit) {
                                    return false;
                                }
                            }

                            if (isHigher) {
                                lowerLimit = expected.higher;
                                if (actual < lowerLimit) {
                                    return false;
                                }
                            }

                            return true;
                        }
                        //etc

                        return true;

                    }
                    return standardComparator(actual, expected);
                }

                var output = filterFilter(array, expression, customComparator);
                return output;
            };
        }]);
})(angular);




