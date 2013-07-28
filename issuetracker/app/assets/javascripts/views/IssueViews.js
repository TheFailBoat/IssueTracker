App.IssueItemView = Ember.View.extend({
  cssClasses: function() {
    var classes = 'row';
    
    if(this.get('content.deleted')) {
      classes += ' deleted'
    }
    
    return classes;
  }.property('content.deleted')
});