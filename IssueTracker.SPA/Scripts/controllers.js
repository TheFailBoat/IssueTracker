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
    $scope.tryAuth = function () {
        $rootScope.updateAuth();
        $location.path('/');
        checkAuth($http, $rootScope, $location);
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
function ProcessComment(comment) {
    comment.CreatedAt = new Date(parseInt(comment.CreatedAt.substr(6)));
    if (comment.UpdatedAt) {
        comment.UpdatedAt = new Date(parseInt(comment.UpdatedAt.substr(6)));
    }

    return comment;
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

    var loadComments = function () {
        Restangular.one('issues', $routeParams.id).getList('comments').then(function (comments) {
            $scope.comments = _.map(comments, function (x) { x.Comment = ProcessComment(x.Comment); return x; });
        });
    };
    loadComments();

    $scope.showCommentBox = false;
    $scope.openCommentBox = function () { $scope.showCommentBox = true; };
    $scope.closeCommentBox = function () { $scope.showCommentBox = false; };

    $scope.commentMessage = "";
    $scope.saveComment = function () {
        var comment = { Message: $scope.commentMessage };

        Restangular.one('issues', $routeParams.id).post('comments', comment).then(function (newComment) {
            // TODO scroll to newComment.Id
            $scope.commentMessage = "";
            loadComments();
        });
    };
}

function IssueFormCommon($scope, Restangular) {
    Restangular.all('categories').getList().then(function (categories) {
        $scope.categories = categories;
    });
    Restangular.all('statuses').getList().then(function (statuses) {
        $scope.statuses = statuses;
    });
    Restangular.all('priorities').getList().then(function (priorities) {
        $scope.priorities = priorities;
    });
}

function IssueCreateCtrl($scope, $location, Restangular) {
    $scope.issue = {};

    IssueFormCommon($scope, Restangular);

    $scope.save = function () {
        Restangular.all('issues').post($scope.issue).then(function (result) {
            $location.path('/issues/' + result.Id);
        });
    };
}
function IssueEditCtrl($scope, $routeParams, $location, Restangular) {
    var issue = Restangular.one('issues', $routeParams.id).get();

    issue.then(function (issue) {
        $scope.issue = ProcessIssue(issue.Issue);
    });

    IssueFormCommon($scope, Restangular);

    $scope.save = function () {
        Restangular.all('issues').customPUT($routeParams.id, {}, {}, $scope.issue).then(function (result) {
            $location.path('/issues/' + result.Id);
        });
    };
}