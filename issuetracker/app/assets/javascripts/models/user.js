App.User = DS.Model.extend({
  customerId: DS.attr('number'),
  customer: DS.belongsTo('App.Customer'),

  name: DS.attr('string'),
  email: DS.attr('string'),
  
  isAdmin: DS.attr('boolean'),
  isMod: DS.attr('boolean')
});