'use strict';

/* App Module */

angular.module('issueTracker', ['restangular', 'issueTrackerFilters']).
  config(['$routeProvider', function ($routeProvider) {
      $routeProvider.
          when('/issues', { templateUrl: 'templates/issues/list.html', controller: IssuesCtrl }).
          when('/issues/:id', { templateUrl: 'templates/issues/detail.html', controller: IssueDetailCtrl }).
          otherwise({ redirectTo: '/issues' });
  }])
  .config(function (RestangularProvider) {
      RestangularProvider.setBaseUrl("/IssueTracker.API");
  });
