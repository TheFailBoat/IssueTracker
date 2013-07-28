App.CommentChange = DS.Model.extend({
  comment: DS.belongsTo('App.Comment'),
  
  column: DS.attr('string'),
  oldValue: DS.attr('string'),
  newValue: DS.attr('string')
});