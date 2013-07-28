App.IssuesIndexController = Ember.ArrayController.extend(
  IHID.InfinitePagination.ControllerMixin,
  App.LoginMixin, {
  paginationParams: function(){
    return $.extend(this._super(), { pageSize: 10 })
  }
});

App.IssueController = Ember.ObjectController.extend(
  App.LoginMixin, {
    
});
App.IssueIndexController = Ember.ObjectController.extend(
  App.LoginMixin, {
    
});