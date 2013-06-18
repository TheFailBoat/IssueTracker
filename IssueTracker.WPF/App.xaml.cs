using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using IssueTracker.Data;
using IssueTracker.Data.Requests;
using ServiceStack.ServiceClient.Web;

namespace IssueTracker.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceClientBase Client { get; private set; }

        public App()
        {
            var baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            Client = new JsonServiceClient(baseUrl);

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                                                              {
                                                                  MessageBox.Show(args.ExceptionObject.ToString(), "Error");
                                                              };
        }

        public static async Task<Person> GetCurrentUser()
        {
            try
            {
                return await Task.Run(() => Client.Get(new AuthCheck()));
            }
            catch (WebServiceException ex)
            {
                return null;
            }
        }
    }
}
