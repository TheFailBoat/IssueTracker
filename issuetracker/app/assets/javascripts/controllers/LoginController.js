App.LoginController = Ember.Controller.extend({

  reset: function() {
    this.setProperties({
      username: "",
      password: "",
      errorMessage: ""
    });
  },

  login: function() {
    var self = this;
    var username = self.get('username'), password = self.get('password');

    self.set('hasError', false);
    
    $.ajax(App.ApiBase + '/auth/login', { 
      data: { username: username, password: password },
      type: 'POST',
      dataType: 'json'
    })
    .done(function(response) {
      App.LoginStateManager.send('login', response);
      self.transitionToRoute('index');
    })
    .fail(function() {
      self.set('hasError', true);
    });
  }
});