'use strict';

/* App Module */
angular.module('issueTracker', ['restangular', 'issueTrackerFilters', 'issueTrackerComponents']).
  config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
      $routeProvider.
          when('/issues', { templateUrl: 'templates/issues/list.html', controller: IssuesCtrl }).
          when('/issues/create', { templateUrl: 'templates/issues/form.html', controller: IssueCreateCtrl }).
          when('/issues/:id', { templateUrl: 'templates/issues/detail.html', controller: IssueDetailCtrl }).
          when('/issues/:id/edit', { templateUrl: 'templates/issues/form.html', controller: IssueEditCtrl }).
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
      $rootScope.login = { username: "", password: "" };
      $rootScope.auth = {};

      $rootScope.updateAuth = function() {
          $http.defaults.headers.common['Authorization'] = 'Basic ' + Base64.encode($rootScope.login.username + ':' + $rootScope.login.password);
      };
      $rootScope.$watch('login.login + login.password', $rootScope.updateAuth);

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
            $rootScope.login.password = "";

            $location.path("/login");
        });
}