App.Priority = DS.Model.extend({
  name: DS.attr('string'),
  colour: DS.attr('string'),
  
  isClosed: DS.attr('boolean'),
  
  order: DS.attr('number')
});