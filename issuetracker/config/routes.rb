Issuetracker::Application.routes.draw do
  get "home/home"
  root :to => 'home#home'
end
