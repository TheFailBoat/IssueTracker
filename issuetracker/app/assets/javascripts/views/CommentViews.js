App.CommentItemView = Ember.View.extend({
  templateName: 'comments/item',
  comment: null,
  
  classNames: ['issue-list'],
  classNameBindings: ['deleted'],
  
  deleted: function() {
    return this.get('comment.deleted');
  }.property('comment.deleted'),
  
  modifedAt: function() {
    var u = this.get('comment.updatedAt');
    
    if(u) return u;
    else return this.get('comment.createdAt');
  }.property('comment.createdAt', 'comment.updatedAt')
});