'use strict';

angular.module('issueTrackerComponents', ['restangular'])
    .directive('iwComment', function () {
        return {
            scope: { commentDetails: "=iwComment" },
            templateUrl: 'templates/comments/details.html',
            controller: CommentWidgetComponent
        };
    })
    .directive('iwChanges', function () {
        return {
            scope: { changes: "=iwChanges" },
            templateUrl: 'templates/comments/changes.html',
            controller: ChangesWidgetComponent
        };
    })
    .directive('itPerson', function () {
        return {
            scope: { personInput: "=itPerson" },
            templateUrl: 'templates/people/link.html',
            controller: PersonLinkComponent
        };
    })
    .directive('itCustomer', function () {
        return {
            scope: { customerInput: "=itCustomer" },
            templateUrl: 'templates/customers/link.html',
            controller: CustomerLinkComponent
        };
    })
    .directive('itCategory', function () {
        return {
            scope: { categoryInput: "=itCategory" },
            templateUrl: 'templates/categories/link.html',
            controller: CategoryLinkComponent
        };
    })
    .directive('itStatus', function () {
        return {
            scope: { statusInput: "=itStatus" },
            templateUrl: 'templates/statuses/link.html',
            controller: StatusLinkComponent
        };
    })
    .directive('itPriority', function () {
        return {
            scope: { priorityInput: "=itPriority" },
            templateUrl: 'templates/priorities/link.html',
            controller: PriorityLinkComponent
        };
    })
    .directive('itProgress', function () {
        return {
            scope: { progress: "=itProgress" },
            templateUrl: 'templates/progress.html',
            controller: ProgressComponent
        };
    });

function CommentWidgetComponent($scope) {
    $scope.$watch('commentDetails', function () {
        $scope.comment = $scope.commentDetails.Comment;
        $scope.changes = $scope.commentDetails.Changes;
        $scope.poster = $scope.commentDetails.Poster;
    });
}
function ChangesWidgetComponent() {
    
}

function PersonLinkComponent($scope, Restangular) {
    $scope.$watch('personInput', function () {
        if (angular.isNumber($scope.personInput)) {
            Restangular.one('person', $scope.personInput).get().then(function (person) {
                $scope.person = person;
            });
        } else {
            $scope.person = $scope.personInput;
        }
    });
}

function CustomerLinkComponent($scope, Restangular) {
    $scope.$watch('customerInput', function () {
        if (angular.isNumber($scope.customerInput)) {
            Restangular.one('customers', $scope.customerInput).get().then(function (customer) {
                $scope.customer = customer;
            });
        } else {
            $scope.customer = $scope.customerInput;
        }
    });
}

function CategoryLinkComponent($scope, Restangular) {
    $scope.$watch('categoryInput', function () {
        if (angular.isNumber($scope.categoryInput)) {
            Restangular.one('categories', $scope.categoryInput).get().then(function (category) {
                $scope.category = category;
            });
        } else {
            $scope.category = $scope.categoryInput;
        }
    });
}

function StatusLinkComponent($scope, Restangular) {
    $scope.$watch('statusInput', function () {
        if (angular.isNumber($scope.statusInput)) {
            Restangular.one('statuses', $scope.statusInput).get().then(function (status) {
                $scope.status = status;
            });
        } else {
            $scope.status = $scope.statusInput;
        }
    });
}

function PriorityLinkComponent($scope, Restangular) {
    $scope.$watch('priorityInput', function () {
        if (angular.isNumber($scope.priorityInput)) {
            Restangular.one('priorities', $scope.priorityInput).get().then(function (priority) {
                $scope.priority = priority;
            });
        } else {
            $scope.priority = $scope.priorityInput;
        }
    });
}

function ProgressComponent($scope) {
    
}