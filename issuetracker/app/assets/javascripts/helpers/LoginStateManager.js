App.LoginStateManager = Ember.StateManager.createWithMixins({
  initialState: function() {
    //TODO check cookie
    return "isNotAuthenticated";
  }.property(),
  isAuthenticated: Ember.State.createWithMixins({
    isAdmin: function() {
      return true;
    }.property(),
    isMod: function() {
      return true;
    }.property(),
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
    enter: function () {
      console.log("enter " + this.name);
    },
    login: function (manager, credentials) {
      // todo get auth token & set cookie

      console.log(credentials);
      manager.transitionTo('isAuthenticated');
    }
  })
});