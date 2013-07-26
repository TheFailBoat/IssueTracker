App.Store = DS.Store.extend({

});

DS.RESTAdapter.configure("plurals", {
  category: "categories",
  priority: "priorities",
  status: "statuses",
});

DS.RESTAdapter.reopen({
  url: 'http://localhost/issuetracker'
});