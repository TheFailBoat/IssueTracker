App.Comment = DS.Model.extend({
  issueId: DS.attr('number'),
  issue: DS.belongsTo('App.Issue'),
  personId: DS.attr('number'),
  person: DS.belongsTo('App.User'),
  
  message: DS.attr('string'),
  deleted: DS.attr('boolean'),
  
  createdAt: DS.attr('date'),
  updatedAt: DS.attr('date')
});