App.LoginStateManager = Ember.StateManager.createWithMixins({
  initialState: function() {
    if($.sessionStorage.isSet('login-token')) {
      
      var user = Ember.Object.create($.sessionStorage.get('login-user'));
      
      this.isAuthenticated.set('currentUser', user);
      return 'isAuthenticated';
    }

    return 'isNotAuthenticated';
  }.property(),
  authToken: function() {
    return $.sessionStorage.get('login-token');
  }.property(),
  isAuthenticated: Ember.State.createWithMixins({
    isAdmin: function() {
      return this.get('currentUser').get('isAdmin');
    }.property('currentUser'),
    isMod: function() {
      return this.get('currentUser').get('isMod');
    }.property('currentUser'),
    enter: function () {
      console.log("enter " + this.name);
    },
    logout: function (manager, context) {
      //todo delete cookie & auth token

      manager.transitionTo('isNotAuthenticated');
    }
  }),
  isNotAuthenticated: Ember.State.createWithMixins({
    isAdmin: function() {
      return false;
    }.property(),
    isMod: function() {
      return false;
    }.property(), 
    currentUser: function() {
      return null;
    }.property(),
    enter: function () {
      console.log("enter " + this.name);
    },
    login: function (manager, authResponse) {
      $.sessionStorage.set('login-token', authResponse.authToken);
      $.sessionStorage.set('login-user', authResponse.user);

      var user = Ember.Object.create(authResponse.user);
      
      manager.transitionTo('isAuthenticated');
      App.LoginStateManager.isAuthenticated.set('currentUser', user);
    }
  })
});