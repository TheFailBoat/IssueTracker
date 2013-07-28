App.IssueItemView = Ember.View.extend({
  classNames: ['issue-list'],
  classNameBindings: ['deleted'],
  
  deleted: function() {
    return this.get('content.deleted');
  }.property('content.deleted')
});