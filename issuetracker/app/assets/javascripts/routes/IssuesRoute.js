App.IssuesIndexRoute = Ember.Route.extend({
  setupController: function (controller, model) {
    IHID.InfinitePagination.setupRoute(App.Issue, controller, true)
  }
});