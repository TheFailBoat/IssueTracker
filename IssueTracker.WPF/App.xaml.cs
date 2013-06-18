using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using IssueTracker.Data;
using IssueTracker.Data.Requests;
using IssueTracker.WPF.Annotations;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using Auth = ServiceStack.Common.ServiceClient.Web.Auth;

namespace IssueTracker.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static Person _currentUser;
        public static ServiceClientBase Client { get; private set; }

        public static Person CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (Equals(_currentUser, value)) return;
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public App()
        {
            var baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            Client = new JsonServiceClient(baseUrl);

            /*Client.Get(new Auth
            {
                provider = CredentialsAuthProvider.Name,
                UserName = "admin",
                Password = "password",
                RememberMe = true
            });*/

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                                                              {
                                                                  MessageBox.Show(args.ExceptionObject.ToString(), "Error");
                                                              };
        }

        public static async Task<Person> GetCurrentUser()
        {
            try
            {
                return await Task.Run(() => CurrentUser = Client.Get(new AuthCheck()));
            }
            catch (WebServiceException ex)
            {
                return null;
            }
        }

        public static event PropertyChangedEventHandler PropertyChanged;

        protected static void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
