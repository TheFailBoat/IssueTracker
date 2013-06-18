﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IssueTracker.WPF.Core;
using ServiceStack.ServiceInterface.Auth;
using Auth = ServiceStack.Common.ServiceClient.Web.Auth;

namespace IssueTracker.WPF.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var auth = new Auth
            {
                provider = CredentialsAuthProvider.Name,
                UserName = UsernameText.Text,
                Password = PasswordText.Password,
                RememberMe = true,
            };

            LoginButton.IsEnabled = false;

            App.Client.PostAsync(auth,
                async (response) => await Dispatcher.InvokeAsync(async () =>
                {
                    // success!
                    await App.GetCurrentUser();

                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();

                    NavigationService.Navigate(new Issues.List(), ClearHistory.Instance);
                }), async (response, exception) => await Dispatcher.InvokeAsync(() =>
                {
                    LoginButton.IsEnabled = true;

                    UsernameText.Foreground = PasswordText.Foreground = new SolidColorBrush(Colors.Red);

                    PasswordText.Focus();
                }));

            PasswordText.Password = "";
        }
    }
}