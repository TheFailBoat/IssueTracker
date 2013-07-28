App.IssuesIndexRoute = Ember.Route.extend({
  setupController: function (controller, model) {
    controller.set('content', App.Issue.find({ page: controller.get('currentPage'), pageSize: 10 }));
  }
});