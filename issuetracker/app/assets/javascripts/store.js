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


var serializer = DS.RESTSerializer.extend({
  keyForAttributeName: function(type, name) {
    return name;
  },
  keyForBelongsTo: function(type, name) {
    var key = this.keyForAttributeName(type, name);

    if (this.embeddedType(type, name)) {
      return key;
    }

    return key + "Id";
  },
  keyForHasMany: function(type, name) {
    var key = this.keyForAttributeName(type, name);

    if (this.embeddedType(type, name)) {
      return key;
    }

    return this.singularize(key) + "Ids";
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