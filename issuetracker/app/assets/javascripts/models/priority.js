App.Priority = DS.Model.extend({
  name: DS.attr('string'),
  colour: DS.attr('string'),
  
  order: DS.attr('number')
});