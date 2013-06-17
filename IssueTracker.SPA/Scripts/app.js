'use strict';

/* App Module */
var apiBase = "/IssueTracker.API";

angular.module('issueTracker', ['restangular', 'issueTrackerFilters', 'issueTrackerComponents']).
  config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
      $routeProvider.
          when('/issues', { templateUrl: 'templates/issues/list.html', controller: IssuesCtrl }).
          when('/issues/:id', { templateUrl: 'templates/issues/detail.html', controller: IssueDetailCtrl }).
          when('/login', { templateUrl: 'templates/auth/login.html', controller: LoginCtrl }).
          when('/logout', { controller: LogoutCtrl }).
          otherwise({ redirectTo: '/issues' });

      //TODO $locationProvider.html5Mode(true);
  }])
  .config(function (RestangularProvider) {
      RestangularProvider.setBaseUrl(apiBase);
      RestangularProvider.setErrorInterceptor(function (response) {
          // TODO handle general error
      });
  })
  .run(function ($http, $rootScope, $location) {
      $rootScope.dateFormat = 'yyyy-MM-dd HH:mm';

      $rootScope.isAuthed = false;
      $rootScope.auth = {};

      checkAuth($http, $rootScope, $location);
  });

function checkAuth($http, $rootScope, $location) {
    $http({ method: 'GET', url: apiBase + '/me' })
        .success(function (data) {
            $rootScope.isAuthed = true;
            $rootScope.auth = data;
        })
        .error(function () {
            $rootScope.isAuthed = false;
            $rootScope.auth = {};

            $location.path("/login");
        });
}