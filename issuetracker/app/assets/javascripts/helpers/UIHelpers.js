if (!Date.prototype.toISOString) {
    Date.prototype.toISOString = function() {
        function pad(n) { return n < 10 ? '0' + n : n }
        return this.getUTCFullYear() + '-'
            + pad(this.getUTCMonth() + 1) + '-'
            + pad(this.getUTCDate()) + 'T'
            + pad(this.getUTCHours()) + ':'
            + pad(this.getUTCMinutes()) + ':'
            + pad(this.getUTCSeconds()) + 'Z';
    };
}

App.DateView = Ember.View.extend({
  tagName: 'attr',
  classNames: ['timeago'],
  attributeBindings: ['dateOut:title'],
  date: null,
  dateOut: function() {
    var d = this.get('date');
    
    Ember.run.scheduleOnce('afterRender', this, 'updateTime');
        
    return d ? d.toISOString() : null;
  }.property('date'),
  
  didInsertElement: function() {
    this.$().timeago();
  },
  
  updateTime: function () {
    this.$().timeago('updateFromDOM');
  }
});

Ember.Handlebars.helper('date', App.DateView);

App.ProgressBarView = Ember.View.extend({
  templateName: 'progress',
  classNames: ['progress', 'secondary'],
  progress: null,

  style: function() {
    var progress = this.get('progress');

    return 'width:' + progress + '%;';
  }.property('progress').cacheable()
});
Ember.Handlebars.helper('progress', App.ProgressBarView);