'use strict';

function ProcessIssue(issue) {
    issue.CreatedAt = new Date(parseInt(issue.CreatedAt.substr(6)));

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

function IssuePersonCtrl($scope, Restangular) {
    Restangular.one('person', $scope.$parent.issue.ReporterId).get().then(function (person) {
        $scope.person = person;
    });
}