App.Router.map(function() {
  this.route('login');

  this.resource('issues', { path: '/i' }, function() {
    this.route('add');    
  });
  this.resource('issue', { path: '/i/:issue_id' }, function() {
    this.route('edit');
    this.route('delete');
  });

  this.resource('customers', { path: '/c' }, function() {
    this.route('add');
    this.resource('customer', { path: '/:id' }, function() {
      this.route('edit');
      this.route('delete');
    });
  });
  
  this.resource('users', { path: '/u' }, function() {
    this.route('add');
    this.resource('user', { path: '/:id' }, function() {
      this.route('edit');
      this.route('delete');
    });
  });
});
