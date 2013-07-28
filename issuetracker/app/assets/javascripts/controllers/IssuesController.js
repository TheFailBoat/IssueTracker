App.IssuesIndexController = Ember.ArrayController.extend(
  IHID.InfinitePagination.ControllerMixin, {
  paginationParams: function(){
    return $.extend(this._super(), { pageSize: 10 })
  }
});