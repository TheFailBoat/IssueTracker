App.Issue = DS.Model.extend({
  customer: DS.belongsTo('App.Customer'),
  
  reporter: DS.belongsTo('App.User'),
  
  category:  DS.belongsTo('App.Category'),
  status:  DS.belongsTo('App.Status'),
  priority: DS.belongsTo('App.Priority'),
  
  title: DS.attr('string'),
  description: DS.attr('string'),
  
  progress: DS.attr('number'),
  deleted: DS.attr('boolean'),
  
  createdAt: DS.attr('date'),
  updatedAt: DS.attr('date')
});