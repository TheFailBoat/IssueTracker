App.CommentChange = DS.Model.extend({
  commentId: DS.attr('number'),
  comment: DS.belongsTo('App.Comment'),
  
  column: DS.attr('string'),
  oldValue: DS.attr('string'),
  newValue: DS.attr('string')
});