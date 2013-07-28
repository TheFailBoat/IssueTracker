//= require_self
//= require ./store
//= require_tree ./models
//= require_tree ./controllers
//= require_tree ./views
//= require_tree ./helpers
//= require_tree ./templates
//= require ./router
//= require_tree ./routes

App.LoginMixin = Ember.Mixin.create({
  authStateBinding: Ember.Binding.oneWay('App.LoginStateManager.currentState'),
  authState: null,
  
  isAuthenticated: function () {
    return (this.get('authState.name') == 'isAuthenticated');
  }.property('authState'),
  authToken: function () {
    return App.LoginStateManager.get('authToken');
  }.property(),
  currentUser: function () {
    return this.get('authState.currentUser');
  }.property('authState'),
  isAdmin: function () {
    return this.get('authState.isAdmin');
  }.property('authState'),
  isMod: function () {
    return this.get('authState.isMod');
  }.property('authState'),
});