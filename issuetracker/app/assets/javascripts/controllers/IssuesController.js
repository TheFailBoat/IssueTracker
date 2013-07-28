App.IssuesIndexController = Ember.ArrayController.extend({
  currentPage: 1,
  canLoadMore: function() {
    // can we load more entries? In this example only 10 pages are possible to fetch ...
    return this.get('currentPage') < 10;
  }.property('currentPage'),
});