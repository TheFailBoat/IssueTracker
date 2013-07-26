App.Router.map(function() {
  this.resource('issues', { url: '/i' }, function() {
    this.route('add');
    this.resource('issue', { url: '/:id' }, function() {
      this.route('edit');
      this.route('delete');
    });
  }); 

  this.resource('customers', { url: '/i' }, function() {
    this.route('add');
    this.resource('customer', { url: '/:id' }, function() {
      this.route('edit');
      this.route('delete');
    });
  });
  
  this.resource('users', { url: '/i' }, function() {
    this.route('add');
    this.resource('user', { url: '/:id' }, function() {
      this.route('edit');
      this.route('delete');
    });
  });
});
