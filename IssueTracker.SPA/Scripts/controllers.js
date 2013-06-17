'use strict';

// General
function MainNavCtrl($scope, $location) {
    $scope.location = $location;
    $scope.links = [
        { url: "#/issues", name: "List Issues" },
        { url: "#/issues/create", name: "Create Issue" }
    ];
}

// Auth
function LoginCtrl($scope, $http, $rootScope, $location) {
    $scope.username = "";
    $scope.password = "";
    $scope.error = false;

    $scope.login = function () {
        $scope.error = false;

        $http.post(apiBase + '/auth/credentials', { username: $scope.username, password: $scope.password })
            .success(function () {
                $location.path('/');
                checkAuth($http, $rootScope, $location);
            })
            .error(function () {
                $scope.password = "";
                $scope.error = true;
            });
    };
}

function LogoutCtrl() {

}

// Issues
function ProcessIssue(issue) {
    issue.CreatedAt = new Date(parseInt(issue.CreatedAt.substr(6)));
    if (issue.UpdatedAt) {
        issue.UpdatedAt = new Date(parseInt(issue.UpdatedAt.substr(6)));
    }

    return issue;
}

function IssuesCtrl($scope, Restangular) {
    Restangular.all('issues').getList().then(function (issues) {
        $scope.issues = _.map(issues, ProcessIssue);
    });
}

function IssueDetailCtrl($scope, $routeParams, Restangular) {
    Restangular.one('issues', $routeParams.id).get().then(function (issue) {
        $scope.issue = ProcessIssue(issue.Issue);

        $scope.category = issue.Category;
        $scope.reporter = issue.Reporter;
        $scope.priority = issue.Priority;
        $scope.status = issue.Status;
    });
}
