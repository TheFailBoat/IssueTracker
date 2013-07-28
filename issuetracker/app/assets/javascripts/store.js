App.Store = DS.Store.extend({
  adapter: 'App.RESTAdapter'
});

DS.RESTAdapter.configure("plurals", {
  category: "categories",
  priority: "priorities",
  status: "statuses",
});

App.ApiBase = 'http://localhost/issuetracker';

DS.RESTAdapter.reopen({
  url: App.ApiBase
});


App.meta = Ember.Object.create();
var serializer = DS.RESTSerializer.extend({
  extractMeta: function(loader, type, json) {
    var metaConfig = this.configurationForType(type);
    
    for(var x in metaConfig) {
      var meta = json[metaConfig[x]];
      if (meta) { 
        Ember.set('App.meta.' + x, meta);
      } else {
        Ember.set('App.meta.' + x, null);
      }
    }
  
    this._super(loader, type, json);
  }
}).create();

App.RESTAdapter = DS.RESTAdapter.extend(App.LoginMixin, {
  serializer: serializer,
  
  ajax: function(url, type, hash) {
    hash         = hash || {};
    hash.headers = hash.headers || {};
    
    if(this.get('isAuthenticated')) {
      hash.headers['Authorization'] = 'Basic ' + this.get('authToken');
    }
    return this._super(url, type, hash);
  },
});

serializer.configure({
  pagination: 'pagination',
  //pageCount: 'pageCount',
  //itemCount: 'itemCount',
  //itemTotalCount : 'itemTotalCount',
});