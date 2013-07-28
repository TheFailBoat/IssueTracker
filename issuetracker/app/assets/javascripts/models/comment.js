App.Comment = DS.Model.extend({
  issue: DS.belongsTo('App.Issue'),
  person: DS.belongsTo('App.User'),
  
  message: DS.attr('string'),
  deleted: DS.attr('boolean'),
  
  createdAt: DS.attr('date'),
  updatedAt: DS.attr('date'),
  
  changes: DS.hasMany('App.CommentChange')
});

App.RESTAdapter.map('App.Comment', {
  changes: { embedded: 'load' }
});