Ember.Handlebars.registerHelper('formatDateTime', function(path, options) {
    var rawDate = this.get(path);
    return new Date(rawDate).toLocaleString();
});
Ember.Handlebars.registerHelper('formatDate', function(path, options) {
    var rawDate = this.get(path);
    return new Date(rawDate).toLocaleDateString();
});
Ember.Handlebars.registerHelper('formatTime', function(path, options) {
    var rawDate = this.get(path);
    return new Date(rawDate).toLocaleTimeString();
});