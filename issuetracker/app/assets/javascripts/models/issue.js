App.Issue = DS.Model.extend({
  customerId: DS.attr('number'),
  customer: DS.belongsTo('App.Customer'),
  
  reporterId: DS.attr('number'),
  reporter: DS.belongsTo('App.User'),
  
  categoryId: DS.attr('number'),
  category:  DS.belongsTo('App.Category'),
  statusId: DS.attr('number'),
  status:  DS.belongsTo('App.Status'),
  priorityId: DS.attr('number'),
  priority: DS.belongsTo('App.Priority'),
  
  title: DS.attr('string'),
  description: DS.attr('string'),
  
  progress: DS.attr('number'),
  deleted: DS.attr('boolean'),
  
  createdAt: DS.attr('date'),
  updatedAt: DS.attr('date')
});