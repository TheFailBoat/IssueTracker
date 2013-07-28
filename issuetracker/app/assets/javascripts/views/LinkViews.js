App.CategoryLink = Ember.View.extend({
  templateName: 'categories/link',
  linkStyle: function() {
    if(!this.get('content.colour')) return null;
  
    return 'background-color:' + this.get('content.colour') + ';';
  }.property('content.colour')
});

App.PriorityLink = Ember.View.extend({
  templateName: 'priorities/link',
  linkStyle: function() {
    if(!this.get('content.colour')) return null;
  
    return 'background-color:' + this.get('content.colour') + ';';
  }.property('content.colour')
});

App.StatusLink = Ember.View.extend({
  templateName: 'statuses/link',
  linkStyle: function() {
    if(!this.get('content.colour')) return null;
  
    return 'background-color:' + this.get('content.colour') + ';';
  }.property('content.colour')
});

App.UserLink = Ember.View.extend({
  templateName: 'users/link',
});