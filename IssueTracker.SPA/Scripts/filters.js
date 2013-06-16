'use strict';

angular.module('issueTrackerFilters', ['restangular']).
    filter('person', function ($scope, Restangular) {
        return function (input) {
            var id = parseInt(input);

            if (!$scope.people[id]) {
                return "Loading...";
            } else {
                return $scope.people[id].Name;
            }
        };
    });
