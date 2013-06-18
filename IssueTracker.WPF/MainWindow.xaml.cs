using System.Windows;
using System.Windows.Navigation;
using IssueTracker.WPF.ViewModels;

namespace IssueTracker.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel Data { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Data = new MainWindowViewModel();
            DataContext = Data;

            Loaded += OnLoaded;
            MainFrame.Navigating += MainFrame_Navigating;
        }

        void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
           
        }

        async void OnLoaded(object sender, RoutedEventArgs e)
        {
            Data.CurrentUser = await App.GetCurrentUser();

            if (Data.CurrentUser == null)
            {
                MainFrame.Navigate(new Views.Login());
            }
            else
            {
                MainFrame.Navigate(new Views.Issues.List());
            }
        }
    }
}
