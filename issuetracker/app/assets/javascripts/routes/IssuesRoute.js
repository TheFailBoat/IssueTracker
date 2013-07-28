App.IssuesIndexRoute = Ember.Route.extend({
  setupController: function (controller, model) {
    IHID.InfinitePagination.setupRoute(App.Issue, controller, true)
  }
});

App.IssueRoute = Ember.Route.extend({
  model: function(params) {
    return App.Issue.find(params.issue_id);
  }
});
App.IssueIndexRoute = Ember.Route.extend({
  model: function(params) {
    return this.modelFor('issue');
  }
});